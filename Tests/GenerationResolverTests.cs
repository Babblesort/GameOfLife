using Engine;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
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

        generation = new Generation(3, 3);
        generation[cell0] = true;
        generation[cell1] = false;
        generation[cell2] = false;
        generation[cell3] = false;
        generation[cell4] = true;
        generation[cell5] = false;
        generation[cell6] = false;
        generation[cell7] = false;
        generation[cell8] = true;

        expectedNextGen = new Generation(3, 3);
        expectedNextGen[cell0] = true;
        expectedNextGen[cell1] = true;
        expectedNextGen[cell2] = true;
        expectedNextGen[cell3] = true;
        expectedNextGen[cell4] = true;
        expectedNextGen[cell5] = true;
        expectedNextGen[cell6] = true;
        expectedNextGen[cell7] = true;
        expectedNextGen[cell8] = true;
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

        generation = new Generation(3, 3);
        generation[cell0] = false;
        generation[cell1] = false;
        generation[cell2] = false;
        generation[cell3] = false;
        generation[cell4] = true;
        generation[cell5] = false;
        generation[cell6] = false;
        generation[cell7] = false;
        generation[cell8] = false;

        expectedNextGen = new Generation(3, 3);
        expectedNextGen[cell0] = false;
        expectedNextGen[cell1] = false;
        expectedNextGen[cell2] = false;
        expectedNextGen[cell3] = false;
        expectedNextGen[cell4] = false;
        expectedNextGen[cell5] = false;
        expectedNextGen[cell6] = false;
        expectedNextGen[cell7] = false;
        expectedNextGen[cell8] = false;
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
        var rules = new Rules([2], [4]);
        var nextGen = new Generation(grid.RowCount, grid.ColCount);
        GenerationResolver.ResolveNextGeneration(grid, rules, generation, nextGen);
        var expectedNextGen = new Generation(3, 3);
        expectedNextGen[cell0] = true;
        expectedNextGen[cell1] = false;
        expectedNextGen[cell2] = false;
        expectedNextGen[cell3] = false;
        expectedNextGen[cell4] = true;
        expectedNextGen[cell5] = false;
        expectedNextGen[cell6] = false;
        expectedNextGen[cell7] = false;
        expectedNextGen[cell8] = true;

        Assert.AreEqual(expectedNextGen, nextGen);
    }
}
