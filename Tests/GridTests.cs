using Engine;
using NUnit.Framework;
using System.ComponentModel;

namespace Tests;

[TestFixture]
public class GridTests
{
    [Test]
    public void GridCanBeCreated()
    {
        var grid = new Grid(1, 1);
        Assert.That(grid, Is.Not.Null);
    }

    [Test]
    public void GridHasCells()
    {
        const int rows = 10;
        const int cols = 10;
        var grid = new Grid(rows, cols);
        Assert.That(grid.Cells.GetType(), Is.EqualTo(typeof(List<RowCol>)));
        Assert.That(grid.Cells.Count, Is.EqualTo(100));
    }

    [Test]
    public void GridDefinesDefaultRowsAndCols()
    {
        Assert.That(Grid.DefaultRows, Is.Not.Null);
        Assert.That(Grid.DefaultCols, Is.Not.Null);
        Assert.That(Grid.DefaultRows, Is.InRange(Grid.MinRows, Grid.MaxRows));
        Assert.That(Grid.DefaultCols, Is.InRange(Grid.MinCols, Grid.MaxCols));
    }

    [Test]
    public void GridDefinesMinAndMaxRows()
    {
        Assert.That(Grid.MinRows, Is.Not.Null);
        Assert.That(Grid.MaxRows, Is.Not.Null);
        Assert.That(Grid.MinRows.GetType(), Is.EqualTo(typeof(int)));
        Assert.That(Grid.MaxRows.GetType(), Is.EqualTo(typeof(int)));
        Assert.That(Grid.MinRows, Is.EqualTo(1));
        Assert.That(Grid.MaxRows, Is.GreaterThan(Grid.MinRows));
    }

    [Test]
    public void GridDefinesMinAndMaxCols()
    {
        Assert.That(Grid.MinCols, Is.Not.Null);
        Assert.That(Grid.MaxCols, Is.Not.Null);
        Assert.That(Grid.MinCols.GetType(), Is.EqualTo(typeof(int)));
        Assert.That(Grid.MaxCols.GetType(), Is.EqualTo(typeof(int)));
        Assert.That(Grid.MinCols, Is.EqualTo(1));
        Assert.That(Grid.MaxCols, Is.GreaterThan(Grid.MinCols));
    }

    [Test]
    public void GridCanBeCreatedWithDefaultSize()
    {
        var grid = new Grid();
        Assert.That(grid.Cells.Count, Is.EqualTo(Grid.DefaultRows * Grid.DefaultCols));
    }

    [Test]
    public void GridEnforcesMinAndMaxRows()
    {
        var subMin = Grid.MinRows - 1;
        var overMax = Grid.MaxRows + 1;
        Assert.That((Action)(() => { new Grid(rows: subMin, cols: 1); }), Throws.TypeOf<ArgumentOutOfRangeException>());
        Assert.That((Action)(() => { new Grid(rows: overMax, cols: 1); }), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void GridEnforcesMinAndMaxCols()
    {
        var subMin = Grid.MinCols - 1;
        var overMax = Grid.MaxCols + 1;
        Assert.That((Action)(() => { new Grid(rows: 1, cols: subMin); }), Throws.TypeOf<ArgumentOutOfRangeException>());
        Assert.That((Action)(() => { new Grid(rows: 1, cols: overMax); }), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void GridProvidesRowsCount()
    {
        var grid = new Grid(3, 5);
        Assert.That(grid.RowCount, Is.EqualTo(3));
    }

    [Test]
    public void GridProvidesColsCount()
    {
        var grid = new Grid(3, 5);
        Assert.That(grid.ColCount, Is.EqualTo(5));
    }

    [Test]
    public void GridProvidesCellCount()
    {
        var grid = new Grid(3, 5);
        Assert.That(grid.CellCount, Is.EqualTo(15));
    }

    [Test]
    public void RowCountCanBeSet()
    {
        var grid = new Grid(2, 3);
        Assert.That(grid.RowCount, Is.EqualTo(2));

        grid.RowCount = 50;
        Assert.That(grid.RowCount, Is.EqualTo(50));
    }

    [Test]
    public void SettingRowCountUpdatesCells()
    {
        var grid = new Grid(2, 3);
        Assert.That(grid.Cells.Count, Is.EqualTo(6));

        grid.RowCount = 5;
        Assert.That(grid.Cells.Count, Is.EqualTo(15));
    }

    [Test]
    public void ColCountCanBeSet()
    {
        var grid = new Grid(2, 3);
        Assert.That(grid.ColCount, Is.EqualTo(3));

        grid.ColCount = 5;
        Assert.That(grid.ColCount, Is.EqualTo(5));
    }

    [Test]
    public void SettingColCountUpdatesCells()
    {
        var grid = new Grid(2, 3);
        Assert.That(grid.Cells.Count, Is.EqualTo(6));

        grid.ColCount = 5;
        Assert.That(grid.Cells.Count, Is.EqualTo(10));
    }

    [Test]
    public void RowAndColMinAndMaxAreEnforced()
    {
        var tooFewRows = Grid.MinRows - 1;
        var tooManyRows = Grid.MaxRows + 1;
        var tooFewCols = Grid.MinCols - 1;
        var tooManyCols = Grid.MaxCols + 1;

        var grid = new Grid();

        Assert.That((Action)(() => grid.RowCount = tooFewRows), Throws.TypeOf<ArgumentOutOfRangeException>());
        Assert.That((Action)(() => grid.RowCount = tooManyRows), Throws.TypeOf<ArgumentOutOfRangeException>());
        Assert.That((Action)(() => grid.ColCount = tooFewCols), Throws.TypeOf<ArgumentOutOfRangeException>());
        Assert.That((Action)(() => grid.ColCount = tooManyCols), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void RowCountPropertyChangeEventsCanBeSubscribedTo()
    {
        var grid = new Grid();
        var callBackCalled = false;
        var callBackAction = new PropertyChangedEventHandler((sender, args) =>
        {
            Assert.That(sender, Is.InstanceOf<Grid>());
            Assert.That(args.PropertyName, Is.EqualTo("RowCount"));
            callBackCalled = true;
        });

        grid.PropertyChanged += callBackAction;
        grid.RowCount = 9;
        Assert.That(callBackCalled, Is.True);
    }

    [Test]
    public void RowCountPropertyChangeEventsOnlyFiresOnValueChange()
    {
        var grid = new Grid(3, 3);
        var callBackCalled = false;
        var callBackAction = new PropertyChangedEventHandler((sender, args) =>
        {
            callBackCalled = true;
        });

        grid.PropertyChanged += callBackAction;
        grid.RowCount = 3;
        Assert.That(callBackCalled, Is.False);
    }

    [Test]
    public void ColCountPropertyChangeEventsCanBeSubscribedTo()
    {
        var grid = new Grid();
        var callBackCalled = false;
        var callBackAction = new PropertyChangedEventHandler((sender, args) =>
        {
            Assert.That(sender, Is.InstanceOf<Grid>());
            Assert.That(args.PropertyName, Is.EqualTo("ColCount"));
            callBackCalled = true;
        });

        grid.PropertyChanged += callBackAction;
        grid.ColCount = 9;
        Assert.That(callBackCalled, Is.True);
    }

    [Test]
    public void ColCountPropertyChangeEventsOnlyFiresOnValueChange()
    {
        var grid = new Grid(3, 3);
        var callBackCalled = false;
        var callBackAction = new PropertyChangedEventHandler((sender, args) =>
        {
            callBackCalled = true;
        });

        grid.PropertyChanged += callBackAction;
        grid.ColCount = 3;
        Assert.That(callBackCalled, Is.False);
    }

    [Test]
    public void GridSizeIncreaseEventFiresOnRowCountIncrease()
    {
        var grid = new Grid(3, 3);
        var increaseCallBackCalled = false;
        var decreaseCallBackCalled = false;

        var increaseCallBackAction = new EventHandler((sender, args) =>
        {
            increaseCallBackCalled = true;
        });
        var decreaseCallBackAction = new EventHandler((sender, args) =>
        {
            decreaseCallBackCalled = true;
        });

        grid.GridSizeIncreased += increaseCallBackAction;
        grid.GridSizeDecreased += decreaseCallBackAction;
        grid.RowCount = 4;
        Assert.That(increaseCallBackCalled, Is.True);
        Assert.That(decreaseCallBackCalled, Is.False);
    }

    [Test]
    public void GridSizeIncreaseEventFiresOnColCountIncrease()
    {
        var grid = new Grid(3, 3);
        var increaseCallBackCalled = false;
        var decreaseCallBackCalled = false;

        var increaseCallBackAction = new EventHandler((sender, args) =>
        {
            increaseCallBackCalled = true;
        });
        var decreaseCallBackAction = new EventHandler((sender, args) =>
        {
            decreaseCallBackCalled = true;
        });

        grid.GridSizeIncreased += increaseCallBackAction;
        grid.GridSizeDecreased += decreaseCallBackAction;
        grid.ColCount = 4;
        Assert.That(increaseCallBackCalled, Is.True);
        Assert.That(decreaseCallBackCalled, Is.False);
    }

    [Test]
    public void GridSizeDecreaseEventFiresOnRowCountDecrease()
    {
        var grid = new Grid(3, 3);
        var increaseCallBackCalled = false;
        var decreaseCallBackCalled = false;

        var increaseCallBackAction = new EventHandler((sender, args) =>
        {
            increaseCallBackCalled = true;
        });
        var decreaseCallBackAction = new EventHandler((sender, args) =>
        {
            decreaseCallBackCalled = true;
        });

        grid.GridSizeIncreased += increaseCallBackAction;
        grid.GridSizeDecreased += decreaseCallBackAction;
        grid.RowCount = 2;
        Assert.That(increaseCallBackCalled, Is.False);
        Assert.That(decreaseCallBackCalled, Is.True);
    }

    [Test]
    public void GridSizeDecreaseEventFiresOnColCountDecrease()
    {
        var grid = new Grid(3, 3);
        var increaseCallBackCalled = false;
        var decreaseCallBackCalled = false;

        var increaseCallBackAction = new EventHandler((sender, args) =>
        {
            increaseCallBackCalled = true;
        });
        var decreaseCallBackAction = new EventHandler((sender, args) =>
        {
            decreaseCallBackCalled = true;
        });

        grid.GridSizeIncreased += increaseCallBackAction;
        grid.GridSizeDecreased += decreaseCallBackAction;
        grid.ColCount = 2;
        Assert.That(increaseCallBackCalled, Is.False);
        Assert.That(decreaseCallBackCalled, Is.True);
    }

    [Test]
    public void CanGenerateAnEmptyGeneration()
    {
        var grid = new Grid(2, 2);
        var generation = grid.CreateEmptyGeneration();

        Assert.That(generation.GetType(), Is.EqualTo(typeof(Generation)));
        Assert.That(generation.Count, Is.EqualTo(4));

        Assert.That(generation[new RowCol(0, 0)], Is.False);
        Assert.That(generation[new RowCol(0, 1)], Is.False);
        Assert.That(generation[new RowCol(1, 0)], Is.False);
        Assert.That(generation[new RowCol(1, 1)], Is.False);

        bool unused;
        Assert.That((Action)(() => unused = generation[new RowCol(1, 2)]), Throws.TypeOf<IndexOutOfRangeException>());
    }

    [Test]
    public void CanGenerateARandomizedGeneration()
    {
        var grid = new Grid(2, 2);
        var generation = grid.CreateRandomGeneration();

        Assert.That(generation.GetType(), Is.EqualTo(typeof(Generation)));
        Assert.That(generation.Count, Is.EqualTo(4));
    }

    [Test]
    public void CreateGenerationFromCopiesSameSizeSource()
    {
        var grid = new Grid(2, 2);
        var old = grid.CreateEmptyGeneration();
        old[new RowCol(0, 0)] = true;
        old[new RowCol(1, 1)] = true;

        var result = grid.CreateGenerationFrom(old);

        Assert.That(result[new RowCol(0, 0)], Is.True);
        Assert.That(result[new RowCol(0, 1)], Is.False);
        Assert.That(result[new RowCol(1, 0)], Is.False);
        Assert.That(result[new RowCol(1, 1)], Is.True);
    }

    [Test]
    public void CreateGenerationFromCropsLargerSource()
    {
        var oldGrid = new Grid(3, 3);
        var old = oldGrid.CreateEmptyGeneration();
        old[new RowCol(0, 0)] = true;
        old[new RowCol(2, 2)] = true;

        var grid = new Grid(2, 2);
        var result = grid.CreateGenerationFrom(old);

        Assert.That(result[new RowCol(0, 0)], Is.True);
        Assert.That(result[new RowCol(0, 1)], Is.False);
        Assert.That(result[new RowCol(1, 0)], Is.False);
        Assert.That(result[new RowCol(1, 1)], Is.False);
    }

    [Test]
    public void CreateGenerationFromPadsSmallerSource()
    {
        var oldGrid = new Grid(2, 2);
        var old = oldGrid.CreateEmptyGeneration();
        old[new RowCol(0, 0)] = true;
        old[new RowCol(1, 1)] = true;

        var grid = new Grid(3, 3);
        var result = grid.CreateGenerationFrom(old);

        Assert.That(result[new RowCol(0, 0)], Is.True);
        Assert.That(result[new RowCol(1, 1)], Is.True);
        Assert.That(result[new RowCol(0, 2)], Is.False);
        Assert.That(result[new RowCol(2, 0)], Is.False);
        Assert.That(result[new RowCol(2, 2)], Is.False);
    }

    [Test, TestCaseSource(nameof(RowColExpectedNeighbors))]
    public void GridCanDeriveNeighborsForCell(RowCol cell, RowCol[] neighbors)
    {
        var grid = new Grid(3, 3);
        Assert.That(grid.NeighborTL(cell), Is.EqualTo(neighbors[0]));
        Assert.That(grid.NeighborTT(cell), Is.EqualTo(neighbors[1]));
        Assert.That(grid.NeighborTR(cell), Is.EqualTo(neighbors[2]));
        Assert.That(grid.NeighborLL(cell), Is.EqualTo(neighbors[3]));
        Assert.That(grid.NeighborRR(cell), Is.EqualTo(neighbors[4]));
        Assert.That(grid.NeighborBL(cell), Is.EqualTo(neighbors[5]));
        Assert.That(grid.NeighborBB(cell), Is.EqualTo(neighbors[6]));
        Assert.That(grid.NeighborBR(cell), Is.EqualTo(neighbors[7]));
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
