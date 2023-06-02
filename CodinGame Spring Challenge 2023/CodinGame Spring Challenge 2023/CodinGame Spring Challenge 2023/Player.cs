using System;
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

        var startTime = DateTime.UtcNow;
        var pathFinder = new PathFinder(gameState);
        var shortestTree = new ShortestTree(pathFinder);
        var mapInfo = new MapInfo(gameState, pathFinder);
        var strategyCostBenefitAdaptive = new StrategyCostBenefitAdaptive(gameState, pathFinder, shortestTree);
        var strategyAssessWinConditions = new StrategyAssessWinConditions(gameState, pathFinder);

        pathFinder.OnPathExpansionComplete(() =>
        {
            Console.Error.WriteLine("OnPathExpansionComplete");
            Console.Error.WriteLine("DebugDistances: " + pathFinder.DebugDistances(gameState));
            Console.Error.WriteLine("DebugPaths: " + pathFinder.DebugPaths(gameState));
            GameStateReader.DetermineOwnership(gameState, pathFinder);
            mapInfo.UpdateStatic();
        });

        var maxLoops = 20;
        while (!pathFinder.ExpandPathKnowledge() && --maxLoops > 0)
        {
        }

        if (maxLoops == 0)
        {
            Console.Error.WriteLine("Could not complete path map, will update each frame but may not be optimal");
        }

        Console.Error.WriteLine($"[StartUp]Frame time: {(DateTime.UtcNow - startTime).TotalMilliseconds}ms");
        
        // game looping
        while (true)
        {
            GameStateReader.ReadStateUpdate(gameState);
            var frameStart = DateTime.UtcNow;
            
            Console.Error.WriteLine(
                $"Resources: {string.Join(",", gameState.Cells.ToList().Select((cell, index) => (cell.Resources, text: $"({index},{cell.Resources})")).Where(x => x.Resources > 0).Select(x => x.text))}");

            pathFinder.ExpandPathKnowledge();
            strategyAssessWinConditions.Update();

            var locations = strategyCostBenefitAdaptive.Update(strategyAssessWinConditions.ProjectedTurnsUntilGameEnds);
            foreach (var index in locations)
            {
                gameActions.Beacon(index, gameState.Cells[index].NumEnemyAnts > 0 ? 2 : 1);
            }

            gameActions.FlushMoves();

            var frameEndTime = DateTime.UtcNow;
            var frameTime = frameEndTime - frameStart;
            Console.Error.WriteLine($"[End]Frame time: {frameTime.TotalMilliseconds}ms");
        }
    }
}