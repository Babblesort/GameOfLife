using Engine;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class GenerationResolverTests
    {
        private Grid grid;
        private RowCol cell0;
        private RowCol cell1;
        private RowCol cell2;
        private RowCol cell3;
        private RowCol cell4;
        private RowCol cell5;
        private RowCol cell6;
        private RowCol cell7;
        private RowCol cell8;
        private Generation generation;
        private Generation expectedNextGen;

        public GenerationResolver CreateBasicResolver()
        {
            // XOO
            // OXO
            // OOX
            grid = new Grid(3, 3);
            cell0 = new RowCol(0, 0);
            cell1 = new RowCol(0, 1);
            cell2 = new RowCol(0, 2);
            cell3 = new RowCol(1, 0);
            cell4 = new RowCol(1, 1);
            cell5 = new RowCol(1, 2);
            cell6 = new RowCol(2, 0);
            cell7 = new RowCol(2, 1);
            cell8 = new RowCol(2, 2);
            generation = new Generation
            {
                { cell0, true },
                { cell1, false },
                { cell2, false },
                { cell3, false },
                { cell4, true },
                { cell5, false },
                { cell6, false },
                { cell7, false },
                { cell8, true }
            };

            expectedNextGen = new Generation
            {
                { cell0, true },
                { cell1, true },
                { cell2, true },
                { cell3, true },
                { cell4, true },
                { cell5, true },
                { cell6, true },
                { cell7, true },
                { cell8, true }
            };
            return new GenerationResolver(grid, new Rules(), generation);
        }

        public GenerationResolver CreateExpiringResolver()
        {
            // OOO
            // OXO
            // OOO
            grid = new Grid(3, 3);
            cell0 = new RowCol(0, 0);
            cell1 = new RowCol(0, 1);
            cell2 = new RowCol(0, 2);
            cell3 = new RowCol(1, 0);
            cell4 = new RowCol(1, 1);
            cell5 = new RowCol(1, 2);
            cell6 = new RowCol(2, 0);
            cell7 = new RowCol(2, 1);
            cell8 = new RowCol(2, 2);
            generation = new Generation
            {
                { cell0, false },
                { cell1, false },
                { cell2, false },
                { cell3, false },
                { cell4, true },
                { cell5, false },
                { cell6, false },
                { cell7, false },
                { cell8, false }
            };

            expectedNextGen = new Generation
            {
                { cell0, false },
                { cell1, false },
                { cell2, false },
                { cell3, false },
                { cell4, false },
                { cell5, false },
                { cell6, false },
                { cell7, false },
                { cell8, false }
            };
            return new GenerationResolver(grid, new Rules(), generation);

        }

        [Test]
        public void CanBeCreated()
        {
            var grid = new Grid(1, 1);
            var generation = grid.CreateEmptyGeneration();

            var resolver = new GenerationResolver(grid, new Rules(), generation);
            Assert.NotNull(resolver);
        }

        [Test]
        public void ThrowsOnNullDependencies()
        {
            Assert.Throws<ArgumentNullException>(() => new GenerationResolver(null, new Rules(), new Generation()));
            Assert.Throws<ArgumentNullException>(() => new GenerationResolver(new Grid(), null, new Generation()));
        }

        [Test]
        public void ThrowsOnGridAndGenerationSizeMismatch()
        {
            var grid = new Grid(2, 2);
            var generation = new Generation { { new RowCol(0, 0), false } };

            Assert.Throws<ArgumentOutOfRangeException>(() => new GenerationResolver(grid, new Rules(), generation));
        }

        [Test]
        public void NextGenResolutionCanBeRequested()
        {
            var resolver = CreateBasicResolver();
            Assert.AreEqual(resolver.CurrentGeneration, generation);
            Assert.IsTrue(resolver.LivingGeneration);
            resolver.ResolveNextGen();
            Assert.AreEqual(resolver.CurrentGeneration, expectedNextGen);
            Assert.IsTrue(resolver.LivingGeneration);

            resolver = CreateExpiringResolver();
            Assert.AreEqual(resolver.CurrentGeneration, generation);
            Assert.IsTrue(resolver.LivingGeneration);
            resolver.ResolveNextGen();
            Assert.AreEqual(resolver.CurrentGeneration, expectedNextGen);
            Assert.IsFalse(resolver.LivingGeneration);
        }

        [Test]
        public void NextGenResolutionRaisesGenerationResolved()
        {
            var eventRaised = false;
            var resolver = CreateBasicResolver();
            resolver.OnGenerationResolved += (sender, e) =>
            {
                eventRaised = true;
                Assert.IsInstanceOf<GenerationResolver>(sender);
                Assert.AreEqual(expectedNextGen, e.Generation);
            };
            resolver.ResolveNextGen();
            Assert.IsTrue(eventRaised);
        }

        [Test]
        public void CellNeighborCount()
        {
            var resolver = CreateBasicResolver();

            Assert.AreEqual(2, resolver.NeighborsCount(cell0));
            Assert.AreEqual(3, resolver.NeighborsCount(cell1));
            Assert.AreEqual(3, resolver.NeighborsCount(cell2));
            Assert.AreEqual(3, resolver.NeighborsCount(cell3));
            Assert.AreEqual(2, resolver.NeighborsCount(cell4));
            Assert.AreEqual(3, resolver.NeighborsCount(cell5));
            Assert.AreEqual(3, resolver.NeighborsCount(cell6));
            Assert.AreEqual(3, resolver.NeighborsCount(cell7));
            Assert.AreEqual(2, resolver.NeighborsCount(cell8));
        }

        [Test]
        public void NextGeneration()
        {
            var resolver = CreateBasicResolver();
            var nextGen = resolver.NextGen(generation);

            Assert.IsTrue(nextGen[cell0]);
            Assert.IsTrue(nextGen[cell1]);
            Assert.IsTrue(nextGen[cell2]);
            Assert.IsTrue(nextGen[cell3]);
            Assert.IsTrue(nextGen[cell4]);
            Assert.IsTrue(nextGen[cell5]);
            Assert.IsTrue(nextGen[cell6]);
            Assert.IsTrue(nextGen[cell7]);
            Assert.IsTrue(nextGen[cell8]);
        }

        [Test]
        public void CellAliveNextGen()
        {
            var grid = new Grid(1, 1);
            var generation = grid.CreateEmptyGeneration();

            var resolver = new GenerationResolver(grid, new Rules(), generation);

            Assert.IsFalse(resolver.CellAliveNextGen(alive: true, neighborCount: 1));
            Assert.IsTrue(resolver.CellAliveNextGen(alive: true, neighborCount: 2));
            Assert.IsTrue(resolver.CellAliveNextGen(alive: true, neighborCount: 3));
            Assert.IsFalse(resolver.CellAliveNextGen(alive: true, neighborCount: 4));

            Assert.IsFalse(resolver.CellAliveNextGen(alive: false, neighborCount: 2));
            Assert.IsTrue(resolver.CellAliveNextGen(alive: false, neighborCount: 3));
            Assert.IsFalse(resolver.CellAliveNextGen(alive: false, neighborCount: 4));
        }

        [Test]
        public void CellAliveNextGenRespectsCustomRules()
        {
            var surviveCounts = new List<int> { 2 };
            var birthCounts = new List<int> { 4 };
            var grid = new Grid(1, 1);
            var generation = grid.CreateEmptyGeneration();

            var resolver = new GenerationResolver(grid, new Rules(surviveCounts, birthCounts), generation);

            Assert.IsFalse(resolver.CellAliveNextGen(alive: true, neighborCount: 1));
            Assert.IsTrue(resolver.CellAliveNextGen(alive: true, neighborCount: 2));
            Assert.IsFalse(resolver.CellAliveNextGen(alive: true, neighborCount: 3));

            Assert.IsFalse(resolver.CellAliveNextGen(alive: false, neighborCount: 3));
            Assert.IsTrue(resolver.CellAliveNextGen(alive: false, neighborCount: 4));
            Assert.IsFalse(resolver.CellAliveNextGen(alive: false, neighborCount: 5));
        }
    }
}