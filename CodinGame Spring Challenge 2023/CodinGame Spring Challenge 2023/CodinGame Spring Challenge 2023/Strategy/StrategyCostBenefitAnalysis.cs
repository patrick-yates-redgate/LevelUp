using System.Collections.Generic;
using CodinGame_Spring_Challenge_2023.Core;
using CodinGame_Spring_Challenge_2023.Domain;
using CodinGame_Spring_Challenge_2023.PathFinding;

namespace CodinGame_Spring_Challenge_2023.Strategy;

public class StrategyCostBenefitAnalysis
{
    private GameState _gameState;
    private PathFinder _pathFinder;
    private MapInfo _mapInfo;

    public StrategyCostBenefitAnalysis(GameState gameState, PathFinder pathFinder, MapInfo mapInfo)
    {
        _gameState = gameState;
        _pathFinder = pathFinder;
        _mapInfo = mapInfo;
    }

    public void CostBenefitAnalysis()
    {
    }

    public IEnumerable<int> CreateStrategy()
    {
        //WFC of all possible states? We consider if we add each one and the cost/benefit.
        //AStar search maybe or all possible states and then we pick the best one?
        //Each addition takes away from the rest, so we look to find optimal
        
        
        
        var totalAnts = _gameState.MyAntCount;

        var locationsIncludedInStrategy = new HashSet<int>();
        var projectedEggCollection = 0;
        var projectedCrystalCollection = 0;

        var orderedLocations = _pathFinder.OrderedPairs(
            _gameState,
            _gameState.MyBaseLocations,
            (_gameState.EggLocations, CellType.Eggs),
            (_gameState.CrystalLocations, CellType.Crystal));

        foreach (var (fromIndex, toIndex, dist, cellType) in orderedLocations)
        {
            switch (cellType)
            {
                case CellType.Crystal:
                {
                    
                    break;
                }

                case CellType.Eggs:
                {
                    
                    
                    break;
                }
            }
        }

        return _gameState.MyBaseLocations;
    }
}