using System;
using System.Collections.Generic;
using System.Linq;
using CodinGame_Spring_Challenge_2023.Core;
using CodinGame_Spring_Challenge_2023.PathFinding;
using CodinGame_Spring_Challenge_2023.Strategy;

namespace CodinGame_Spring_Challenge_2023;

public static class Player
{
    static void Main(string[] args)
    {
        var gameActions = new GameActions();
        var gameState = GameStateReader.ReadInitialState();
        var pathFinder = new PathFinder(gameState);
        var shortestTree = new ShortestTree(pathFinder);
        var mapInfo = new MapInfo(gameState, pathFinder);
        var strategyCostBenefitAdaptive = new StrategyCostBenefitAdaptive(gameState, pathFinder, mapInfo, shortestTree);
        
        pathFinder.OnPathExpansionComplete(() =>
        {
            GameStateReader.DetermineOwnership(gameState, pathFinder);
            mapInfo.UpdateStatic();
        });

        var maxLoops = 20;
        while (!pathFinder.ExpandPathKnowledge() && maxLoops-- > 0)
        {
        }

        if (maxLoops == 0)
        {
            Console.Error.WriteLine("Could not complete path map, will update each frame but may not be optimal");
        }

        List<int> currentlyVisiting = new List<int>();

        // game looping
        while (true)
        {
            GameStateReader.ReadStateUpdate(gameState);
            pathFinder.ExpandPathKnowledge();

            foreach (var index in strategyCostBenefitAdaptive.Update())
            {
                gameActions.Beacon(index, 1);
            }
            
            /*
            currentlyVisiting.RemoveAll(index => gameState.Cells[index].Resources == 0);
            
            if (currentlyVisiting.Count == 0)
            {
                currentlyVisiting = gameState.MyBaseLocations.ToList();
            }

            //TODO, build existing tree to build on
            var allpoints =
                ShortestTreeWalker.WalkShortestTree(gameState, pathFinder, shortestTree.GetShortestTree(locations));
            
            
            var eggsCloserThan3FromBase = gameState.EggLocations.Select(x => gameState.)
                
                
            var shortestTreeUsingHighestValueMyCrystalAndClosestEggLessThan3FromBase =
                new int[] { gameState.MyCrystalLocations.Max(x => gameState.Cells[x].Resources) }



            var eggs = pathFinder.ClosestNOf(gameState, 1, gameState.MyEggLocations, gameState.ContestedEggLocations)
                .ToList();
            var crystals = pathFinder.ClosestNOf(gameState, 1, gameState.ContestedCrystalLocations,
                gameState.EnemyCrystalLocations);

            var locations = new[]
            {
                gameState.MyBaseLocations,
                gameState.MyCrystalLocations,
                crystals,
                eggs
            }.SelectMany(x => x).ToList();

            Console.Error.WriteLine(string.Join(",", locations.Select(x => x.ToString())));
*/

            // MUST TRIM THE NODES BEFORE WE PASS IN HERE, NOT AFTER

            /*
        shortestTree.GetShortestTree(locations)
            .ToList()
            .ForEach(x =>
            {
                var dist = pathFinder.Distance(x.fromIndex, x.toIndex);
                var isEgg = eggs.Contains(x.toIndex) || eggs.Contains(x.fromIndex);
                if (isEgg)
                {
                    var strength = 3 - dist;
                    if (strength > 0)
                    {
                        gameActions.Line(x.fromIndex, x.toIndex, strength * 10);
                    }

                    return;
                }

                var isMyCrystal = gameState.MyCrystalLocations.Contains(x.toIndex) ||
                                  gameState.MyCrystalLocations.Contains(x.fromIndex);
                gameActions.Line(x.fromIndex, x.toIndex, isMyCrystal ? 6 : 2);
            });
            */

            gameActions.FlushMoves();
        }
    }
}