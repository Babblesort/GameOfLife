using Engine;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class GridTests
    {
        [Test]
        public void GridCanBeCreated()
        {
            var grid = new Grid(1, 1);
            Assert.NotNull(grid);
        }

        [Test]
        public void GridHasCells()
        {
            const int rows = 10;
            const int cols = 10;
            var grid = new Grid(rows, cols);
            Assert.AreEqual(grid.Cells.GetType(), typeof(List<RowColTuple>));
            Assert.AreEqual(100, grid.Cells.Count);
        }

        [Test]
        public void GridDefinesMinAndMaxRows()
        {
            Assert.NotNull(Grid.MinRows);
            Assert.NotNull(Grid.MaxRows);
            Assert.AreEqual(Grid.MinRows.GetType(), typeof(int));
            Assert.AreEqual(Grid.MaxRows.GetType(), typeof(int));
            Assert.AreEqual(1, Grid.MinRows);
            Assert.IsTrue(Grid.MaxRows > Grid.MinRows);
        }

        [Test]
        public void GridDefinesMinAndMaxCols()
        {
            Assert.NotNull(Grid.MinCols);
            Assert.NotNull(Grid.MaxCols);
            Assert.AreEqual(Grid.MinCols.GetType(), typeof(int));
            Assert.AreEqual(Grid.MaxCols.GetType(), typeof(int));
            Assert.AreEqual(1, Grid.MinCols);
            Assert.IsTrue(Grid.MaxCols > Grid.MinCols);
        }

        [Test]
        public void GridCanBeCreatedWithDefaultSize()
        {
            var grid = new Grid();
            Assert.AreEqual(Grid.MaxRows * Grid.MaxCols, grid.Cells.Count);
        }

        [Test]
        public void GridEnforcesMinAndMaxRows()
        {
            var subMin = Grid.MinRows - 1;
            var overMax = Grid.MaxRows + 1;
            Assert.Throws<ArgumentOutOfRangeException>(() => new Grid(rows: subMin, cols: 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Grid(rows: overMax, cols: 1));
        }

        [Test]
        public void GridEnforcesMinAndMaxCols()
        {
            var subMin = Grid.MinCols - 1;
            var overMax = Grid.MaxCols + 1;
            Assert.Throws<ArgumentOutOfRangeException>(() => new Grid(rows: 1, cols: subMin));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Grid(rows: 1, cols: overMax));
        }

        [Test] 
        public void GridProvidesRowsCount()
        {
            var grid = new Grid(3, 5);
            Assert.AreEqual(3, grid.RowCount);
        }

        [Test]
        public void GridProvidesColsCount()
        {
            var grid = new Grid(3, 5);
            Assert.AreEqual(5, grid.ColCount);
        }

        [Test]
        public void CanGenerateAnEmptyGeneration()
        {
            var grid = new Grid(2, 2);
            var generation = grid.CreateEmptyGeneration();

            Assert.AreEqual(typeof(Generation), generation.GetType());
            Assert.AreEqual(4, generation.Count);

            Assert.IsFalse(generation[new RowColTuple(0, 0)]);
            Assert.IsFalse(generation[new RowColTuple(0, 1)]);
            Assert.IsFalse(generation[new RowColTuple(1, 0)]);
            Assert.IsFalse(generation[new RowColTuple(1, 1)]);

            bool unused;
            Assert.Throws<KeyNotFoundException>(() => unused = generation[new RowColTuple(1, 2)]);
        }

        [Test]
        public void GridCanDeriveCellIndexFromTuple()
        {
            var grid = new Grid(3, 3);
            Assert.AreEqual(0, grid.CellIndex(new RowColTuple(0, 0)));
            Assert.AreEqual(1, grid.CellIndex(new RowColTuple(0, 1)));
            Assert.AreEqual(2, grid.CellIndex(new RowColTuple(0, 2)));
            Assert.AreEqual(3, grid.CellIndex(new RowColTuple(1, 0)));
            Assert.AreEqual(4, grid.CellIndex(new RowColTuple(1, 1)));
            Assert.AreEqual(5, grid.CellIndex(new RowColTuple(1, 2)));
            Assert.AreEqual(6, grid.CellIndex(new RowColTuple(2, 0)));
            Assert.AreEqual(7, grid.CellIndex(new RowColTuple(2, 1)));
            Assert.AreEqual(8, grid.CellIndex(new RowColTuple(2, 2)));
        }

        [Test]
        public void CellIndexEnforcesMinRowAndCol()
        {
            var grid = new Grid(1, 1);
            Assert.Throws<ArgumentOutOfRangeException>(() => grid.CellIndex(new RowColTuple(row: -1, col: 1)));
            Assert.Throws<ArgumentOutOfRangeException>(() => grid.CellIndex(new RowColTuple(row: 1, col: -1)));
        }

        [Test]
        public void CellIndexEnforcesMaxRowColCombo()
        {
            var grid = new Grid(2, 2);
            Assert.Throws<ArgumentOutOfRangeException>(() => grid.CellIndex(new RowColTuple(row: 2, col: 1)));
            Assert.Throws<ArgumentOutOfRangeException>(() => grid.CellIndex(new RowColTuple(row: 1, col: 2)));
        }

        [Test]
        public void GridCanDeriveCellRowColTupleFromIndex()
        {
            var grid = new Grid(3, 3);
            Assert.AreEqual(new RowColTuple(0, 0), grid.CellRowCol(index: 0));
            Assert.AreEqual(new RowColTuple(0, 1), grid.CellRowCol(index: 1));
            Assert.AreEqual(new RowColTuple(0, 2), grid.CellRowCol(index: 2));
            Assert.AreEqual(new RowColTuple(1, 0), grid.CellRowCol(index: 3));
            Assert.AreEqual(new RowColTuple(1, 1), grid.CellRowCol(index: 4));
            Assert.AreEqual(new RowColTuple(1, 2), grid.CellRowCol(index: 5));
            Assert.AreEqual(new RowColTuple(2, 0), grid.CellRowCol(index: 6));
            Assert.AreEqual(new RowColTuple(2, 1), grid.CellRowCol(index: 7));
            Assert.AreEqual(new RowColTuple(2, 2), grid.CellRowCol(index: 8));
        }

        [Test]
        public void CellRowCol_ThrowsOnIndexTooSmall()
        {
            var grid = new Grid(2, 2);
            Assert.Throws<ArgumentOutOfRangeException>(() => grid.CellRowCol(-1));
        }

        [Test]
        public void CellRowCol_ThrowsOnIndexTooLarge()
        {
            var grid = new Grid(2, 2);
            Assert.Throws<ArgumentOutOfRangeException>(() => grid.CellRowCol(4));
        }

        [Test, TestCaseSource("RowColExpectedNeighbors")]
        public void GridCanDeriveNeighborsForCell(RowColTuple cell, RowColTuple[] neighbors)
        {
            var grid = new Grid(3, 3);
            Assert.AreEqual(neighbors[0], grid.NeighborTL(cell));
            Assert.AreEqual(neighbors[1], grid.NeighborTT(cell));
            Assert.AreEqual(neighbors[2], grid.NeighborTR(cell));
            Assert.AreEqual(neighbors[3], grid.NeighborLL(cell));
            Assert.AreEqual(neighbors[4], grid.NeighborRR(cell));
            Assert.AreEqual(neighbors[5], grid.NeighborBL(cell));
            Assert.AreEqual(neighbors[6], grid.NeighborBB(cell));
            Assert.AreEqual(neighbors[7], grid.NeighborBR(cell));
        }

        public static IEnumerable<TestCaseData> RowColExpectedNeighbors
        {
            // Expected neighbors for each cell on a 3x3 grid
            get
            {
                yield return new TestCaseData(
                    new RowColTuple(0, 0), new RowColTuple[]
                    {
                        new RowColTuple(2, 2), new RowColTuple(2, 0), new RowColTuple(2, 1),
                        new RowColTuple(0, 2), new RowColTuple(0, 1),
                        new RowColTuple(1, 2), new RowColTuple(1, 0), new RowColTuple(1, 1)
                    }
                );
                yield return new TestCaseData(
                    new RowColTuple(0, 1), new RowColTuple[]
                    {
                        new RowColTuple(2, 0), new RowColTuple(2, 1), new RowColTuple(2, 2),
                        new RowColTuple(0, 0), new RowColTuple(0, 2),
                        new RowColTuple(1, 0), new RowColTuple(1, 1), new RowColTuple(1, 2)
                    }
                );
                yield return new TestCaseData(
                    new RowColTuple(0, 2), new RowColTuple[]
                    {
                        new RowColTuple(2, 1), new RowColTuple(2, 2), new RowColTuple(2, 0),
                        new RowColTuple(0, 1), new RowColTuple(0, 0),
                        new RowColTuple(1, 1), new RowColTuple(1, 2), new RowColTuple(1, 0)
                    }
                );
                yield return new TestCaseData(
                    new RowColTuple(1, 0), new RowColTuple[]
                    {
                        new RowColTuple(0, 2), new RowColTuple(0, 0), new RowColTuple(0, 1),
                        new RowColTuple(1, 2), new RowColTuple(1, 1),
                        new RowColTuple(2, 2), new RowColTuple(2, 0), new RowColTuple(2, 1)
                    }
                );
                yield return new TestCaseData(
                    new RowColTuple(1, 1), new RowColTuple[]
                    {
                        new RowColTuple(0, 0), new RowColTuple(0, 1), new RowColTuple(0, 2),
                        new RowColTuple(1, 0), new RowColTuple(1, 2),
                        new RowColTuple(2, 0), new RowColTuple(2, 1), new RowColTuple(2, 2)
                    }
                );
                yield return new TestCaseData(
                    new RowColTuple(1, 2), new RowColTuple[]
                    {
                        new RowColTuple(0, 1), new RowColTuple(0, 2), new RowColTuple(0, 0),
                        new RowColTuple(1, 1), new RowColTuple(1, 0),
                        new RowColTuple(2, 1), new RowColTuple(2, 2), new RowColTuple(2, 0)
                    }
                );
                yield return new TestCaseData(
                    new RowColTuple(2, 0), new RowColTuple[]
                    {
                        new RowColTuple(1, 2), new RowColTuple(1, 0), new RowColTuple(1, 1),
                        new RowColTuple(2, 2), new RowColTuple(2, 1),
                        new RowColTuple(0, 2), new RowColTuple(0, 0), new RowColTuple(0, 1)
                    }
                );
                yield return new TestCaseData(
                    new RowColTuple(2, 1), new RowColTuple[]
                    {
                        new RowColTuple(1, 0), new RowColTuple(1, 1), new RowColTuple(1, 2),
                        new RowColTuple(2, 0), new RowColTuple(2, 2),
                        new RowColTuple(0, 0), new RowColTuple(0, 1), new RowColTuple(0, 2)
                    }
                );
                yield return new TestCaseData(
                    new RowColTuple(2, 2), new RowColTuple[]
                    {
                        new RowColTuple(1, 1), new RowColTuple(1, 2), new RowColTuple(1, 0),
                        new RowColTuple(2, 1), new RowColTuple(2, 0),
                        new RowColTuple(0, 1), new RowColTuple(0, 2), new RowColTuple(0, 0)
                    }
                );
            }
        }
    }
}
