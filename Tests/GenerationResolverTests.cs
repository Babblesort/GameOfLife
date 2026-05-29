using Engine;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class GenerationResolverTests
{
    private Grid grid = null!;
    private RowCol cell0;
    private RowCol cell1;
    private RowCol cell2;
    private RowCol cell3;
    private RowCol cell4;
    private RowCol cell5;
    private RowCol cell6;
    private RowCol cell7;
    private RowCol cell8;
    private Generation generation = null!;
    private Generation expectedNextGen = null!;

    public void SetupBasicResolver()
    {
        // XOO
        // OXO
        // OOX
        grid = new Grid(3, 3);
        cell0 = new RowCol(0, 0);
        cell1 = new RowCol(0, 1);
        cell2 = new RowCol(0, 2);
        cell3 = new RowCol(1, 0);
        cell4 = new RowCol(1, 1);
        cell5 = new RowCol(1, 2);
        cell6 = new RowCol(2, 0);
        cell7 = new RowCol(2, 1);
        cell8 = new RowCol(2, 2);
        generation = new Generation(3, 3)
        {
            { cell0, true },
            { cell1, false },
            { cell2, false },
            { cell3, false },
            { cell4, true },
            { cell5, false },
            { cell6, false },
            { cell7, false },
            { cell8, true }
        };

        expectedNextGen = new Generation(3, 3)
        {
            { cell0, true },
            { cell1, true },
            { cell2, true },
            { cell3, true },
            { cell4, true },
            { cell5, true },
            { cell6, true },
            { cell7, true },
            { cell8, true }
        };
    }

    public void SetupExpiringResolver()
    {
        // OOO
        // OXO
        // OOO
        grid = new Grid(3, 3);
        cell0 = new RowCol(0, 0);
        cell1 = new RowCol(0, 1);
        cell2 = new RowCol(0, 2);
        cell3 = new RowCol(1, 0);
        cell4 = new RowCol(1, 1);
        cell5 = new RowCol(1, 2);
        cell6 = new RowCol(2, 0);
        cell7 = new RowCol(2, 1);
        cell8 = new RowCol(2, 2);
        generation = new Generation(3, 3)
        {
            { cell0, false },
            { cell1, false },
            { cell2, false },
            { cell3, false },
            { cell4, true },
            { cell5, false },
            { cell6, false },
            { cell7, false },
            { cell8, false }
        };

        expectedNextGen = new Generation(3, 3)
        {
            { cell0, false },
            { cell1, false },
            { cell2, false },
            { cell3, false },
            { cell4, false },
            { cell5, false },
            { cell6, false },
            { cell7, false },
            { cell8, false }
        };
    }

    [Test]
    public void NextGenResolutionCanBeRequested()
    {
        SetupBasicResolver();
        var nextGen = new Generation(grid.RowCount, grid.ColCount);
        GenerationResolver.ResolveNextGeneration(grid, new Rules(), generation, nextGen);
        Assert.AreEqual(expectedNextGen, nextGen);

        SetupExpiringResolver();
        nextGen = new Generation(grid.RowCount, grid.ColCount);
        GenerationResolver.ResolveNextGeneration(grid, new Rules(), generation, nextGen);
        Assert.AreEqual(expectedNextGen, nextGen);
    }

    [Test]
    public void ResolveNextGenerationRespectsCustomRules()
    {
        // Diagonal live cells each have 2 neighbors; dead cells each have 3.
        // Default rules would birth all dead cells (birth={3}); custom rules here require 4 for birth.
        // Verifies that the bitmask path uses the supplied rules rather than hardcoded defaults.
        SetupBasicResolver();
        var rules = new Rules(new List<int> { 2 }, new List<int> { 4 });
        var nextGen = new Generation(grid.RowCount, grid.ColCount);
        GenerationResolver.ResolveNextGeneration(grid, rules, generation, nextGen);
        var expected = new Generation(3, 3)
        {
            { cell0, true },
            { cell1, false },
            { cell2, false },
            { cell3, false },
            { cell4, true },
            { cell5, false },
            { cell6, false },
            { cell7, false },
            { cell8, true }
        };
        Assert.AreEqual(expected, nextGen);
    }

    [Test]
    public void CellNeighborCount()
    {
        SetupBasicResolver();
        Assert.AreEqual(2, GenerationResolver.NeighborsCount(cell0, grid, generation));
        Assert.AreEqual(3, GenerationResolver.NeighborsCount(cell1, grid, generation));
        Assert.AreEqual(3, GenerationResolver.NeighborsCount(cell2, grid, generation));
        Assert.AreEqual(3, GenerationResolver.NeighborsCount(cell3, grid, generation));
        Assert.AreEqual(2, GenerationResolver.NeighborsCount(cell4, grid, generation));
        Assert.AreEqual(3, GenerationResolver.NeighborsCount(cell5, grid, generation));
        Assert.AreEqual(3, GenerationResolver.NeighborsCount(cell6, grid, generation));
        Assert.AreEqual(3, GenerationResolver.NeighborsCount(cell7, grid, generation));
        Assert.AreEqual(2, GenerationResolver.NeighborsCount(cell8, grid, generation));
    }

}
