using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    public class Grid
    {
        public static readonly int MinRows = 1;
        public static readonly int MaxRows = 200;
        public static readonly int MinCols = 1;
        public static readonly int MaxCols = 200;
        public static readonly int DefaultRows = 45;
        public static readonly int DefaultCols = 45;

        private int _rowCount = DefaultRows;
        private int _colCount = DefaultCols;

        public List<RowCol> Cells { get; private set; }

        public Grid() : this(DefaultRows, DefaultCols) { }

        public Grid(int rows, int cols)
        {
            ValidateGridSize(rows, cols);
            RowCount = rows;
            ColCount = cols;
            UpdateGridCells();
        }

        public int RowCount
        {
            get { return _rowCount; }
            set
            {
                if (value < MinRows || value > MaxRows) throw new ArgumentOutOfRangeException(nameof(RowCount), $"Must be between {MinRows} and {MaxRows} inclusive");
                _rowCount = value;
                UpdateGridCells();
            }
        }
        public int ColCount
        {
            get { return _colCount; }
            set
            {
                if (value < MinCols || value > MaxCols) throw new ArgumentOutOfRangeException(nameof(ColCount), $"Must be between {MinCols} and {MaxCols} inclusive");
                _colCount = value;
                UpdateGridCells();
            }
        }

        public int CellCount => RowCount * ColCount;

        public Generation CreateEmptyGeneration()
        {
            var generation = new Generation();
            Cells.ForEach(cell => generation.Add(cell, false));
            return generation;
        }

        public Generation CreateRandomGeneration()
        {
            var rnd = new Random();
            var generation = new Generation();
            Cells.ForEach(cell => generation.Add(cell, rnd.Next(0, 100) > 60));
            return generation;
        }

        public RowCol NeighborTL(RowCol cell) => new RowCol(Up(cell.Row), Left(cell.Col));
        public RowCol NeighborTT(RowCol cell) => new RowCol(Up(cell.Row), cell.Col);
        public RowCol NeighborTR(RowCol cell) => new RowCol(Up(cell.Row), Right(cell.Col));
        public RowCol NeighborLL(RowCol cell) => new RowCol(cell.Row, Left(cell.Col));
        public RowCol NeighborRR(RowCol cell) => new RowCol(cell.Row, Right(cell.Col));
        public RowCol NeighborBL(RowCol cell) => new RowCol(Down(cell.Row), Left(cell.Col));
        public RowCol NeighborBB(RowCol cell) => new RowCol(Down(cell.Row), cell.Col);
        public RowCol NeighborBR(RowCol cell) => new RowCol(Down(cell.Row), Right(cell.Col));

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

        private void UpdateGridCells()
        {
            Cells = CreateGridCells();
        }

        private List<RowCol> CreateGridCells()
        {
            var tuples = new List<RowCol>(RowCount * ColCount);
            var rowIndices = Enumerable.Range(0, RowCount);
            var colIndices = Enumerable.Range(0, ColCount);

            tuples.AddRange(from r in rowIndices from c in colIndices select new RowCol(r, c));
            return tuples;
        }
    }
}
