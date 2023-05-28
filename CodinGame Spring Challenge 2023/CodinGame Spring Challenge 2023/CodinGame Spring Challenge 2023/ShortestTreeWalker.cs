using System.Collections.Generic;
using System.Linq;

namespace CodinGame_Spring_Challenge_2023
{
    public static class ShortestTreeWalker
    {
        public static IEnumerable<int> WalkShortestTree(GameState gameState, PathFinder pathFinder,
            IEnumerable<(int fromIndex, int toIndex)> tree) => tree
            .SelectMany(x => pathFinder.PathTo(gameState, x.fromIndex, x.toIndex)).Distinct();
    }
}