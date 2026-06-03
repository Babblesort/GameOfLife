using Engine;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class GaeaTests
{
    private static readonly Grid stubGrid = new(1, 1);
    private static readonly Generation stubGen = new(1, 1);
    private static readonly Rules stubRules = new();
    private static readonly GenerationUpdateHandler stubHandler = (_, _, _) => { };

    [Test]
    public void CanBeCreated()
    {
        var gaea = new Gaea(stubGrid, stubRules, stubGen, stubHandler);
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
        var gaea = new Gaea(stubGrid, stubRules, stubGen, stubHandler);
        Assert.That(gaea.DelayMilliseconds, Is.EqualTo(Gaea.DefaultDelayMilliseconds));

        gaea.DelayMilliseconds = 100;
        Assert.That(gaea.DelayMilliseconds, Is.EqualTo(100));
    }

    [Test]
    public void ThrowsOnInvalidDelay()
    {
        var tooSmall = Gaea.MinDelayMilliseconds - 1;
        var tooBig = Gaea.MaxDelayMilliseconds + 1;
        var gaea = new Gaea(stubGrid, stubRules, stubGen, stubHandler);
        void assignTooSmall() => gaea.DelayMilliseconds = tooSmall;
        void assignTooBig() => gaea.DelayMilliseconds = tooBig;

        Assert.That((Action)assignTooSmall, Throws.TypeOf<ArgumentOutOfRangeException>());
        Assert.That((Action)assignTooBig, Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void RunOrStepThrowsOnMissizedCells()
    {
        var genOneByOne = new Generation(1, 1);
        var gridTwoByTwo = new Grid(2, 2);
        var gaea = new Gaea(gridTwoByTwo, stubRules, genOneByOne, stubHandler);
        void callGaeaStep() => gaea.Step();
        void callGaeaRun() => gaea.Run();

        Assert.That((Action)callGaeaStep, Throws.TypeOf<InvalidOperationException>());
        Assert.That((Action)callGaeaRun, Throws.TypeOf<InvalidOperationException>());
    }

    [Test]
    public async Task DelayMillisecondsChangeTakesEffectDuringRun()
    {
        var grid = new Grid();
        var cells = grid.CreateRandomGeneration();
        int latestGeneration = 0;
        var gaea = new Gaea(grid, stubRules, cells, (i, _, _) => Interlocked.Exchange(ref latestGeneration, i));

        gaea.DelayMilliseconds = Gaea.MinDelayMilliseconds;
        gaea.Run();
        await Task.Delay(300);
        int genAfterFast = latestGeneration;

        gaea.DelayMilliseconds = Gaea.MaxDelayMilliseconds;
        await Task.Delay(300);
        int genAfterSlow = latestGeneration;

        gaea.Pause();

        Assert.That(genAfterFast, Is.GreaterThan(5));
        Assert.That(genAfterSlow - genAfterFast, Is.LessThan(2));
    }

}
