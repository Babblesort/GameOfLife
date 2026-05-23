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

    public Gaea(Grid grid, Rules rules) : this(grid, rules, updateFn: (i, c) => { }, cells: null) { }

    public Gaea(Grid grid, Rules rules, Action<int, Generation> updateFn, Generation? cells = null)
    {
        if (grid == null) throw new ArgumentNullException(nameof(grid), "Cannot be null");
        if (rules == null) throw new ArgumentNullException(nameof(rules), "Cannot be null");

        Grid = grid;
        Rules = rules;
        Cells = cells;
        UpdateVisualization = updateFn;
    }

    public Grid Grid { get; }
    public Rules Rules { get; }
    public Generation? Cells { get; private set; }
    public Action<int, Generation> UpdateVisualization { get; set; }

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
        UpdateVisualization(_generationNumber, Grid.CreateEmptyGeneration());
    }

    private void PerformGenerationTask(bool runMode)
    {
        CancelIfRunning();
        _tokenSource = new CancellationTokenSource();
        var token = _tokenSource.Token;
        ValidateExecuteGenerationConditions();
        _task = Task.Run(() => ResolveGenerationsAsync(runMode, token), token);
    }

    private void ValidateExecuteGenerationConditions()
    {
        if (Cells == null) throw new ArgumentNullException(nameof(Cells), "Cells must not be null before running");
        if (Cells.Count != Grid.CellCount) throw new ArgumentException(nameof(Cells), $"{nameof(Cells)} count and {nameof(Grid)} cell count do not match");
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
        Cells = GenerationResolver.ResolveNextGeneration(Grid, Rules, Cells!);
        UpdateVisualization(++_generationNumber, Cells);

        if (!runMode) return;

        while (true)
        {
            try { await Task.Delay(_delay, token); }
            catch (OperationCanceledException) { return; }
            Cells = GenerationResolver.ResolveNextGeneration(Grid, Rules, Cells!);
            UpdateVisualization(++_generationNumber, Cells);
        }
    }
}
