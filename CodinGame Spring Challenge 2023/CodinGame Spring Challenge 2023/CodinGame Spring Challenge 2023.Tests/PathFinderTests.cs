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

        /*
            |0|
         |1|2|3|
        |4|5|6|
        
        Distance from 3 to 4 is 3 but we are seeing 4 in the current code
        */
        [Test]
        public void ThatASpecificScenarioWithMultipleRoutesIsCalculatedCorrectly()
        {
            var gameState = BuildGameState(
                BuildCell((4, 2), (5, 3)),
                BuildCell((0, 2), (4, 4), (5, 5)),
                BuildCell((0, 3), (1, 0), (3, 1), (4, 5), (5, 6)),
                BuildCell((2, 0), (3, 2), (4, 6)),
                BuildCell((0, 5), (1, 1)),
                BuildCell((0, 6), (1, 2), (2, 1), (3, 4)),
                BuildCell((1, 3), (2, 2), (3, 5))
                );
            
            var pathFinder = new PathFinder(gameState);
            pathFinder.ExpandPathKnowledge();
            pathFinder.ExpandPathKnowledge();
            pathFinder.ExpandPathKnowledge();
            pathFinder.Distance(3, 4).Should().Be(3);
        }

        /*
         *          0
         *        1   2
         *
         * Commented out, this shouldn't be a valid scenario seeing as the base state establishes direct neighbours
        [Test]
        public void ThatIfWeHaveAnInefficientPathThenWeWillCorrectThroughExpansion()
        {
            var cellPathMap = new List<Dictionary<int, (int dir, int dist)>> {
                new Dictionary<int, (int dir, int dist)>
                {
                    [1] = (4, 1),
                    [2] = (4, 2) //Inefficient, it should go direct to 2 in dir 5
                },
                new Dictionary<int, (int dir, int dist)>
                {
                    [0] = (1, 1),
                    [2] = (0, 1)
                },
                new Dictionary<int, (int dir, int dist)>
                {
                    [0] = (2, 1),
                    [1] = (3, 1)
                }
            };

            var pathFinder = new PathFinder(cellPathMap, 3);
            
            pathFinder.Distance(0, 2).Should().Be(2); //Initial state
            pathFinder.ExpandPathKnowledge();
            pathFinder.Distance(0, 2).Should().Be(1); //After expansion
        }
         */
        
        
        /*
         *          0   1
         *        2   3   4
         */
        [Test]
        public void ThatIfWeHaveAnInefficientPathThenWeWillCorrectThroughExpansion()
        {
            var cellPathMap = new List<Dictionary<int, (int dir, int dist)>> {
                new Dictionary<int, (int dir, int dist)>
                {
                    [1] = (0, 1),
                    [2] = (4, 1),
                    [3] = (5, 1),
                    [4] = (1, 2)
                },
                new Dictionary<int, (int dir, int dist)>
                {
                    [0] = (3, 1),
                    [3] = (4, 1),
                    [4] = (5, 1),
                    [2] = (3, 2)
                },
                new Dictionary<int, (int dir, int dist)>
                {
                    [0] = (1, 1),
                    [3] = (0, 1),
                    [1] = (1, 2),
                    [4] = (1, 3) //Inefficient, it should go direct to 2 in dir 5 (dist 2)
                },
                new Dictionary<int, (int dir, int dist)>
                {
                    [0] = (2, 1),
                    [1] = (1, 1),
                    [2] = (3, 1),
                    [4] = (0, 1)
                },
                new Dictionary<int, (int dir, int dist)>
                {
                    [1] = (2, 2),
                    [3] = (3, 3),
                    [0] = (2, 2),
                    [2] = (2, 3) //Inefficient, it should go direct to 2 in dir 3 (dist 2)
                }
            };

            var pathFinder = new PathFinder(cellPathMap, 5);
            
            //Initial state
            pathFinder.Distance(2, 4).Should().Be(3);
            pathFinder.Distance(4, 2).Should().Be(3);
            
            pathFinder.ExpandPathKnowledge();
            
            //After expansion
            pathFinder.Distance(2, 4).Should().Be(2); //Initial state
            pathFinder.Distance(4, 2).Should().Be(2); //Initial state
        }
    }
}