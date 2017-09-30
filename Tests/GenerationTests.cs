using Engine;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

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

        [Test]
        public void ToCsv()
        {
            var generation = new Generation
            {
                { new RowCol(0, 0), true },
                { new RowCol(0, 1), false }
            };

            var csv = generation.ToCsv().ToList();
            Assert.AreEqual(2, csv.Count);
            Assert.AreEqual("0,0,True", csv[0]);
            Assert.AreEqual("0,1,False", csv[1]);
        }

        [Test]
        public void ToCsvHandlesEmptyGeneration()
        {
            var generation = new Generation();

            var csv = generation.ToCsv().ToList();
            Assert.AreEqual(0, csv.Count);
        }
    }
}
