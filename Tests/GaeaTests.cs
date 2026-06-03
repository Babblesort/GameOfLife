using Engine;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class GaeaTests
{
    private static readonly GenerationUpdateHandler NoOp = (_, _, _) => { };

    [Test]
    public void CanBeCreated()
    {
        var grid = new Grid();
        var rules = new Rules();

        var gaea = new Gaea(grid, rules, NoOp, grid.CreateEmptyGeneration());
        Assert.That(gaea, Is.Not.Null);
    }

    [Test]
    public void DelayMillisecondsSettings()
    {
        Assert.That(Gaea.MinDelayMilliseconds, Is.EqualTo(25));
        Assert.That(Gaea.MaxDelayMilliseconds, Is.EqualTo(500));
        Assert.That(Gaea.DefaultDelayMilliseconds, Is.EqualTo(225));
    }

    [Test]
    public void DelayMillisecondsPropertyDefaultsAndSets()
    {
        var grid = new Grid();
        var gaea = new Gaea(grid, new Rules(), NoOp, grid.CreateEmptyGeneration());
        Assert.That(gaea.DelayMilliseconds, Is.EqualTo(Gaea.DefaultDelayMilliseconds));

        gaea.DelayMilliseconds = 100;
        Assert.That(gaea.DelayMilliseconds, Is.EqualTo(100));
    }

    [Test]
    public void ThrowsOnInvalidDelay()
    {
        var tooSmall = Gaea.MinDelayMilliseconds - 1;
        var tooBig = Gaea.MaxDelayMilliseconds + 1;
        var grid = new Grid();
        var gaea = new Gaea(grid, new Rules(), NoOp, grid.CreateEmptyGeneration());
        Assert.That((Action)(() => gaea.DelayMilliseconds = tooSmall), Throws.TypeOf<ArgumentOutOfRangeException>());
        Assert.That((Action)(() => gaea.DelayMilliseconds = tooBig), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void StepThrowsOnMissizedCells()
    {
        var gen = new Generation(1, 1);
        gen[new RowCol(0, 0)] = false;
        var gaea = new Gaea(new Grid(2, 2), new Rules(), NoOp, gen);
        Assert.That((Action)(() => gaea.Step()), Throws.TypeOf<InvalidOperationException>());
    }

    [Test]
    public void RunThrowsOnMissizedCells()
    {
        var gen = new Generation(1, 1);
        gen[new RowCol(0, 0)] = false;
        var gaea = new Gaea(new Grid(2, 2), new Rules(), NoOp, gen);
        Assert.That((Action)(() => gaea.Run()), Throws.TypeOf<InvalidOperationException>());
    }

    [Test]
    public async Task DelayMillisecondsChangeTakesEffectDuringRun()
    {
        var grid = new Grid();
        var cells = grid.CreateRandomGeneration();
        int latestGeneration = 0;
        var gaea = new Gaea(grid, new Rules(),
            (i, _, _) => Interlocked.Exchange(ref latestGeneration, i), cells);

        gaea.DelayMilliseconds = Gaea.MinDelayMilliseconds; // 25ms — fast
        gaea.Run();
        await Task.Delay(300);                               // 300ms / 25ms ≈ 12 generations
        int genAfterFast = latestGeneration;

        gaea.DelayMilliseconds = Gaea.MaxDelayMilliseconds; // 500ms — slow
        await Task.Delay(300);                               // 300ms < 500ms, so 0–1 new generations
        int genAfterSlow = latestGeneration;

        gaea.Pause();

        Assert.That(genAfterFast, Is.GreaterThan(5));
        Assert.That(genAfterSlow - genAfterFast, Is.LessThan(2));
    }

}
