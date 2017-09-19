using Engine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
