using System;
using System.Linq;
using CodinGame_Spring_Challenge_2023.Core;

namespace CodinGame_Spring_Challenge_2023.Strategy;

public class StrategyAssessWinConditions
{
    private readonly GameState _gameState;
    private readonly int _startingCrystalResources;
    private int _numFrames;
    
    public int ProjectedTurnsUntilGameEnds { get; private set; }

    public StrategyAssessWinConditions(GameState gameState)
    {
        _gameState = gameState;
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
    }
}