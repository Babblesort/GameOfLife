using Engine;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class GenerationTests
    {
        [Test]
        public void HasExpectedDataStructure()
        {
            var generation = new Generation();
            Assert.IsInstanceOf<Dictionary<RowCol, bool>>(generation);
        }

        [Test]
        public void HasLiveCellsProperty()
        {
            var generation = new Generation();
            Assert.IsFalse(generation.HasLiveCells);

            var cell = new RowCol(0, 0);

            generation.Add(cell, false);
            Assert.IsFalse(generation.HasLiveCells);

            generation[cell] = true;
            Assert.IsTrue(generation.HasLiveCells);
        }
    }
}
