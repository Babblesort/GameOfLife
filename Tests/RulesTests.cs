using Engine;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class RulesTests
{
    [Test]
    public void CanBeCreated()
    {
        var rules = new Rules();
        Assert.That(rules, Is.Not.Null);
    }

    [Test]
    public void DefinesMinAndMaxNeighbors()
    {
        Assert.That(Rules.MinNeighbors, Is.EqualTo(0));
        Assert.That(Rules.MaxNeighbors, Is.EqualTo(8));
    }

    [Test]
    public void DefinesMinAndMaxCountItems()
    {
        Assert.That(Rules.MinCount, Is.EqualTo(1));
        Assert.That(Rules.MaxCount, Is.EqualTo(8));
    }

    [Test]
    public void ProvidesSurviveNeighborCounts()
    {
        var rules = new Rules();
        Assert.That(rules.SurviveNeighborCounts, Is.InstanceOf<IReadOnlyList<int>>());
    }

    [Test]
    public void CanBeCreatedWithDefaultSurviveCounts()
    {
        var grid = new Rules();
        Assert.That(grid.SurviveNeighborCounts, Is.Not.Null);
        Assert.That(grid.SurviveNeighborCounts.Count, Is.EqualTo(2));
        Assert.That(grid.SurviveNeighborCounts.Contains(2), Is.True);
        Assert.That(grid.SurviveNeighborCounts.Contains(3), Is.True);
    }

    [Test]
    public void ProvidesBirthNeighborCounts()
    {
        var rules = new Rules();
        Assert.That(rules.BirthNeighborCounts, Is.InstanceOf<IReadOnlyList<int>>());
    }

    [Test]
    public void CanBeCreatedWithDefaultBirthCounts()
    {
        var grid = new Rules();
        Assert.That(grid.BirthNeighborCounts, Is.Not.Null);
        Assert.That(grid.BirthNeighborCounts.Count, Is.EqualTo(1));
        Assert.That(grid.BirthNeighborCounts.Contains(3), Is.True);
    }

    [Test]
    public void CanBeCreatedWithCustomCounts()
    {
        var surviveCounts = new List<int> { 1, 2, 3 };
        var birthCounts = new List<int> { 7, 8 };

        var grid = new Rules(surviveCounts, birthCounts);
        Assert.That(grid.SurviveNeighborCounts, Is.EqualTo(surviveCounts));
        Assert.That(grid.BirthNeighborCounts, Is.EqualTo(birthCounts));
    }

    [Test]
    public void ThrowsOnNullCustomCounts()
    {
        Assert.That((Action)(() => { new Rules(null!, new List<int> { 1 }); }), Throws.TypeOf<ArgumentNullException>());
        Assert.That((Action)(() => { new Rules(new List<int> { 1 }, null!); }), Throws.TypeOf<ArgumentNullException>());
    }

    [Test]
    public void ThrowsOnEmptyAndTooLongCustomCounts()
    {
        var emptyList = new List<int>();
        var tooManyList = Enumerable.Repeat(1, Rules.MaxCount + 1).ToList();
        var validList = new List<int> { 1 };
        Assert.That((Action)(() => { new Rules(emptyList, validList); }), Throws.TypeOf<ArgumentOutOfRangeException>());
        Assert.That((Action)(() => { new Rules(validList, emptyList); }), Throws.TypeOf<ArgumentOutOfRangeException>());
        Assert.That((Action)(() => { new Rules(tooManyList, validList); }), Throws.TypeOf<ArgumentOutOfRangeException>());
        Assert.That((Action)(() => { new Rules(validList, tooManyList); }), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void ThrowsOnCustomCountsWithAnInvalidCount()
    {
        var invalidHighItemList = new List<int> { Rules.MaxNeighbors + 1};
        var invalidLowItemList = new List<int> { Rules.MinNeighbors - 1 };
        var validList = new List<int> { 1 };
        Assert.That((Action)(() => { new Rules(invalidHighItemList, validList); }), Throws.TypeOf<ArgumentOutOfRangeException>());
        Assert.That((Action)(() => { new Rules(invalidLowItemList, validList); }), Throws.TypeOf<ArgumentOutOfRangeException>());
        Assert.That((Action)(() => { new Rules(validList, invalidHighItemList); }), Throws.TypeOf<ArgumentOutOfRangeException>());
        Assert.That((Action)(() => { new Rules(validList, invalidLowItemList); }), Throws.TypeOf<ArgumentOutOfRangeException>());
    }
}
