using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CodinGame_Spring_Challenge_2023.Core;
using CodinGame_Spring_Challenge_2023.Domain;
using CodinGame_Spring_Challenge_2023.PathFinding;
using CodinGame_Spring_Challenge_2023.Utils;

namespace CodinGame_Spring_Challenge_2023.Strategy;

public class StrategyCostBenefitAdaptive
{
    private GameState _gameState;
    private PathFinder _pathFinder;
    private MapInfo _mapInfo;
    private readonly ShortestTree _shortestTree;

    //private List<int> _currentlyVisiting = new();

    public StrategyCostBenefitAdaptive(GameState gameState, PathFinder pathFinder, MapInfo mapInfo, ShortestTree shortestTree)
    {
        _gameState = gameState;
        _pathFinder = pathFinder;
        _mapInfo = mapInfo;
        _shortestTree = shortestTree;
    }

    public OneOf<(int cost, float benefit), Invalid> CostBenefit(
        List<int> currentlyVisiting,
        int potentialLocation,
        bool isEgg,
        int currentNumAnts,
        List<int> eggsResourcesVisited,
        List<int> crystalResourcesVisited)
    {
        var averageCollectionRate = (float)currentNumAnts / currentlyVisiting.Count;
        var eggCollectionRate = eggsResourcesVisited.Count * averageCollectionRate;
        var crystalCollectionRate = crystalResourcesVisited.Count * averageCollectionRate;

        return _pathFinder.ClosestOrDefault(potentialLocation, currentlyVisiting)
            .Match<OneOf<(int cost, float benefit), Invalid>>(
                found =>
                {
                    var newAverageCollectionRate = (float)(currentNumAnts) / (currentlyVisiting.Count + found.dist);
                    if (isEgg)
                    {
                        newAverageCollectionRate = (currentNumAnts + newAverageCollectionRate) / (currentlyVisiting.Count + found.dist);
                    }

                    var newEggCollectionRate = (eggsResourcesVisited.Count + (isEgg ? 1 : 0)) * newAverageCollectionRate;
                    var newCrystalCollectionRate = (crystalResourcesVisited.Count + (isEgg ? 0 : 1)) * newAverageCollectionRate;
                    return (found.dist, newEggCollectionRate - eggCollectionRate + newCrystalCollectionRate - crystalCollectionRate);
                },
                _ => new Invalid());
    }

    public IEnumerable<int> Update()
    {
        /*
        _currentlyVisiting.RemoveAll(index => _gameState.Cells[index].Resources == 0);

        if (_currentlyVisiting.Count == 0)
        {
            _currentlyVisiting = _gameState.MyBaseLocations.ToList();
        }
        */

        var currentlyVisiting = _gameState.MyBaseLocations.ToList();
        while (true)
        {
            Console.Error.WriteLine("Strategy: Currently visiting: " + string.Join(",", currentlyVisiting));
            var potentialLocations = _gameState.CrystalLocations.Select(index => (index, isEgg: true)).ToList();
            var myVisitedEggLocations = currentlyVisiting.Where(x => _gameState.EggLocations.Contains(x)).ToList();
            var myVisitedCrystalLocations = currentlyVisiting.Where(x => _gameState.CrystalLocations.Contains(x)).ToList();

            if (!myVisitedEggLocations.Any())
            {
                potentialLocations.AddRange(_gameState.EggLocations.Select(index => (index, isEgg: false)));
            }

            potentialLocations.RemoveAll(x => currentlyVisiting.Contains(x.index));

            var costBenefits = potentialLocations.Select(x => (x.index, costBenefit: CostBenefit(currentlyVisiting, x.index,
                    x.isEgg, _gameState.MyAntCount,
                    myVisitedEggLocations, myVisitedCrystalLocations))).Where(x => x.costBenefit.IsValue1)
                .Select(x => (x.index, costBenefit: x.costBenefit.Value1))
                .OrderBy(x => -x.costBenefit.benefit).ToList();

            if (costBenefits.Any())
            {
                foreach (var result in costBenefits)
                {
                    Console.Error.WriteLine($"Strategy: Cost of going to {result.index} is {result.costBenefit.cost} and benefit is {result.costBenefit.benefit}");
                }
                
                var first = costBenefits.First();
                if (first.costBenefit.benefit > 0 || (myVisitedCrystalLocations.Count == 0 && myVisitedEggLocations.Count == 0))
                {
                    //Console.Error.WriteLine($"Strategy: Cost of going to {first.index} is {first.costBenefit.cost} and benefit is {first.costBenefit.benefit}");
                    var locations = _gameState.MyBaseLocations.Concat(myVisitedEggLocations)
                        .Concat(myVisitedCrystalLocations).Concat(new []{ first.index});
                    currentlyVisiting = ShortestTreeWalker.WalkShortestTree(_gameState,_pathFinder, _shortestTree.GetShortestTree(locations)).ToList();
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