using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Engine
{
    public class Gaea
    {
        public Grid Grid { get; private set; }
        public Rules Rules { get; private set; }
        public Generation Cells { get; private set; }
        public RunStates RunState { get; }
        private int _generationNumber = 0;
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

        public void Run(Action<int, Generation> updateVisualizationFn)
        {
            Task.Factory.StartNew(() =>
                {
                    for (int i = 0; i < 100; i++)
                    {
                        _generationNumber++;
                        var nextCells = GenerationResolver.ResolveNextGeneration(Grid, Rules, Cells);
                        Thread.Sleep(500);
                        updateVisualizationFn(_generationNumber, nextCells);
                        Cells = nextCells;
                    }
                });
        }

    }
}
