using System;
using CodinGame_Spring_Challenge_2023.Core;
using CodinGame_Spring_Challenge_2023.PathFinding;
using CodinGame_Spring_Challenge_2023.Strategy;

namespace CodinGame_Spring_Challenge_2023;

public static class Player
{
    static void Main(string[] args)
    {
        var lastFrame = DateTime.UtcNow;
        
        var gameActions = new GameActions();
        var gameState = GameStateReader.ReadInitialState();
        var pathFinder = new PathFinder(gameState);
        var shortestTree = new ShortestTree(pathFinder);
        var mapInfo = new MapInfo(gameState, pathFinder);
        var strategyCostBenefitAdaptive = new StrategyCostBenefitAdaptive(gameState, pathFinder, shortestTree);
        var strategyAssessWinConditions = new StrategyAssessWinConditions(gameState);

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

        // game looping
        while (true)
        {
            GameStateReader.ReadStateUpdate(gameState);
            pathFinder.ExpandPathKnowledge();

            var locations = strategyCostBenefitAdaptive.Update(strategyAssessWinConditions.ProjectedTurnsUntilGameEnds);
            foreach (var index in locations)
            {
                gameActions.Beacon(index, 1);
            }

            strategyAssessWinConditions.Update();
            gameActions.FlushMoves();

            var frameEndTime = DateTime.UtcNow;
            var frameTime = frameEndTime - lastFrame;
            Console.Error.WriteLine($"Frame time: {frameTime.TotalMilliseconds}ms");
            lastFrame = frameEndTime;
        }
    }
}