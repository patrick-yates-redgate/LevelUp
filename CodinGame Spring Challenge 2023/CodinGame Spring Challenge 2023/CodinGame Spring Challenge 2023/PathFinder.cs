using System.Collections.Generic;
using System.Linq;

public class Path
{
    public List<int> Steps { get; set; }
    public int Distance => Steps.Count + 1;
}

public class PathFinder
{
    private GameState _gameState;

    private List<Dictionary<int, (int dir, int dist)>> _cellPathMap = new List<Dictionary<int, (int dir, int dist)>>();

    private bool fullyExpanded = false;

    public PathFinder(GameState gameState)
    {
        _gameState = gameState;

        BuildPathInfo();
    }

    public void ExpandPathKnowledge()
    {
        if (fullyExpanded) return;

        var newPathsFound = new List<(int from, int fromDir, int to, int toDir, int dist)>();

        for (var i = 0; i < _gameState.NumberOfCells; ++i)
        {
            var pathsForCell = _cellPathMap[i];

            foreach (var knownCellIndex in pathsForCell.Keys)
            {
                var pathToKnownCell = pathsForCell[knownCellIndex];
                var pathsForKnownCell = _cellPathMap[knownCellIndex];
                foreach (var otherCellIndex in pathsForKnownCell.Keys)
                {
                    if (i == otherCellIndex) continue;

                    var possibleDistViaKnownCell = pathToKnownCell.dist + pathsForKnownCell[otherCellIndex].dist;

                    if (pathsForCell.TryGetValue(otherCellIndex, out var myCurrentBestPathToOtherCell))
                    {
                        if (myCurrentBestPathToOtherCell.dist < possibleDistViaKnownCell)
                        {
                            continue;
                        }
                    }

                    //We are new here or simply have a better path!
                    var pathsForOtherCell = _cellPathMap[otherCellIndex];
                    newPathsFound.Add((i, pathToKnownCell.dir, otherCellIndex, pathsForOtherCell[knownCellIndex].dir,
                        possibleDistViaKnownCell));
                }
            }
        }

        if (newPathsFound.Count == 0)
        {
            fullyExpanded = true;
            return;
        }

        foreach (var newPath in newPathsFound)
        {
            _cellPathMap[newPath.from][newPath.to] = (newPath.fromDir, newPath.dist);
            _cellPathMap[newPath.to][newPath.from] = (newPath.toDir, newPath.dist);
        }
    }


    private void BuildPathInfo()
    {
        for (var i = 0; i < _gameState.NumberOfCells; ++i)
        {
            var pathsForCell = new Dictionary<int, (int dir, int dist)>();
            _cellPathMap.Add(pathsForCell);

            var cell = _gameState.Cells[i];
            for (var dir = 0; dir < 6; ++dir)
            {
                var neighbourIndex = cell.Neighbours[dir];
                if (neighbourIndex == -1) continue;

                pathsForCell.Add(neighbourIndex, (dir, 1));
            }
        }
    }
    /*
    for (var i = 0; i < _gameState.NumberOfCells; ++i)
        {
            var cell = _gameState.Cells[i];
            var pathsForCell = _cellPathMap[i];

            foreach (var pathToIndex in pathsForCell.Keys)
            {
                var knownCell = _gameState.Cells[pathToIndex];
                var pathsForKnownCell = _cellPathMap[pathToIndex];
                foreach (var otherPathToIndex in pathsForCell.Keys)
                {
                    if (pathToIndex == otherPathToIndex) continue;

                    if (!pathsForKnownCell.ContainsKey(otherPathToIndex))
                    {
                        pathsForKnownCell[otherPathToIndex] = pathsForCell[pathToIndex].
                    }   
                }
            }
        }
    }
    */

    public IOrderedEnumerable<(int index, int dist)> ClosestDistances(int fromIndex, IEnumerable<int> toIndexList) =>
        toIndexList.Select(index => (index: index, dist: Distance(fromIndex, index))).Where((_, dist) => dist > -1)
            .OrderBy(x => x.dist);

    public (int index, int dist) ClosestOrDefault(int fromIndex, IEnumerable<int> toIndexList)
    {
        var closestDistances = ClosestDistances(fromIndex, toIndexList);
        if (closestDistances.Any())
        {
            return closestDistances.First();
        }
        
        return (-1, -1);
    }
    
    public int Distance(int fromIndex, int toIndex)
    {
        if (_cellPathMap[fromIndex].TryGetValue(toIndex, out var path))
        {
            return path.dist;
        }

        return -1;
    }
    
    public IEnumerable<int> PathTo(int fromIndex, int toIndex)
    {
        if (!_cellPathMap[fromIndex].ContainsKey(toIndex))
        {
            return Enumerable.Empty<int>();
        }
        
        var path = new List<int>();
        var currentCellIndex = fromIndex;
        while (currentCellIndex != toIndex)
        {
            path.Add(currentCellIndex);
            var nextCellDir = _cellPathMap[currentCellIndex][toIndex].dir;
            currentCellIndex = _gameState.Cells[currentCellIndex].Neighbours[nextCellDir];
        }

        path.Add(toIndex);
        return path;
    }

    public string DebugDistances(GameState gameState)
    {
        var output = "Distances from my base: ";
        foreach (var myBase in gameState.MyBases)
        {
            var crystalDistances = ClosestDistances(myBase.CellIndex, gameState.CrystalLocations);
            foreach (var crystal in crystalDistances)
            {
                output += $"({crystal.index}, {crystal.dist}) ";
            }
        }
        
        output += " Distances from enemy base: ";
        foreach (var enemyBase in gameState.EnemyBases)
        {
            var crystalDistances = ClosestDistances(enemyBase.CellIndex, gameState.CrystalLocations);
            foreach (var crystal in crystalDistances)
            {
                output += $"({crystal.index}, {crystal.dist}) ";
            }
        }

        return output;
    }

    public string DebugPaths(GameState gameState)
    {
        var output = "Paths from my base: ";
        foreach (var myBase in gameState.MyBases)
        {
            foreach (var crystal in gameState.CrystalLocations)
            {
                var path = PathTo(myBase.CellIndex, crystal);
                output += $"(Base({myBase.CellIndex}) -> Crystal({crystal}) : ";
                foreach (var step in path)
                {
                    output += step + " ";
                }
            }
        }
        
        output += " Paths from enemy base: ";
        foreach (var enemyBase in gameState.EnemyBases)
        {
            foreach (var crystal in gameState.CrystalLocations)
            {
                var path = PathTo(enemyBase.CellIndex, crystal);
                output += $"(Base({enemyBase.CellIndex}) -> Crystal({crystal}) : ";
                foreach (var step in path)
                {
                    output += step + " ";
                }
            }
        }

        return output;
    }
}