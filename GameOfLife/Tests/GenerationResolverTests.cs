using Engine;
using NUnit.Framework;
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

        public void SetupBasicResolver()
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
        }

        public void SetupExpiringResolver()
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
        }

        [Test]
        public void NextGenResolutionCanBeRequested()
        {
            SetupBasicResolver();
            var nextGen = GenerationResolver.ResolveNextGeneration(grid, new Rules(), generation);
            Assert.AreEqual(expectedNextGen, nextGen);

            SetupExpiringResolver();
            nextGen = GenerationResolver.ResolveNextGeneration(grid, new Rules(), generation);
            Assert.AreEqual(expectedNextGen, nextGen);
        }

        [Test]
        public void CellNeighborCount()
        {
            SetupBasicResolver();
            Assert.AreEqual(2, GenerationResolver.NeighborsCount(cell0, grid, generation));
            Assert.AreEqual(3, GenerationResolver.NeighborsCount(cell1, grid, generation));
            Assert.AreEqual(3, GenerationResolver.NeighborsCount(cell2, grid, generation));
            Assert.AreEqual(3, GenerationResolver.NeighborsCount(cell3, grid, generation));
            Assert.AreEqual(2, GenerationResolver.NeighborsCount(cell4, grid, generation));
            Assert.AreEqual(3, GenerationResolver.NeighborsCount(cell5, grid, generation));
            Assert.AreEqual(3, GenerationResolver.NeighborsCount(cell6, grid, generation));
            Assert.AreEqual(3, GenerationResolver.NeighborsCount(cell7, grid, generation));
            Assert.AreEqual(2, GenerationResolver.NeighborsCount(cell8, grid, generation));
        }

        [Test]
        public void CellAliveNextGen()
        {
            var rules = new Rules();

            Assert.IsFalse(GenerationResolver.CellAliveNextGen(true, 1, rules));
            Assert.IsTrue(GenerationResolver.CellAliveNextGen (true, 2, rules));
            Assert.IsTrue(GenerationResolver.CellAliveNextGen (true, 3, rules));
            Assert.IsFalse(GenerationResolver.CellAliveNextGen(true, 4, rules));

            Assert.IsFalse(GenerationResolver.CellAliveNextGen(false, 2, rules));
            Assert.IsTrue(GenerationResolver.CellAliveNextGen (false, 3, rules));
            Assert.IsFalse(GenerationResolver.CellAliveNextGen(false, 4, rules));
        }

        [Test]
        public void CellAliveNextGenRespectsCustomRules()
        {
            var surviveCounts = new List<int> { 2 };
            var birthCounts = new List<int> { 4 };
            var rules = new Rules(surviveCounts, birthCounts);

            Assert.IsFalse(GenerationResolver.CellAliveNextGen(true, 1, rules));
            Assert.IsTrue(GenerationResolver. CellAliveNextGen(true, 2, rules));
            Assert.IsFalse(GenerationResolver.CellAliveNextGen(true, 3, rules));

            Assert.IsFalse(GenerationResolver.CellAliveNextGen(false, 3, rules));
            Assert.IsTrue(GenerationResolver. CellAliveNextGen(false, 4, rules));
            Assert.IsFalse(GenerationResolver.CellAliveNextGen(false, 5, rules));
        }
    }
}