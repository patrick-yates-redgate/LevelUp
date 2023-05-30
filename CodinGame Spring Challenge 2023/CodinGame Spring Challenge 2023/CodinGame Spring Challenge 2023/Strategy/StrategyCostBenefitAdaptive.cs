using System;
using System.Collections.Generic;
using System.Linq;
using CodinGame_Spring_Challenge_2023.Core;
using CodinGame_Spring_Challenge_2023.PathFinding;
using CodinGame_Spring_Challenge_2023.Utils;

namespace CodinGame_Spring_Challenge_2023.Strategy;

public class StrategyCostBenefitAdaptive
{
    private GameState _gameState;
    private PathFinder _pathFinder;
    private readonly ShortestTree _shortestTree;

    //private List<int> _currentlyVisiting = new();

    public StrategyCostBenefitAdaptive(GameState gameState, PathFinder pathFinder,
        ShortestTree shortestTree)
    {
        _gameState = gameState;
        _pathFinder = pathFinder;
        _shortestTree = shortestTree;
    }

    public static float CalculatePotentialBenefit(List<(int index, float resources)> crystalResources,
        List<(int index, float resources)> eggResources, float myAnts,
        int numLocations, int numFrames, float enemyPotentialConsumeRate)
    {
        if (numFrames == 0)
        {
            return 0;
        }

        var averageAntsPerLocation = myAnts / numLocations;
        if (eggResources.Count == 0)
        {
            return crystalResources.Sum(x => Math.Min(x.resources, numFrames * averageAntsPerLocation));
        }

        var newEggResources = eggResources
            .Select(x => x with { resources = Math.Max(0, x.resources - averageAntsPerLocation) }).ToList();
        var newCrystalResources = crystalResources
            .Select(x => x with { resources = Math.Max(0, x.resources - averageAntsPerLocation) }).ToList();

        var eggsConsumed = newEggResources.Sum(x => Math.Min(x.resources, averageAntsPerLocation));
        var crystalsConsumed = newCrystalResources.Sum(x => Math.Min(x.resources, averageAntsPerLocation));

        var myNewAnts = myAnts + eggsConsumed;

        return 0.2f * enemyPotentialConsumeRate + crystalsConsumed + CalculatePotentialBenefit(newCrystalResources, newEggResources, myNewAnts,
            numLocations, numFrames - 1, enemyPotentialConsumeRate);
    }

    private OneOf<(int cost, float benefit), Invalid> CostBenefit(
        List<int> currentlyVisiting,
        List<int> enemyLocations,
        int potentialLocation,
        bool isEgg,
        int currentNumAnts,
        int enemyNumAnts,
        List<(int index, float resources)> eggsResourcesVisited,
        List<(int index, float resources)> crystalResourcesVisited,
        int lookAhead)
    {
        var enemyDistances = 0;
        if (!enemyLocations.Contains(potentialLocation))
        {
            enemyDistances = _pathFinder.ClosestOrDefault(potentialLocation, enemyLocations).Match(found => found.dist, _ => 5);   
        }

        var enemyPotentialConsumeRate = enemyNumAnts / (enemyLocations.Count + enemyDistances);
        
        var currentBenefit = CalculatePotentialBenefit(crystalResourcesVisited, eggsResourcesVisited, currentNumAnts,
            currentlyVisiting.Count, lookAhead, enemyPotentialConsumeRate);

        return _pathFinder.ClosestOrDefault(potentialLocation, currentlyVisiting)
            .Match<OneOf<(int cost, float benefit), Invalid>>(
                found =>
                {
                    var updatedCrystalResourcesVisited = crystalResourcesVisited.ToList();
                    var updatedEggsResourcesVisited = eggsResourcesVisited.ToList();
                    (isEgg ? ref updatedEggsResourcesVisited : ref updatedCrystalResourcesVisited).Add((found.index,
                        resources: _gameState
                            .Cells[potentialLocation].Resources));

                    var updatedBenefit = CalculatePotentialBenefit(updatedCrystalResourcesVisited,
                        updatedEggsResourcesVisited, currentNumAnts,
                        currentlyVisiting.Count + found.dist, lookAhead, enemyPotentialConsumeRate);

                    if (isEgg)
                    {
                        Console.Error.WriteLine(
                            $"CostBenefit for location {potentialLocation} Eggs: {_gameState.Cells[potentialLocation].Resources} (dist: {found.dist}) = {currentBenefit} -> {updatedBenefit}");
                    }
                    else
                    {
                        Console.Error.WriteLine(
                            $"CostBenefit for location {potentialLocation} Crystals: {_gameState.Cells[potentialLocation].Resources} (dist: {found.dist}) = {currentBenefit} -> {updatedBenefit}");
                    }

                    return (found.dist, updatedBenefit - currentBenefit);
                },
                _ => new Invalid());
    }

    public IEnumerable<int> Update(int projectedTurnsUntilGameEnds)
    {
        Console.Error.WriteLine("Strategy: Projected Turns Until Game Ends: " + projectedTurnsUntilGameEnds);
        var currentlyVisiting = _gameState.MyBaseLocations.ToList();
        while (true)
        {
            Console.Error.WriteLine("Strategy: Currently visiting: " + string.Join(",", currentlyVisiting));
            var potentialLocations = _gameState.CrystalLocations.Select(index => (index, isEgg: false)).ToList();
            var myVisitedEggLocations = currentlyVisiting.Where(x => _gameState.EggLocations.Contains(x))
                .Select(index => (index, resources: (float)_gameState.Cells[index].Resources)).ToList();
            var myVisitedCrystalLocations =
                currentlyVisiting.Where(x => _gameState.CrystalLocations.Contains(x))
                    .Select(index => (index, resources: (float)_gameState.Cells[index].Resources)).ToList();

            if (!myVisitedEggLocations.Any())
            {
                potentialLocations.AddRange(_gameState.EggLocations.Select(index => (index, isEgg: true)));
            }

            var enemyLocations = _gameState.Cells.Where(x => x.NumEnemyAnts > 0).Select((_, index) => index).ToList();

            potentialLocations.RemoveAll(x => currentlyVisiting.Contains(x.index));

            var costBenefits = potentialLocations.Select(x => (
                    x.index,
                    costBenefit: CostBenefit(
                        currentlyVisiting,
                        enemyLocations,
                        x.index,
                        x.isEgg,
                        _gameState.MyAntCount,
                        _gameState.EnemyAntCount,
                        myVisitedEggLocations,
                        myVisitedCrystalLocations,
                        projectedTurnsUntilGameEnds)))
                .Where(x => x.costBenefit.IsValue1)
                .Select(x => (x.index, costBenefit: x.costBenefit.Value1))
                .OrderBy(x => -x.costBenefit.benefit).ToList();

            if (costBenefits.Any())
            {
                foreach (var result in costBenefits)
                {
                    Console.Error.WriteLine(
                        $"Strategy: Cost of going to {result.index} is {result.costBenefit.cost} and benefit is {result.costBenefit.benefit}");
                }

                var first = costBenefits.First();
                if (first.costBenefit.benefit > .5f ||
                    (myVisitedCrystalLocations.Count == 0 && myVisitedEggLocations.Count == 0))
                {
                    //Console.Error.WriteLine($"Strategy: Cost of going to {first.index} is {first.costBenefit.cost} and benefit is {first.costBenefit.benefit}");
                    var locations = _gameState.MyBaseLocations.Concat(myVisitedEggLocations.Select(x => x.index))
                        .Concat(myVisitedCrystalLocations.Select(x => x.index)).Concat(new[] { first.index });
                    currentlyVisiting = ShortestTreeWalker
                        .WalkShortestTree(_gameState, _pathFinder, _shortestTree.GetShortestTree(locations)).ToList();
                    continue;
                }
            }

            break;
        }

        return currentlyVisiting;

        //WFC of all possible states? We consider if we add each one and the cost/benefit.
        //AStar search maybe or all possible states and then we pick the best one?
        //Each addition takes away from the rest, so we look to find optimal


        /*
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
        */
    }
}