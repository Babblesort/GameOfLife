﻿using Engine;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class RowColTests
    {
        [Test]
        public void CanBeCreated()
        {
            var tuple = new RowCol(1, 1);
            Assert.NotNull(tuple);
        }

        [Test]
        public void OverridesDefaultTupleItemsWithRowCol()
        {
            var tuple = new RowCol(1, 2);
            Assert.AreEqual(1, tuple.Row);
            Assert.AreEqual(1, tuple.Item1);
            Assert.AreEqual(2, tuple.Col);
            Assert.AreEqual(2, tuple.Item2);
        }

        [Test]
        public void ToCsv()
        {
            var tuple = new RowCol(1, 2);
            Assert.AreEqual("1,2", tuple.ToCsv());
        }
    }
}
