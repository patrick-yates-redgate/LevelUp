using System.Collections.Generic;
using System.Linq;

namespace CodinGame_Spring_Challenge_2023.PathFinding
{
    public class ShortestTree
    {
        private PathFinder _pathFinder;
        
        public ShortestTree(PathFinder pathFinder)
        {
            _pathFinder = pathFinder;
        }
        
        public IEnumerable<(int fromIndex, int toIndex)> GetShortestTree(IEnumerable<int> includeLocations)
        {
            var shortestTree = new List<(int fromIndex, int toIndex)>();
            
            var visited = new HashSet<int>();
            var unvisited = new HashSet<int>();
            foreach (var index in includeLocations)
            {
                unvisited.Add(index);
            }
            
            var currentCellIndex = unvisited.First();
            visited.Add(currentCellIndex);
            unvisited.Remove(currentCellIndex);
            
            while (unvisited.Count > 0)
            {
                var shortestPath = (fromIndex: -1, toIndex: -1, dist: int.MaxValue);
                foreach (var visitedCellIndex in visited)
                {
                    foreach (var unvisitedCellIndex in unvisited)
                    {
                        var dist = _pathFinder.Distance(unvisitedCellIndex, visitedCellIndex);
                        if (dist < 0) continue;
                        
                        if (dist < shortestPath.dist)
                        {
                            shortestPath = (visitedCellIndex, unvisitedCellIndex, dist);
                        }
                    }
                }
                
                shortestTree.Add((shortestPath.fromIndex, shortestPath.toIndex));
                
                visited.Add(shortestPath.toIndex);
                unvisited.Remove(shortestPath.toIndex);
            }
            
            return shortestTree;
        }
    }
}