using Engine;
using NUnit.Framework;
using System;

namespace Tests
{
    [TestFixture]
    public class GaeaTests
    {
        [Test]
        public void CanBeCreated()
        {
            var grid = new Grid();
            var rules = new Rules();

            var gaea = new Gaea(grid, rules);
            Assert.NotNull(gaea);
        }

        [Test]
        public void ThrowsOnNullDependencies()
        {
            var grid = new Grid();
            var rules = new Rules();

            Assert.Throws<ArgumentNullException>(() => new Gaea(null, rules));
            Assert.Throws<ArgumentNullException>(() => new Gaea(grid, null));
        }

        [Test]
        public void DelayMillisecondsSettings()
        {
            Assert.AreEqual(25, Gaea.MinDelayMilliseconds);
            Assert.AreEqual(500, Gaea.MaxDelayMilliseconds);
            Assert.AreEqual(225, Gaea.DefaultDelayMilliseconds);
        }

        [Test]
        public void DelayMillisecondsPropertyDefaultsAndSets()
        {
            var gaea = new Gaea(new Grid(), new Rules());
            Assert.AreEqual(Gaea.DefaultDelayMilliseconds, gaea.DelayMilliseconds);

            gaea.DelayMilliseconds = 100;
            Assert.AreEqual(100, gaea.DelayMilliseconds);
        }

        [Test]
        public void ThrowsOnInvalidDelay()
        {
            var tooSmall = Gaea.MinDelayMilliseconds - 1;
            var tooBig = Gaea.MaxDelayMilliseconds + 1;
            var gaea = new Gaea(new Grid(), new Rules());
            Assert.Throws<ArgumentOutOfRangeException>(() => gaea.DelayMilliseconds = tooSmall);
            Assert.Throws<ArgumentOutOfRangeException>(() => gaea.DelayMilliseconds = tooBig);
        }

        [Test]
        public void StepThrowsOnNullCells()
        {
            var gaea = new Gaea(new Grid(), new Rules());
            Assert.Throws<ArgumentNullException>(() => gaea.Step((i, cells) => { Assert.True(false, "Should not be here"); }));
        }

        [Test]
        public void StepThrowsOnMissizedCells()
        {
            var gen = new Generation { { new RowCol(0, 0), false } };
            var gaea = new Gaea(new Grid(2, 2), new Rules(), gen);
            Assert.Throws<ArgumentException>(() => gaea.Step((i, cells) => { Assert.True(false, "Should not be here"); }));
        }

        [Test]
        public void RunThrowsOnNullCells()
        {
            var gaea = new Gaea(new Grid(), new Rules());
            Assert.Throws<ArgumentNullException>(() => gaea.Run((i, cells) => { Assert.True(false, "Should not be here"); }));
        }

        [Test]
        public void RunThrowsOnMissizedCells()
        {
            var gen = new Generation { { new RowCol(0, 0), false } };
            var gaea = new Gaea(new Grid(2, 2), new Rules(), gen);
            Assert.Throws<ArgumentException>(() => gaea.Run((i, cells) => { Assert.True(false, "Should not be here"); }));
        }

    }
}
