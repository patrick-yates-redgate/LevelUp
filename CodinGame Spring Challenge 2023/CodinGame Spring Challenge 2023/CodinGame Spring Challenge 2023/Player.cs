using System;
using System.Linq;

public static class Player
{
    static void Main(string[] args)
    {
        var gameActions = new GameActions();
        var gameStateReader = new GameStateReader();
        var gameState = gameStateReader.ReadInitialState();
        var pathFinder = new PathFinder(gameState);

        // game looping
        while (true)
        {
            gameStateReader.ReadStateUpdate(gameState);

            var hasMoves = false;
            
            foreach (var myBase in gameState.MyBases)
            {
                var bestCrystals = gameState.CrystalLocations
                    .Select(crystalIndex => (crystalIndex, dist: pathFinder.Distance(myBase.CellIndex, crystalIndex)))
                    .Where(x => x.dist > -1)
                    .OrderBy(x => x.dist);

                if (bestCrystals.Count() > 0)
                {
                    gameActions.Line(myBase.CellIndex, bestCrystals.First().crystalIndex, 1);
                    hasMoves = true;
                }
            }
             
            if (!hasMoves)
            {
                gameActions.Wait();
            }

            // Write an action us Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");
            gameActions.FlushMoves();
        }
    }
}