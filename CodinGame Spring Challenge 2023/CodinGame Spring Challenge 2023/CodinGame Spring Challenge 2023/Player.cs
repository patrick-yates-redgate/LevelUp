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

        var strengthToUseForMyCrystals = 2;
        var strengthToUseForEnemyCrystals = 1;

        var myClosestCost = 0.3f;
        var enemyClosestCost = 0.7f;

        var crystalInitialCost = 10f;
        var eggInitialCost = 1f;
        var crystalAdjustmentEachFrame = -0.5f;

        var crystalCost = crystalInitialCost;
        var eggCost = eggInitialCost;

        // game looping
        while (true)
        {
            gameStateReader.ReadStateUpdate(gameState);
            pathFinder.ExpandPathKnowledge();
            Console.Error.WriteLine(pathFinder.DebugPaths(gameState));

            if (gameState.EggLocations.Count == 0)
            {
                crystalCost = 1f;
            }

            //TODO: Have some consideration over the overall game state, i.e. lots of crystals and lots of ants, or few ants and lots of eggs, etc.

            var locationsOfInterest = gameState.CrystalLocations
                .Select(cellIndex => (cellIndex, cellType: CellType.Crystal))
                .Concat(gameState.EggLocations.Select(cellIndex => (cellIndex, cellType: CellType.Eggs)));

            var myClosestLocationsOfInterest = locationsOfInterest
                .Select(location => (
                    location.cellIndex,
                    location.cellType,
                    preferenceCostFactor: location.cellType == CellType.Crystal ? crystalCost : eggCost,
                    distForMe: pathFinder.ClosestOrDefault(location.cellIndex, gameState.MyBaseLocations),
                    distForEnemy: pathFinder.ClosestOrDefault(location.cellIndex, gameState.EnemyBaseLocations)))
                .Where(x => x.distForMe.index != -1)
                .Select(
                    x => (
                        x.cellIndex,
                        x.cellType,
                        baseIndex: x.distForMe.index,
                        closerToEnemy: x.distForEnemy.index > -1 && x.distForEnemy.dist < x.distForMe.dist,
                        cost: x.preferenceCostFactor * (x.distForMe.dist * myClosestCost +
                                                        (x.distForEnemy.index > -1 ? 100 : x.distForEnemy.dist) *
                                                        enemyClosestCost)))
                .OrderBy(x => -x.cost);

            var myLocationCount = 0;
            var enemyLocationCount = 0;
            var crystalsCount = 0;
            var eggsCount = 0;

            foreach (var location in myClosestLocationsOfInterest)
            {
                if (location.closerToEnemy)
                {
                    //Don't go to too many enemy locations
                    if (enemyLocationCount == 0)
                    {
                        gameActions.Line(location.baseIndex, location.cellIndex, strengthToUseForEnemyCrystals);
                        ++enemyLocationCount;   
                    }
                }
                else
                {
                    gameActions.Line(location.baseIndex, location.cellIndex, strengthToUseForMyCrystals);
                    ++myLocationCount;
                }

                if (location.cellType == CellType.Crystal)
                {
                    ++crystalsCount;
                }
                else
                {
                    ++eggsCount;
                }

                if (myLocationCount > 0 && enemyLocationCount > 0 && crystalsCount > 0 && eggsCount > 0)
                {
                    break;
                }
            }
            
            gameActions.Message($"My Locations: {myLocationCount}, Enemy Locations: {enemyLocationCount}, Crystals: {crystalsCount}, Eggs: {eggsCount}");

            // Write an action us Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");
            gameActions.FlushMoves();

            crystalCost = Math.Max(1f, crystalCost + crystalAdjustmentEachFrame);
        }
    }
}