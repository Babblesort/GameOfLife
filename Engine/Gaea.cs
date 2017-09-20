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
        public RunStates RunState { get; }
        public static int MinDelayMilliseconds = 50;
        public static int MaxDelayMilliseconds = 5000;
        public static int DefaultDelayMilliseconds = 500;
        public static int DefaultStopOnGeneration = 250;
        private int _generationNumber;
        private int _stopOnGeneration = DefaultStopOnGeneration;
        private int _delay = DefaultDelayMilliseconds;

        private CancellationTokenSource _tokenSource;
        private CancellationToken _token;

        private bool _runningFlag = false;

        public enum RunStates
        {
            Idle = 0,
            Step = 1,
            Run = 2
        }

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
            RunState = RunStates.Idle;
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

        public int StopOnGeneration
        { 
            get { return _stopOnGeneration; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), $"Must be greater than zero");

                _stopOnGeneration = value;
            }
        }
        
        public void Run(Action<int, Generation> updateGui)
        {
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;
            Task.Factory.StartNew(() => RunToStopGeneration(updateGui, _token), _token);
        }

        public void RunToStopGeneration(Action<int, Generation> updateGui, CancellationToken ct)
        {
            _runningFlag = true;
            while (_generationNumber < StopOnGeneration && !ct.IsCancellationRequested)
            {
                _generationNumber++;
                var nextCells = GenerationResolver.ResolveNextGeneration(Grid, Rules, Cells);
                Thread.Sleep(DelayMilliseconds);
                updateGui(_generationNumber, nextCells);
                Cells = nextCells;
            }
            _runningFlag = false;
        }

        public void PauseRun()
        {
            if (!_token.IsCancellationRequested)
            {
                _tokenSource?.Cancel();
            }
        }

        public void StepGeneration(Action<int, Generation> updateGui)
        {
            if (!_runningFlag)
            {
                Task.Factory.StartNew(() => RunStep(updateGui));
            }
        }

        public void RunStep(Action<int, Generation> updateGui)
        {
            _generationNumber++;
            var nextCells = GenerationResolver.ResolveNextGeneration(Grid, Rules, Cells);
            updateGui(_generationNumber, nextCells);
            Cells = nextCells;
        }
    }
}
