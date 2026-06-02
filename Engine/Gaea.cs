using System.Diagnostics;

namespace Engine;

public class Gaea
{
    public const int MinDelayMilliseconds = 25;
    public const int MaxDelayMilliseconds = 500;
    public const int DefaultDelayMilliseconds = 225;
    private int _generationNumber;
    private int _delay = DefaultDelayMilliseconds;
    private CancellationTokenSource? _tokenSource;
    private Task? _task;
    private readonly double[] _msRingBuffer = new double[60];
    private int _msIdx;
    private Generation? _spare;

    public Gaea(Grid grid, Rules rules, Action<int, Generation, double> updateFn, Generation? cells = null)
    {
        Grid = grid ?? throw new ArgumentNullException(nameof(grid), "Cannot be null");
        Rules = rules ?? throw new ArgumentNullException(nameof(rules), "Cannot be null");
        Cells = cells;
        UpdateVisualization = updateFn;
    }

    public Grid Grid { get; }
    public Rules Rules { get; }
    public Generation? Cells { get; private set; }
    public Action<int, Generation, double> UpdateVisualization { get; }
    public event Action? Stopped;

    public int DelayMilliseconds
    {
        get => _delay;
        set
        {
            if (value < MinDelayMilliseconds || value > MaxDelayMilliseconds)
                throw new ArgumentOutOfRangeException(nameof(value), $"Must be between {MinDelayMilliseconds} and {MaxDelayMilliseconds} inclusive.");
            _delay = value;
        }
    }

    public void Run() => PerformGenerationTask(runMode: true);
    public void Step() => PerformGenerationTask(runMode: false);
    public void Pause() => CancelIfRunning();

    public void Clear()
    {
        CancelIfRunning();
        _generationNumber = 0;
        UpdateVisualization(_generationNumber, Grid.CreateEmptyGeneration(), 0.0);
    }

    private void PerformGenerationTask(bool runMode)
    {
        CancelIfRunning();
        _tokenSource = new CancellationTokenSource();
        var token = _tokenSource.Token;
        ValidateExecuteGenerationConditions();
        if (_spare == null || _spare.Rows != Grid.RowCount || _spare.Cols != Grid.ColCount)
        {
            _spare = new Generation(Grid.RowCount, Grid.ColCount);
        }
        _task = Task.Run(() => ResolveGenerationsAsync(runMode, token), token);
    }

    private void ValidateExecuteGenerationConditions()
    {
        if (Cells == null) throw new ArgumentNullException(nameof(Cells), "Cells must not be null before running");
        if (Cells.Count != Grid.CellCount) throw new ArgumentException($"{nameof(Cells)} count and {nameof(Grid)} cell count do not match", nameof(Cells));
    }

    private void CancelIfRunning()
    {
        if (_task == null || _task.IsCompleted) return;
        _tokenSource!.Cancel();
        try { _task.Wait(); }
        catch (AggregateException) { } // OperationCanceledException from WaitForNextTickAsync cancellation
    }

    private async Task ResolveGenerationsAsync(bool runMode, CancellationToken token)
    {
        var t0 = Stopwatch.GetTimestamp();
        Cells!.ResolveNextGeneration(Rules, _spare!);
        (Cells, _spare) = (_spare, Cells);
        UpdateVisualization(++_generationNumber, Cells!, RecordMs(Stopwatch.GetElapsedTime(t0).TotalMilliseconds));

        if (!Cells!.HasLiveCells)
        {
            Stopped?.Invoke();
            return;
        }

        if (!runMode) return;

        while (true)
        {
            try { await Task.Delay(_delay, token); }
            catch (OperationCanceledException) { return; }
            t0 = Stopwatch.GetTimestamp();
            Cells!.ResolveNextGeneration(Rules, _spare!);
            (Cells, _spare) = (_spare, Cells);
            UpdateVisualization(++_generationNumber, Cells!, RecordMs(Stopwatch.GetElapsedTime(t0).TotalMilliseconds));

            if (!Cells!.HasLiveCells)
            {
                Stopped?.Invoke();
                return;
            }
        }
    }

    private double RecordMs(double ms)
    {
        _msRingBuffer[_msIdx % 60] = ms;
        _msIdx++;
        int count = Math.Min(_msIdx, 60);
        double sum = 0;
        for (int i = 0; i < count; i++)
        {
            sum += _msRingBuffer[i];
        }
        return sum / count;
    }
}
