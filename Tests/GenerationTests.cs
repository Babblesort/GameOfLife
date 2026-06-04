using Engine;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class GenerationTests
{
    [Test]
    public void ToCsv()
    {
        var generation = new Generation(1, 2);
        generation[new RowCol(0, 0)] = true;
        generation[new RowCol(0, 1)] = false;

        var csv = generation.ToCsv().ToList();
        Assert.That(csv.Count, Is.EqualTo(2));
        Assert.That(csv[0], Is.EqualTo("0,0,True"));
        Assert.That(csv[1], Is.EqualTo("0,1,False"));
    }

    [Test]
    public void ToCsvHandlesEmptyGeneration()
    {
        var generation = new Generation(0, 0);

        var csv = generation.ToCsv().ToList();
        Assert.That(csv.Count, Is.EqualTo(0));
    }

    [Test]
    public void HasLiveCellsProperty()
    {
        var generation = new Generation(1, 1);
        Assert.That(generation.HasLiveCells, Is.False);

        var cell = new RowCol(0, 0);

        generation[cell] = false;
        Assert.That(generation.HasLiveCells, Is.False);

        generation[cell] = true;
        Assert.That(generation.HasLiveCells, Is.True);
    }

    [Test]
    public void IsExtinctProperty()
    {
        var generation = new Generation(1, 1);
        Assert.That(generation.IsExtinct, Is.True);

        var cell = new RowCol(0, 0);

        generation[cell] = false;
        Assert.That(generation.IsExtinct, Is.True);

        generation[cell] = true;
        Assert.That(generation.IsExtinct, Is.False);
    }

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
        generation.ResolveNextGeneration(new Rules(), nextGen);
        Assert.That(nextGen, Is.EqualTo(expectedNextGen));

        SetupExpiringResolver();
        nextGen = new Generation(grid.RowCount, grid.ColCount);
        generation.ResolveNextGeneration(new Rules(), nextGen);
        Assert.That(nextGen, Is.EqualTo(expectedNextGen));
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
        generation.ResolveNextGeneration(rules, nextGen);
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

        Assert.That(nextGen, Is.EqualTo(expectedNextGen));
    }

    [Test]
    public void CopyToProducesRowMajorCopyIndependentOfSource()
    {
        // 2x3 generation with a known pattern
        var generation = new Generation(2, 3);
        generation[new RowCol(0, 0)] = true;
        generation[new RowCol(0, 1)] = false;
        generation[new RowCol(0, 2)] = true;
        generation[new RowCol(1, 0)] = false;
        generation[new RowCol(1, 1)] = true;
        generation[new RowCol(1, 2)] = false;

        var dest = new bool[6];
        generation.CopyTo(dest);

        // Verify row-major order: row 0 cols 0-2, then row 1 cols 0-2
        Assert.That(dest[0], Is.True);
        Assert.That(dest[1], Is.False);
        Assert.That(dest[2], Is.True);
        Assert.That(dest[3], Is.False);
        Assert.That(dest[4], Is.True);
        Assert.That(dest[5], Is.False);

        // Verify the copy is independent: mutating dest does not affect the source
        dest[0] = false;
        Assert.That(generation[new RowCol(0, 0)], Is.True);
    }

    [Test]
    public void CopyToThrowsOnMismatchedDestinationLength()
    {
        var generation = new Generation(2, 3);
        var tooShort = new bool[5];
        var tooLong = new bool[7];
        Assert.That((Action)(() => generation.CopyTo(tooShort)), Throws.TypeOf<ArgumentException>());
        Assert.That((Action)(() => generation.CopyTo(tooLong)), Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void EqualsReturnsTrueForIdenticalGenerations()
    {
        var a = new Generation(2, 2);
        var b = new Generation(2, 2);
        a[new RowCol(0, 0)] = true;
        b[new RowCol(0, 0)] = true;
        Assert.That(a.Equals(b), Is.True);
    }

    [Test]
    public void EqualsReturnsFalseForNonGenerationType()
    {
        var generation = new Generation(2, 2);
        Assert.That(generation.Equals("not a generation"), Is.False);
    }

    [Test]
    public void EqualsReturnsFalseForNull()
    {
        var generation = new Generation(2, 2);
        Assert.That(generation.Equals(null), Is.False);
    }

    [Test]
    public void EqualsReturnsFalseForDifferentDimensions()
    {
        var a = new Generation(2, 3);
        var b = new Generation(3, 2);
        Assert.That(a.Equals(b), Is.False);
    }

    [Test]
    public void EqualsReturnsFalseForDifferentCellData()
    {
        var a = new Generation(2, 2);
        var b = new Generation(2, 2);
        a[new RowCol(0, 0)] = true;
        b[new RowCol(0, 0)] = false;
        Assert.That(a.Equals(b), Is.False);
    }

    [Test]
    public void EqualGenerationsHaveEqualHashCodes()
    {
        var a = new Generation(2, 2);
        var b = new Generation(2, 2);
        a[new RowCol(0, 0)] = true;
        b[new RowCol(0, 0)] = true;
        Assert.That(a.GetHashCode(), Is.EqualTo(b.GetHashCode()));
    }
}
