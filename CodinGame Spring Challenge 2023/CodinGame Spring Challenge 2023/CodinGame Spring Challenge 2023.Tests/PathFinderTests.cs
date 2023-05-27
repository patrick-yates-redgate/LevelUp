using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace CodinGame_Spring_Challenge_2023.Tests
{
    [TestFixture]
    public class PathFinderTests
    {
        private Cell BuildCell(params (int dir, int index)[] neighbours)
        {
            var cell = new Cell
            {
                CellType = CellType.Empty,
                Neighbours = new[] { -1, -1, -1, -1, -1, -1 }
            };

            foreach (var neighbour in neighbours)
            {
                cell.Neighbours[neighbour.dir] = neighbour.index;
            }

            return cell;
        }

        private GameState BuildGameState(params Cell[] cells) => new GameState
        {
            NumberOfCells = cells.Count(),
            Cells = cells.ToList()
        };

        [Test]
        public void ThatForAdjacentCells_WeCanCalculateTheDistance()
        {
            var cell0 = BuildCell((0, 1));
            var cell1 = BuildCell((3, 0));
            var gameState = BuildGameState(cell0, cell1);
            var pathFinder = new PathFinder(gameState);
            pathFinder.Distance(0, 1).Should().Be(1);
        }

        [Test]
        public void ThatForASimpleChain_WeInitiallyDontKnowHowToGetFrom0To2()
        {
            var cell0 = BuildCell((0, 1));
            var cell1 = BuildCell((3, 0), (0, 2));
            var cell2 = BuildCell((3, 1));
            var gameState = BuildGameState(cell0, cell1, cell2);
            var pathFinder = new PathFinder(gameState);
            pathFinder.Distance(0, 2).Should().Be(-1);
        }

        [Test]
        public void ThatForASimpleChain_AfterWeExpandPathKnowledgeOnceWeShouldKnowHowToGetTo2()
        {
            var cell0 = BuildCell((0, 1));
            var cell1 = BuildCell((3, 0), (0, 2));
            var cell2 = BuildCell((3, 1));
            var gameState = BuildGameState(cell0, cell1, cell2);
            var pathFinder = new PathFinder(gameState);
            pathFinder.ExpandPathKnowledge();
            pathFinder.Distance(0, 2).Should().Be(2);
        }
    }
}