﻿using Engine;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class LifeRunnerTests
    {
        [Test]
        public void CanBeCreated()
        {
            var grid = new Grid(1, 1);
            var generation = grid.CreateEmptyGeneration();

            var runner = new LifeRunner(grid, new Rules(), generation);
            Assert.NotNull(runner);
        }

        [Test]
        public void ThrowsOnNullDependencies()
        {
            Assert.Throws<ArgumentNullException>(() => new LifeRunner(null, new Rules(), new Generation()));
            Assert.Throws<ArgumentNullException>(() => new LifeRunner(new Grid(), null, new Generation()));
        }

        [Test]
        public void ThrowsOnGridAndGenerationSizeMismatch()
        {
            var grid = new Grid(2, 2);
            var generation = new Generation { { new RowColTuple(0, 0), false } };

            Assert.Throws<ArgumentOutOfRangeException>(() => new LifeRunner(grid, new Rules(), generation));
        }

        [Test]
        public void InitialGenerationDefaultsToEmptyGeneration()
        {
            var grid = new Grid(1, 1);
            Assert.DoesNotThrow(() => new LifeRunner(grid, new Rules()));
        }

        [Test]
        public void AccessToReadonlyCurrentGeneration()
        {
            var grid = new Grid(2, 2);
            var runner = new LifeRunner(grid, new Rules());
            var generation = runner.CurrentGeneration;
            Assert.NotNull(generation);
            Assert.AreEqual(generation.Count, grid.Cells.Count);
            Assert.IsInstanceOf<IReadOnlyDictionary<RowColTuple, bool>>(generation);
        }

        [Test]
        public void ExtinctionStatusIsAvailable()
        {
            var runner = new LifeRunner(new Grid(), new Rules());
            Assert.IsFalse(runner.LivingGeneration);
            Assert.IsTrue(runner.Extinction);

            var grid = new Grid(1, 1);
            var cell0 = new RowColTuple(0, 0);
            var generation = new Generation
            {
                { cell0, true },
            };

            runner = new LifeRunner(grid, new Rules(), generation);
            Assert.IsTrue(runner.LivingGeneration);
            Assert.IsFalse(runner.Extinction);
        }

        [Test]
        public void CellNeighborCount()
        {
            // XOO
            // OXO
            // OOX
            var grid = new Grid(3, 3);
            var cell0 = new RowColTuple(0, 0);
            var cell1 = new RowColTuple(0, 1);
            var cell2 = new RowColTuple(0, 2);
            var cell3 = new RowColTuple(1, 0);
            var cell4 = new RowColTuple(1, 1);
            var cell5 = new RowColTuple(1, 2);
            var cell6 = new RowColTuple(2, 0);
            var cell7 = new RowColTuple(2, 1);
            var cell8 = new RowColTuple(2, 2);
            var generation = new Generation
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

            var runner = new LifeRunner(grid, new Rules(), generation);

            Assert.AreEqual(2, runner.NeighborsCount(cell0));
            Assert.AreEqual(3, runner.NeighborsCount(cell1));
            Assert.AreEqual(3, runner.NeighborsCount(cell2));
            Assert.AreEqual(3, runner.NeighborsCount(cell3));
            Assert.AreEqual(2, runner.NeighborsCount(cell4));
            Assert.AreEqual(3, runner.NeighborsCount(cell5));
            Assert.AreEqual(3, runner.NeighborsCount(cell6));
            Assert.AreEqual(3, runner.NeighborsCount(cell7));
            Assert.AreEqual(2, runner.NeighborsCount(cell8));
        }

        [Test]
        public void NextGeneration()
        {
            // XOO
            // OXO
            // OOX
            var grid = new Grid(3, 3);
            var cell0 = new RowColTuple(0, 0);
            var cell1 = new RowColTuple(0, 1);
            var cell2 = new RowColTuple(0, 2);
            var cell3 = new RowColTuple(1, 0);
            var cell4 = new RowColTuple(1, 1);
            var cell5 = new RowColTuple(1, 2);
            var cell6 = new RowColTuple(2, 0);
            var cell7 = new RowColTuple(2, 1);
            var cell8 = new RowColTuple(2, 2);
            var generation = new Generation
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

            var runner = new LifeRunner(grid, new Rules(), generation);
            var nextGen = runner.NextGen(generation);

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

            var runner = new LifeRunner(grid, new Rules(), generation);

            Assert.IsFalse(runner.CellAliveNextGen(alive: true, neighborCount: 1));
            Assert.IsTrue(runner.CellAliveNextGen(alive: true, neighborCount: 2));
            Assert.IsTrue(runner.CellAliveNextGen(alive: true, neighborCount: 3));
            Assert.IsFalse(runner.CellAliveNextGen(alive: true, neighborCount: 4));

            Assert.IsFalse(runner.CellAliveNextGen(alive: false, neighborCount: 2));
            Assert.IsTrue(runner.CellAliveNextGen(alive: false, neighborCount: 3));
            Assert.IsFalse(runner.CellAliveNextGen(alive: false, neighborCount: 4));
        }

        [Test]
        public void CellAliveNextGenRespectsCustomRules()
        {
            var surviveCounts = new List<int> { 2 };
            var birthCounts = new List<int> { 4 };
            var grid = new Grid(1, 1);
            var generation = grid.CreateEmptyGeneration();

            var runner = new LifeRunner(grid, new Rules(surviveCounts, birthCounts), generation);

            Assert.IsFalse(runner.CellAliveNextGen(alive: true, neighborCount: 1));
            Assert.IsTrue(runner.CellAliveNextGen(alive: true, neighborCount: 2));
            Assert.IsFalse(runner.CellAliveNextGen(alive: true, neighborCount: 3));

            Assert.IsFalse(runner.CellAliveNextGen(alive: false, neighborCount: 3));
            Assert.IsTrue(runner.CellAliveNextGen(alive: false, neighborCount: 4));
            Assert.IsFalse(runner.CellAliveNextGen(alive: false, neighborCount: 5));
        }
    }
}