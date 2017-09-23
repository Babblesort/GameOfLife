using System;
using System.Threading;
using System.Threading.Tasks;

namespace Engine
{
    public class Gaea
    {
        public Grid Grid { get; }
        public Rules Rules { get; }
        public Generation Cells { get; private set; }
        public Action<int, Generation> UpdateVisualization { get; set; }
        public static int MinDelayMilliseconds = 25;
        public static int MaxDelayMilliseconds = 500;
        public static int DefaultDelayMilliseconds = 225;
        private int _generationNumber;
        private int _delay = DefaultDelayMilliseconds;

        private CancellationTokenSource _tokenSource;
        private CancellationToken _token;
        private Task _task;

        public Gaea(Grid grid, Rules rules) : this(grid, rules,  updateFn: (i, c) => { }, cells: null) { }
        public Gaea(Grid grid, Rules rules, Action<int, Generation> updateFn, Generation cells = null)
        {
            if (grid == null) throw new ArgumentNullException(nameof(grid), "Cannot be null");
            if (rules == null) throw new ArgumentNullException(nameof(rules), "Cannot be null");

            Grid = grid;
            Rules = rules;
            Cells = cells;
            UpdateVisualization = updateFn;
        }

        public int DelayMilliseconds
        {
            get { return _delay; }
            set
            {
                if (value < MinDelayMilliseconds || value > MaxDelayMilliseconds)
                    throw new ArgumentOutOfRangeException(nameof(value), $"Must be between {MinDelayMilliseconds} and {MaxDelayMilliseconds} inclusive.");

                _delay = value;
            }
        }

        public void Run()
        {
            PerformGenerationTask(runMode: true);
        }

        public void Step()
        {
            PerformGenerationTask(runMode: false);
        }

        public void Pause()
        {
            CancelIfRunning();
        }

        public void Clear()
        {
            CancelIfRunning();
            _generationNumber = 0;
            UpdateVisualization(_generationNumber, Grid.CreateEmptyGeneration());
        }

        private Task PerformGenerationTask(bool runMode)
        {
            CancelIfRunning();
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;
            ValidateExecuteGenerationConditions();
            _task = Task.Factory.StartNew(() => ResolveGenerations(runMode), _token);
            return _task;
        }

        private void ValidateExecuteGenerationConditions()
        {
            if (Cells == null) throw new ArgumentNullException(nameof(Cells), $"Cannot {nameof(ResolveGenerations)} with null Cells");
            if (Cells.Count != Grid.CellCount) throw new ArgumentException(nameof(Cells), $"{nameof(Cells)} count and {nameof(Grid)} cell count do not match");
        }

        private void CancelIfRunning()
        {
            if (_task?.Status != TaskStatus.Running) return;
            _tokenSource.Cancel();
            _task.Wait();
        }

        private void ResolveGenerations(bool runMode = false)
        {
            do
            {
                _generationNumber++;
                var nextCells = GenerationResolver.ResolveNextGeneration(Grid, Rules, Cells);
                UpdateVisualization(_generationNumber, nextCells);
                Cells = nextCells;
                if (runMode)
                {
                    Thread.Sleep(DelayMilliseconds);
                }
            }
            while (runMode && !_token.IsCancellationRequested);
        }
    }
}
