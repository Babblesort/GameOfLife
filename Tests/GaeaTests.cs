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
        public void ThrowsOnGridAndInitialCellsSizeMismatch()
        {
            var grid = new Grid(2, 2);
            var generation = new Generation { { new RowCol(0, 0), false } };

            Assert.Throws<ArgumentOutOfRangeException>(() => new Gaea(grid, new Rules(), generation));
        }

        [Test]
        public void ExposesRunStatesEnum()
        {
            Assert.AreEqual(0, (int)Gaea.RunStates.Idle);
            Assert.AreEqual(1, (int)Gaea.RunStates.Step);
            Assert.AreEqual(2, (int)Gaea.RunStates.Run);
        }

        [Test]
        public void DefaultsToIdleState()
        {
            var gaea = new Gaea(new Grid(), new Rules());
            Assert.AreEqual(gaea.RunState, Gaea.RunStates.Idle);
        }

        [Test]
        public void DelayMillisecondsSettings()
        {
            Assert.AreEqual(50, Gaea.MinDelayMilliseconds);
            Assert.AreEqual(5000, Gaea.MaxDelayMilliseconds);
            Assert.AreEqual(500, Gaea.DefaultDelayMilliseconds);
        }

        [Test]
        public void DelayMillisecondsPropertyDefaultsAndSets()
        {
            var gaea = new Gaea(new Grid(), new Rules());
            Assert.AreEqual(Gaea.DefaultDelayMilliseconds, gaea.DelayMilliseconds);

            gaea.DelayMilliseconds = 1000;
            Assert.AreEqual(1000, gaea.DelayMilliseconds);
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
    }
}
