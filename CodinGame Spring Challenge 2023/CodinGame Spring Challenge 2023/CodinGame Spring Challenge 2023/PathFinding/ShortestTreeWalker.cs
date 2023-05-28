using System.Collections.Generic;
using System.Linq;
using CodinGame_Spring_Challenge_2023.Core;

namespace CodinGame_Spring_Challenge_2023.PathFinding
{
    public static class ShortestTreeWalker
    {
        public static IEnumerable<int> WalkShortestTree(GameState gameState, PathFinder pathFinder,
            IEnumerable<(int fromIndex, int toIndex)> tree) => tree
            .SelectMany(x => pathFinder.PathTo(gameState, x.fromIndex, x.toIndex)).Distinct();
    }
}