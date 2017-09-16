﻿using System;
using System.Linq;

namespace Engine
{
    public class LifeRunner
    {
        private Grid _grid;
        private Rules _rules;
        private Generation _cells;
        private int _generationCount;

        public Generation CurrentGeneration => _cells;
        public int GenerationCount => _generationCount;
        public bool LivingGeneration => _cells.Any(cell => cell.Value);
        public bool Extinction => !LivingGeneration;

        public event EventHandler<GenerationResolvedEventArgs> OnGenerationResolved;
        protected virtual void GenerationResolved(GenerationResolvedEventArgs e) => OnGenerationResolved?.Invoke(this, e);

        public LifeRunner(Grid grid, Rules rules, Generation cells = null)
        {
            if (grid == null) throw new ArgumentNullException(nameof(grid), "Cannot be null");
            if (rules == null) throw new ArgumentNullException(nameof(rules), "Cannot be null");
            if (cells == null)
            {
                cells = grid.CreateEmptyGeneration();
            }

            if (grid.Cells.Count != cells.Count) throw new ArgumentOutOfRangeException(nameof(cells), "Grid cell count does not match generation cells count");

            _grid = grid;
            _rules = rules;
            _cells = cells;
            _generationCount = 0;
        }

        public void ResolveNextGen()
        {
            _cells = NextGen(_cells);
            _generationCount++;
            GenerationResolved(new GenerationResolvedEventArgs { GenerationCount = _generationCount, Generation = _cells });
        }

        public Generation NextGen(Generation cells)
        {
            var nextGen = new Generation();
            foreach(var cell in cells)
            {
                nextGen.Add(cell.Key, CellAliveNextGen(cell.Value, NeighborsCount(cell.Key)));
            }
            return nextGen;
        }

        public int NeighborsCount(RowCol cell)
        {
            var count = 0;
            count += NeighborCount(_grid.NeighborTL(cell));
            count += NeighborCount(_grid.NeighborTT(cell));
            count += NeighborCount(_grid.NeighborTR(cell));
            count += NeighborCount(_grid.NeighborLL(cell));
            count += NeighborCount(_grid.NeighborRR(cell));
            count += NeighborCount(_grid.NeighborBL(cell));
            count += NeighborCount(_grid.NeighborBB(cell));
            count += NeighborCount(_grid.NeighborBR(cell));
            return count;
        }

        private int NeighborCount(RowCol neighbor) =>  _cells[neighbor] ? 1 : 0;

        public bool CellAliveNextGen(bool alive, int neighborCount)
        {
            var survives = alive && _rules.SurviveNeighborCounts.Contains(neighborCount);
            var born = !alive && _rules.BirthNeighborCounts.Contains(neighborCount);

            return survives || born;
        }
    }
}
