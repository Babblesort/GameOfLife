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
        private int _generationNumber;
        private int _delay = DefaultDelayMilliseconds;
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

        public void Run(Action<int, Generation> updateVisualizationFn)
        {
            Task.Factory.StartNew(() =>
                {
                    for (int i = 0; i < 100; i++)
                    {
                        _generationNumber++;
                        var nextCells = GenerationResolver.ResolveNextGeneration(Grid, Rules, Cells);
                        Thread.Sleep(DelayMilliseconds);
                        updateVisualizationFn(_generationNumber, nextCells);
                        Cells = nextCells;
                    }
                });
        }
    }
}
