using System.Collections.Generic;
using System.Linq;
using CodinGame_Spring_Challenge_2023.Core;

namespace CodinGame_Spring_Challenge_2023.PathFinding;

public class MapInfo
{
    private GameState _gameState;
    private PathFinder _pathFinder;

    public List<CellMapInfoDynamic> CellMapInfoDynamic { get; } = new();
    public List<CellMapInfoStatic> CellMapInfoStatic { get; } = new();

    public MapInfo(GameState gameState, PathFinder pathFinder)
    {
        _gameState = gameState;
        _pathFinder = pathFinder;
    }
    
    public void UpdateStatic()
    {
        for (var i = 0; i < _gameState.NumberOfCells; ++i)
        {
            var cellMapInfoStatic = new CellMapInfoStatic
            {
                MyClosestBase = -1,
                EnemyClosestBase = -1
            };

            var closestDistances = _pathFinder.ClosestDistances(i, _gameState.MyBaseLocations);
            if (closestDistances.Any())
            {
                cellMapInfoStatic.MyClosestBase = closestDistances.First().index;
            }

            closestDistances = _pathFinder.ClosestDistances(i, _gameState.EnemyBaseLocations);
            if (closestDistances.Any())
            {
                cellMapInfoStatic.EnemyClosestBase = closestDistances.First().index;
            }
            
            CellMapInfoStatic.Add(cellMapInfoStatic);
        }
    }
    
    public void UpdateDynamic()
    {
        
    }
}

public record CellMapInfoStatic
{
    public int MyClosestBase;
    public int EnemyClosestBase;
}

public record CellMapInfoDynamic
{
    
}