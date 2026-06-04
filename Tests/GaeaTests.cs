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

    private static void AssertAllCellsAreFalse(Generation generation, Grid grid)
    {
        for (int r = 0; r < grid.RowCount; r++)
        {
            for (int c = 0; c < grid.ColCount; c++)
            {
                Assert.That(generation[new RowCol(r, c)], Is.False, 
                    $"Cell at ({r},{c}) should be false after clear");
            }
        }
    }

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

    [Test]
    public void ClearWorksWhenNoSimulationIsRunning()
    {
        var grid = new Grid(2, 2);
        var initialCells = grid.CreateRandomGeneration();
        Generation capturedGeneration = null!;
        int capturedGenerationNumber = -1;
        
        var gaea = new Gaea(grid, new Rules(), initialCells, (genNum, gen, ms) =>
        {
            capturedGenerationNumber = genNum;
            capturedGeneration = gen;
        });

        gaea.Clear();
        Assert.That(capturedGenerationNumber, Is.EqualTo(0), "Generation number should be reset to 0");
        AssertAllCellsAreFalse(capturedGeneration, grid);
    }

    [Test]
    public void ClearResetsGameStateAndStopsSimulation()
    {
        var grid = new Grid(3, 3);
        var initialCells = grid.CreateRandomGeneration();
        Generation capturedGeneration = null!;
        int capturedGenerationNumber = -1;
        double capturedMilliseconds = -1;
        int generationBeforeClear = -1;
        
        var gaea = new Gaea(grid, new Rules(), initialCells, (genNum, gen, ms) =>
        {
            capturedGenerationNumber = genNum;
            capturedGeneration = gen;
            capturedMilliseconds = ms;
            if (genNum > generationBeforeClear)
                generationBeforeClear = genNum;
        });

        gaea.Run();
        Task.Delay(50).Wait();
        Assert.That(generationBeforeClear, Is.GreaterThan(0), "Generation should be greater than 0 before clear");

        gaea.Clear();
        Assert.That(capturedGenerationNumber, Is.EqualTo(0), "Generation number should be reset to 0");
        Assert.That(capturedMilliseconds, Is.EqualTo(0.0), "Milliseconds should be 0 for cleared state");
        AssertAllCellsAreFalse(capturedGeneration, grid);
    }


}
