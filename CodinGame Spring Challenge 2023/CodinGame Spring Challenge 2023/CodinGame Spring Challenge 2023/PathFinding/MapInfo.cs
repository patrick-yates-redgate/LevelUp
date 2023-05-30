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
        if (CellMapInfoStatic.Count > 0) return;
        
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
            CellMapInfoDynamic.Add(new CellMapInfoDynamic());
        }
    }
    
    public void UpdateDynamic()
    {
        for (var i = 0; i < _gameState.NumberOfCells; ++i)
        {
            CellMapInfoDynamic[i].Resources = _gameState.Cells[i].Resources;
        }
    }
    
    public (CellMapInfoStatic staticInfo, CellMapInfoDynamic dynamicInfo) GetCellInfo(int cellIndex)
    {
        return (CellMapInfoStatic[cellIndex], CellMapInfoDynamic[cellIndex]);
    }
}

public record CellMapInfoStatic
{
    public int MyClosestBase;
    public int EnemyClosestBase;
}

public record CellMapInfoDynamic
{
    public int Resources;
}