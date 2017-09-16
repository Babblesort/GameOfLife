using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    public class Grid
    {
        public static readonly int MinRows = 1;
        public static readonly int MaxRows = 250;
        public static readonly int MinCols = 1;
        public static readonly int MaxCols = 250;
        public List<RowColTuple> Cells { get; private set; }
        public int RowCount { get; private set; }
        public int ColCount { get; private set; }

        public Grid() : this(MaxRows, MaxCols) { }

        public Grid(int rows, int cols)
        {
            ValidateGridSize(rows, cols);
            RowCount = rows;
            ColCount = cols;
            Cells = CreateGridCells();
        }

        public int CellIndex(RowColTuple tuple)
        {
            if (tuple.Row < 0) throw new ArgumentOutOfRangeException(nameof(tuple.Row), "Must be zero or greater");
            if (tuple.Col < 0) throw new ArgumentOutOfRangeException(nameof(tuple.Col), "Must be zero or greater");

            var index = tuple.Row * ColCount + tuple.Col;
            if (index > Cells.Count - 1)
            {
                throw new ArgumentOutOfRangeException(nameof(tuple), "Requested index is outside grid");
            }

            return index;
        }

        public RowColTuple CellRowCol(int index)
        {
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index), "Must be zero or greater");
            if (index >= Cells.Count) throw new ArgumentOutOfRangeException(nameof(index), "Requested index is outside grid");

            var row = (int)(index / ColCount);
            var col = index % ColCount;

            return new RowColTuple(row, col);
        }

        public Generation CreateEmptyGeneration()
        {
            var generation = new Generation();
            Cells.ForEach(cell => generation.Add(cell, false));
            return generation;
        }

        public RowColTuple NeighborTL(RowColTuple cell) => new RowColTuple(Up(cell.Row), Left(cell.Col));
        public RowColTuple NeighborTT(RowColTuple cell) => new RowColTuple(Up(cell.Row), cell.Col);
        public RowColTuple NeighborTR(RowColTuple cell) => new RowColTuple(Up(cell.Row), Right(cell.Col));
        public RowColTuple NeighborLL(RowColTuple cell) => new RowColTuple(cell.Row, Left(cell.Col));
        public RowColTuple NeighborRR(RowColTuple cell) => new RowColTuple(cell.Row, Right(cell.Col));
        public RowColTuple NeighborBL(RowColTuple cell) => new RowColTuple(Down(cell.Row), Left(cell.Col));
        public RowColTuple NeighborBB(RowColTuple cell) => new RowColTuple(Down(cell.Row), cell.Col);
        public RowColTuple NeighborBR(RowColTuple cell) => new RowColTuple(Down(cell.Row), Right(cell.Col));

        private int Left(int col) => col - 1 < 0 ? ColCount - 1 : col - 1;
        private int Right(int col) => col + 1 == ColCount ? 0 : col + 1;
        private int Up(int row) => row - 1 < 0 ? RowCount - 1 : row - 1;
        private int Down(int row) => row + 1 == RowCount ? 0 : row + 1;

        private static void ValidateGridSize(int rows, int cols)
        {
            if (rows < MinRows || rows > MaxRows)
            {
                throw new ArgumentOutOfRangeException(nameof(rows), $"Must be between {MinRows} and {MaxRows} inclusive");
            }

            if (cols < MinCols || cols > MaxCols)
            {
                throw new ArgumentOutOfRangeException(nameof(cols), $"Must be between {MinCols} and {MaxCols} inclusive");
            }
        }

        private List<RowColTuple> CreateGridCells()
        {
            var tuples = new List<RowColTuple>(RowCount * ColCount);
            var rowIndices = Enumerable.Range(0, RowCount);
            var colIndices = Enumerable.Range(0, ColCount);

            foreach (var r in rowIndices)
            {
                foreach (var c in colIndices)
                {
                    tuples.Add(new RowColTuple(r, c));
                }
            }
            return tuples;
        }
    }
}
