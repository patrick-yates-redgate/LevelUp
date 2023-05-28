using System;
using System.Linq;
using CodinGame_Spring_Challenge_2023;

public static class Player
{
    static void Main(string[] args)
    {
        var gameActions = new GameActions();
        var gameState = GameStateReader.ReadInitialState();
        var pathFinder = new PathFinder(gameState);
        pathFinder.OnPathExpansionComplete(() => GameStateReader.DetermineOwnership(gameState, pathFinder));
        var shortestTree = new ShortestTree(pathFinder);

        var maxLoops = 20;
        while (!pathFinder.ExpandPathKnowledge() && maxLoops-- > 0)
        {
        }

        if (maxLoops == 0)
        {
            Console.Error.WriteLine("Could not complete path map, will update each frame but may not be optimal");
        }

        // game looping
        while (true)
        {
            GameStateReader.ReadStateUpdate(gameState);
            pathFinder.ExpandPathKnowledge();

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

            foreach (var index in ShortestTreeWalker.WalkShortestTree(gameState, pathFinder, shortestTree.GetShortestTree(locations)))
            {
                gameActions.Beacon(index, 1);
            }

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