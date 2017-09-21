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
        public static int MinDelayMilliseconds = 25;
        public static int MaxDelayMilliseconds = 500;
        public static int DefaultDelayMilliseconds = 225;
        private int _generationNumber;
        private int _delay = DefaultDelayMilliseconds;

        private CancellationTokenSource _tokenSource;
        private CancellationToken _token;
        private Task _runTask;

        public Gaea(Grid grid, Rules rules, Generation initialCells = null)
        {
            if (grid == null) throw new ArgumentNullException(nameof(grid), "Cannot be null");
            if (rules == null) throw new ArgumentNullException(nameof(rules), "Cannot be null");
            if (initialCells == null)
            {
                initialCells = grid.CreateRandomGeneration();
            }
            if (initialCells.Count != grid.RowCount * grid.ColCount)
                throw new ArgumentOutOfRangeException(nameof(initialCells), "Grid cell count does not match generation cells count");

            Grid = grid;
            Rules = rules;
            Cells = initialCells;
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

        public void Run(Action<int, Generation> updateGui)
        {
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;
            _runTask = Task.Factory.StartNew(() => RunContinuous(updateGui), _token);
        }

        public void Step(Action<int, Generation> updateGui)
        {
            CancelIfRunning();
            Task.Factory.StartNew(() => RunStep(updateGui));
        }

        public void Pause()
        {
            CancelIfRunning();
        }

        public void Clear(Action<int, Generation> updateGui)
        {
            CancelIfRunning();
            _generationNumber = 0;
            updateGui(_generationNumber, Grid.CreateEmptyGeneration());
        }

        private void CancelIfRunning()
        {
            if (_runTask?.Status != TaskStatus.Running) return;
            _tokenSource.Cancel();
            _runTask.Wait();
        }

        private void RunContinuous(Action<int, Generation> updateGui)
        {
            while (!_token.IsCancellationRequested)
            {
                ExecuteLifeGeneration(updateGui, useDelay: true);
            }
        }

        private void RunStep(Action<int, Generation> updateGui)
        {
            ExecuteLifeGeneration(updateGui);
        }

        private void ExecuteLifeGeneration(Action<int, Generation> updateGui, bool useDelay = false)
        {
            _generationNumber++;
            var nextCells = GenerationResolver.ResolveNextGeneration(Grid, Rules, Cells);
            updateGui(_generationNumber, nextCells);
            Cells = nextCells;
            if (useDelay)
            {
                Thread.Sleep(DelayMilliseconds);
            }
        }
    }
}
