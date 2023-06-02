using System;
using System.Linq;
using CodinGame_Spring_Challenge_2023.Core;
using CodinGame_Spring_Challenge_2023.PathFinding;

namespace CodinGame_Spring_Challenge_2023.Strategy;

public class StrategyAssessWinConditions
{
    private readonly GameState _gameState;
    private readonly PathFinder _pathFinder;
    private readonly int _startingCrystalResources;
    private int _numFrames;

    public int ProjectedTurnsUntilGameEnds { get; private set; }

    public StrategyAssessWinConditions(GameState gameState, PathFinder pathFinder)
    {
        _gameState = gameState;
        _pathFinder = pathFinder;
        _startingCrystalResources = _gameState.CrystalLocations.Sum(x => _gameState.Cells[x].Resources);
        ProjectedTurnsUntilGameEnds = 100;
        _numFrames = 0;
    }

    public void Update()
    {
        ++_numFrames;

        var remainingCrystalResources = (float)_gameState.CrystalLocations.Sum(x => _gameState.Cells[x].Resources);
        var consumedCrystalResources = _startingCrystalResources - remainingCrystalResources;
        consumedCrystalResources = Math.Max(1f, consumedCrystalResources);

        var consumptionRate = consumedCrystalResources / _numFrames;

        ProjectedTurnsUntilGameEnds =
            (int)Math.Max(2f, Math.Min(100f, 0.5f * remainingCrystalResources / consumptionRate));

        GenerateCrystalVsEggStrategy();
    }

    private void GenerateCrystalVsEggStrategy()
    {
        var totalEggs = _gameState.EggLocations.Sum(index => _gameState.Cells[index].Resources);
        var totalCrystals = _gameState.CrystalLocations.Sum(index => _gameState.Cells[index].Resources);
        var averageEggDist = (float) _gameState.EggLocations
            .Select(index => _pathFinder.ClosestOrDefault(index, _gameState.MyBaseLocations))
            .Where(x => x.IsValue1)
            .Average(x => x.Value1.dist);
        var averageCrystalDist = (float) _gameState.CrystalLocations
            .Select(index => _pathFinder.ClosestOrDefault(index, _gameState.MyBaseLocations))
            .Where(x => x.IsValue1)
            .Average(x => x.Value1.dist);

        var target = 1 + totalCrystals / 2;

        float CountFramesToWinFocusingOnEggs(float frameCount, float remainingTarget, float numAnts, float remainingEggs)
        {
            while (true)
            {
                if (remainingTarget <= 0) return frameCount;

                var eggsCollected = numAnts / averageEggDist;
                if (eggsCollected >= remainingEggs)
                {
                    var newNumAnts = numAnts + remainingEggs;
                    var crystalsCollectedPerFrame = newNumAnts / averageEggDist;
                    return frameCount + remainingTarget / crystalsCollectedPerFrame;
                }

                frameCount = frameCount + 1;
                numAnts = numAnts + eggsCollected;
                remainingEggs = remainingEggs - eggsCollected;
            }
        }
        
        var framesToWinFocusingOnEggs = CountFramesToWinFocusingOnEggs(0, target, _gameState.MyAntCount, totalEggs);
        var framesToWinFocusingOnCrystals = target / (_gameState.MyAntCount / averageCrystalDist);
        
        if (framesToWinFocusingOnEggs < framesToWinFocusingOnCrystals)
        {
            _gameState.Strategy = StrategyType.CollectEggs;
            _gameState.StrategyStrength = framesToWinFocusingOnCrystals / (framesToWinFocusingOnEggs + framesToWinFocusingOnCrystals);
            Console.Error.WriteLine($"Strategy: CollectEggs ({_gameState.StrategyStrength})");
        }
        else
        {
            _gameState.Strategy = StrategyType.CollectCrystals;
            _gameState.StrategyStrength = framesToWinFocusingOnEggs / (framesToWinFocusingOnEggs + framesToWinFocusingOnCrystals);
            Console.Error.WriteLine($"Strategy: CollectCrystals ({_gameState.StrategyStrength})");
        }
    }

    private float Clamp(float minValue, float maxValue, float value) => Math.Max(minValue, Math.Min(maxValue, value));
}