using Engine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class RulesTests
    {
        [Test]
        public void CanBeCreated()
        {
            var rules = new Rules();
            Assert.NotNull(rules);
        }

        [Test]
        public void DefinesMinAndMaxNeighbors()
        {
            Assert.AreEqual(0, Rules.MinNeighbors);
            Assert.AreEqual(8, Rules.MaxNeighbors);
        }

        [Test]
        public void DefinesMinAndMaxCountItems()
        {
            Assert.AreEqual(1, Rules.MinCount);
            Assert.AreEqual(8, Rules.MaxCount);
        }

        [Test]
        public void ProvidesSurviveNeighborCounts()
        {
            var rules = new Rules();
            Assert.IsInstanceOf<List<int>>(rules.SurviveNeighborCounts);
        }

        [Test]
        public void CanBeCreatedWithDefaultSurviveCounts()
        {
            var grid = new Rules();
            Assert.IsNotNull(grid.SurviveNeighborCounts);
            Assert.AreEqual(2, grid.SurviveNeighborCounts.Count);
            Assert.IsTrue(grid.SurviveNeighborCounts.Contains(2));
            Assert.IsTrue(grid.SurviveNeighborCounts.Contains(3));
        }

        [Test]
        public void ProvidesBirthNeighborCounts()
        {
            var rules = new Rules();
            Assert.IsInstanceOf<List<int>>(rules.BirthNeighborCounts);
        }

        [Test]
        public void CanBeCreatedWithDefaultBirthCounts()
        {
            var grid = new Rules();
            Assert.IsNotNull(grid.BirthNeighborCounts);
            Assert.AreEqual(1, grid.BirthNeighborCounts.Count);
            Assert.IsTrue(grid.BirthNeighborCounts.Contains(3));
        }

        [Test]
        public void CanBeCreatedWithCustomCounts()
        {
            var surviveCounts = new List<int> { 1, 2, 3 };
            var birthCounts = new List<int> { 7, 8 };

            var grid = new Rules(surviveCounts, birthCounts);
            CollectionAssert.AreEqual(surviveCounts, grid.SurviveNeighborCounts);
            CollectionAssert.AreEqual(birthCounts, grid.BirthNeighborCounts);
        }

        [Test]
        public void ThrowsOnNullCustomCounts()
        {
            Assert.Throws<ArgumentNullException>(() => new Rules(null, new List<int> { 1 }));
            Assert.Throws<ArgumentNullException>(() => new Rules(new List<int> { 1 }, null));
        }

        [Test]
        public void ThrowsOnEmptyAndTooLongCustomCounts()
        {
            var emptyList = new List<int>();
            var tooManyList = Enumerable.Repeat(1, Rules.MaxCount + 1).ToList();
            var validList = new List<int> { 1 };
            Assert.Throws<ArgumentOutOfRangeException>(() => new Rules(emptyList, validList));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Rules(validList, emptyList));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Rules(tooManyList, validList));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Rules(validList, tooManyList));
        }

        [Test]
        public void ThrowsOnCustomCountsWithAnInvalidCount()
        {
            var invalidHighItemList = new List<int> { Rules.MaxNeighbors + 1};
            var invalidLowItemList = new List<int> { Rules.MinNeighbors - 1 };
            var validList = new List<int> { 1 };
            Assert.Throws<ArgumentOutOfRangeException>(() => new Rules(invalidHighItemList, validList));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Rules(invalidLowItemList, validList));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Rules(validList, invalidHighItemList));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Rules(validList, invalidLowItemList));
        }
    }
}
