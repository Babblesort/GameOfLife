using Engine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;

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
            Assert.AreEqual(grid.Cells.GetType(), typeof(List<RowCol>));
            Assert.AreEqual(100, grid.Cells.Count);
        }

        [Test]
        public void GridDefinesDefaultRowsAndCols()
        {
            Assert.NotNull(Grid.DefaultRows);
            Assert.NotNull(Grid.DefaultCols);
            Assert.IsTrue(Grid.DefaultRows >= Grid.MinRows && Grid.DefaultRows <= Grid.MaxRows);
            Assert.IsTrue(Grid.DefaultCols >= Grid.MinCols && Grid.DefaultCols <= Grid.MaxCols);
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
            Assert.AreEqual(Grid.DefaultRows * Grid.DefaultCols, grid.Cells.Count);
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
        public void GridProvidesCellCount()
        {
            var grid = new Grid(3, 5);
            Assert.AreEqual(15, grid.CellCount);
        }

        [Test]
        public void RowCountCanBeSet()
        {
            var grid = new Grid(2, 3);
            Assert.AreEqual(2, grid.RowCount);

            grid.RowCount = 50;
            Assert.AreEqual(50, grid.RowCount);
        }

        [Test]
        public void SettingRowCountUpdatesCells()
        {
            var grid = new Grid(2, 3);
            Assert.AreEqual(6, grid.Cells.Count);

            grid.RowCount = 5;
            Assert.AreEqual(15, grid.Cells.Count);
        }

        [Test]
        public void ColCountCanBeSet()
        {
            var grid = new Grid(2, 3);
            Assert.AreEqual(3, grid.ColCount);

            grid.ColCount = 5;
            Assert.AreEqual(5, grid.ColCount);
        }

        [Test]
        public void SettingColCountUpdatesCells()
        {
            var grid = new Grid(2, 3);
            Assert.AreEqual(6, grid.Cells.Count);

            grid.ColCount = 5;
            Assert.AreEqual(10, grid.Cells.Count);
        }

        [Test]
        public void RowAndColMinAndMaxAreEnforced()
        {
            var tooFewRows = Grid.MinRows - 1;
            var tooManyRows = Grid.MaxRows + 1;
            var tooFewCols = Grid.MinCols - 1;
            var tooManyCols = Grid.MaxCols + 1;

            var grid = new Grid();

            Assert.Throws<ArgumentOutOfRangeException>(() => grid.RowCount = tooFewRows);
            Assert.Throws<ArgumentOutOfRangeException>(() => grid.RowCount = tooManyRows);
            Assert.Throws<ArgumentOutOfRangeException>(() => grid.ColCount = tooFewCols);
            Assert.Throws<ArgumentOutOfRangeException>(() => grid.ColCount = tooManyCols);
        }

        [Test]
        public void RowCountPropertyChangeEventsCanBeSubscribedTo()
        {
            var grid = new Grid();
            var callBackCalled = false;
            var callBackAction = new PropertyChangedEventHandler((sender, args) =>
            {
                Assert.IsInstanceOf<Grid>(sender);
                Assert.AreEqual("RowCount", args.PropertyName);
                callBackCalled = true;
            });

            grid.PropertyChanged += callBackAction;
            grid.RowCount = 9;
            Assert.IsTrue(callBackCalled);
        }

        [Test]
        public void ColCountPropertyChangeEventsCanBeSubscribedTo()
        {
            var grid = new Grid();
            var callBackCalled = false;
            var callBackAction = new PropertyChangedEventHandler((sender, args) =>
            {
                Assert.IsInstanceOf<Grid>(sender);
                Assert.AreEqual("ColCount", args.PropertyName);
                callBackCalled = true;
            });

            grid.PropertyChanged += callBackAction;
            grid.ColCount = 9;
            Assert.IsTrue(callBackCalled);
        }

        [Test]
        public void CanGenerateAnEmptyGeneration()
        {
            var grid = new Grid(2, 2);
            var generation = grid.CreateEmptyGeneration();

            Assert.AreEqual(typeof(Generation), generation.GetType());
            Assert.AreEqual(4, generation.Count);

            Assert.IsFalse(generation[new RowCol(0, 0)]);
            Assert.IsFalse(generation[new RowCol(0, 1)]);
            Assert.IsFalse(generation[new RowCol(1, 0)]);
            Assert.IsFalse(generation[new RowCol(1, 1)]);

            bool unused;
            Assert.Throws<KeyNotFoundException>(() => unused = generation[new RowCol(1, 2)]);
        }

        [Test]
        public void CanGenerateARandomizedGeneration()
        {
            var grid = new Grid(2, 2);
            var generation = grid.CreateRandomGeneration();

            Assert.AreEqual(typeof(Generation), generation.GetType());
            Assert.AreEqual(4, generation.Count);
        }

        [Test, TestCaseSource(nameof(RowColExpectedNeighbors))]
        public void GridCanDeriveNeighborsForCell(RowCol cell, RowCol[] neighbors)
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
                    new RowCol(0, 0), new[]
                    {
                        new RowCol(2, 2), new RowCol(2, 0), new RowCol(2, 1),
                        new RowCol(0, 2), new RowCol(0, 1),
                        new RowCol(1, 2), new RowCol(1, 0), new RowCol(1, 1)
                    }
                );
                yield return new TestCaseData(
                    new RowCol(0, 1), new[]
                    {
                        new RowCol(2, 0), new RowCol(2, 1), new RowCol(2, 2),
                        new RowCol(0, 0), new RowCol(0, 2),
                        new RowCol(1, 0), new RowCol(1, 1), new RowCol(1, 2)
                    }
                );
                yield return new TestCaseData(
                    new RowCol(0, 2), new[]
                    {
                        new RowCol(2, 1), new RowCol(2, 2), new RowCol(2, 0),
                        new RowCol(0, 1), new RowCol(0, 0),
                        new RowCol(1, 1), new RowCol(1, 2), new RowCol(1, 0)
                    }
                );
                yield return new TestCaseData(
                    new RowCol(1, 0), new[]
                    {
                        new RowCol(0, 2), new RowCol(0, 0), new RowCol(0, 1),
                        new RowCol(1, 2), new RowCol(1, 1),
                        new RowCol(2, 2), new RowCol(2, 0), new RowCol(2, 1)
                    }
                );
                yield return new TestCaseData(
                    new RowCol(1, 1), new[]
                    {
                        new RowCol(0, 0), new RowCol(0, 1), new RowCol(0, 2),
                        new RowCol(1, 0), new RowCol(1, 2),
                        new RowCol(2, 0), new RowCol(2, 1), new RowCol(2, 2)
                    }
                );
                yield return new TestCaseData(
                    new RowCol(1, 2), new[]
                    {
                        new RowCol(0, 1), new RowCol(0, 2), new RowCol(0, 0),
                        new RowCol(1, 1), new RowCol(1, 0),
                        new RowCol(2, 1), new RowCol(2, 2), new RowCol(2, 0)
                    }
                );
                yield return new TestCaseData(
                    new RowCol(2, 0), new[]
                    {
                        new RowCol(1, 2), new RowCol(1, 0), new RowCol(1, 1),
                        new RowCol(2, 2), new RowCol(2, 1),
                        new RowCol(0, 2), new RowCol(0, 0), new RowCol(0, 1)
                    }
                );
                yield return new TestCaseData(
                    new RowCol(2, 1), new[]
                    {
                        new RowCol(1, 0), new RowCol(1, 1), new RowCol(1, 2),
                        new RowCol(2, 0), new RowCol(2, 2),
                        new RowCol(0, 0), new RowCol(0, 1), new RowCol(0, 2)
                    }
                );
                yield return new TestCaseData(
                    new RowCol(2, 2), new[]
                    {
                        new RowCol(1, 1), new RowCol(1, 2), new RowCol(1, 0),
                        new RowCol(2, 1), new RowCol(2, 0),
                        new RowCol(0, 1), new RowCol(0, 2), new RowCol(0, 0)
                    }
                );
            }
        }
    }
}
