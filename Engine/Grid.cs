﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Engine.Annotations;

namespace Engine
{
    public class Grid : INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler GridSizeIncreased;
        public event EventHandler GridSizeDecreased;

        public Grid() : this(DefaultRows, DefaultCols) { }

        public Grid(int rows, int cols)
        {
            ValidateGridSize(rows, cols);
            RowCount = rows;
            ColCount = cols;
            UpdateGridCells();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnGridSizeIncreased()
        {
            GridSizeIncreased?.Invoke(this, EventArgs.Empty);
        }

        private void OnGridSizeDecreased()
        {
            GridSizeDecreased?.Invoke(this, EventArgs.Empty);
        }

        public int RowCount
        {
            get { return _rowCount; }
            set
            {
                if (value < MinRows || value > MaxRows) throw new ArgumentOutOfRangeException(nameof(RowCount), $"Must be between {MinRows} and {MaxRows} inclusive");
                if (_rowCount != value)
                {
                    _rowCount = value;
                    OnPropertyChanged(nameof(RowCount));
                    UpdateGridCells();
                }
            }
        }
        public int ColCount
        {
            get { return _colCount; }
            set
            {
                if (value < MinCols || value > MaxCols) throw new ArgumentOutOfRangeException(nameof(ColCount), $"Must be between {MinCols} and {MaxCols} inclusive");
                if (_colCount != value)
                {
                    _colCount = value;
                    OnPropertyChanged(nameof(ColCount));
                    UpdateGridCells();
                }
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
            var afterUpdateCells = CreateGridCells();
            var isSizeIncrease = afterUpdateCells.Count > Cells?.Count;
            Cells = afterUpdateCells;
            if (isSizeIncrease)
            {
                OnGridSizeIncreased();
            }
            else
            {
                OnGridSizeDecreased();
            }
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
