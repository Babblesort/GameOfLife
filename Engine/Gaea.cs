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
    private readonly double[] _millisecondRingBuffer = new double[60];
    private int _msIndex;
    private Generation _spare;

    public Gaea(Grid grid, Rules rules, Action<int, Generation, double> update, Generation cells)
    {
        ArgumentNullException.ThrowIfNull(grid);
        ArgumentNullException.ThrowIfNull(rules);
        Grid = grid;
        Rules = rules;
        Cells = cells;
        _spare = new Generation(cells.Rows, cells.Cols);
        UpdateVisualization = update;
    }

    public Grid Grid { get; }
    public Rules Rules { get; }
    public Generation Cells { get; private set; }
    public Action<int, Generation, double> UpdateVisualization { get; }
    public event Action? Stopped;

    public int DelayMilliseconds
    {
        get => _delay;
        set
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(value, MinDelayMilliseconds);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, MaxDelayMilliseconds);
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
        if (Cells.Count != Grid.CellCount)
        {
            throw new ArgumentException($"{nameof(Cells)} count and {nameof(Grid)} cell count do not match", nameof(Cells));
        }

        _task = Task.Run(() => ResolveGenerationsAsync(runMode, token), token);
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
        var startTimestamp = Stopwatch.GetTimestamp();
        Cells.ResolveNextGeneration(Rules, _spare);
        (Cells, _spare) = (_spare, Cells);
        UpdateVisualization(++_generationNumber, Cells, RecordMilliseconds(Stopwatch.GetElapsedTime(startTimestamp).TotalMilliseconds));

        if (Cells.IsExtinct)
        {
            Stopped?.Invoke();
            return;
        }

        if (!runMode) return;

        while (true)
        {
            try { await Task.Delay(_delay, token); }
            catch (OperationCanceledException) { return; }
            startTimestamp = Stopwatch.GetTimestamp();
            Cells.ResolveNextGeneration(Rules, _spare);
            (Cells, _spare) = (_spare, Cells);
            UpdateVisualization(++_generationNumber, Cells, RecordMilliseconds(Stopwatch.GetElapsedTime(startTimestamp).TotalMilliseconds));

            if (Cells.IsExtinct)
            {
                Stopped?.Invoke();
                return;
            }
        }
    }

    private double RecordMilliseconds(double ms)
    {
        _millisecondRingBuffer[_msIndex % 60] = ms;
        _msIndex++;
        int count = Math.Min(_msIndex, 60);
        double sum = 0;
        for (int i = 0; i < count; i++)
        {
            sum += _millisecondRingBuffer[i];
        }
        return sum / count;
    }
}
