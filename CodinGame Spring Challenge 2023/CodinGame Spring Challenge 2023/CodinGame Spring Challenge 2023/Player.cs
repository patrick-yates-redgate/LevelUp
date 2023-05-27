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
            var myClosestWeight = 0.7;
            var enemyClosestWeight = 0.3;
            var strengthToUseForMyCrystals = 2;
            var strengthToUseForEnemyCrystals = 1;

            var myClosestCrystals = gameState.CrystalLocations
                .Where(x => gameState.Cells[x].Resources > 0)
                .Select(crystalIndex => (crystalIndex,
                    distForMe: pathFinder.ClosestOrDefault(crystalIndex, gameState.MyBases),
                    distForEnemy: pathFinder.ClosestOrDefault(crystalIndex, gameState.EnemyBases)))
                .Where(x => x.distForMe.index != -1)
                .Select(
                    x => (x.crystalIndex,
                        baseIndex: x.distForMe.index,
                        closerToEnemy: x.distForEnemy.index > -1 && x.distForEnemy.dist < x.distForMe.dist,
                        weightedDist: x.distForMe.dist * myClosestWeight + (x.distForEnemy.index > -1 ? 100 : x.distForEnemy.dist) *
                        enemyClosestWeight)).OrderBy(x => x.weightedDist);

            var hasGoneToOneOfMyCrystals = false;
            var hasGoneToOneOfEnemyCrystals = false;
            
            foreach (var crystal in myClosestCrystals)
            {
                if (crystal.closerToEnemy)
                {
                    gameActions.Line(crystal.baseIndex, crystal.crystalIndex, strengthToUseForEnemyCrystals);
                    hasGoneToOneOfEnemyCrystals = true;
                }
                else
                {
                    gameActions.Line(crystal.baseIndex, crystal.crystalIndex, strengthToUseForMyCrystals);
                    hasGoneToOneOfMyCrystals = true;
                }
                
                if (hasGoneToOneOfEnemyCrystals && hasGoneToOneOfMyCrystals)
                {
                    break;
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