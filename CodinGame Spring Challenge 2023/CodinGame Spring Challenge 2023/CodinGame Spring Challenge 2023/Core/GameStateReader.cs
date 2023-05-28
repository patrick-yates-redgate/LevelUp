using System;
using System.Collections.Generic;
using System.Linq;
using CodinGame_Spring_Challenge_2023.Domain;
using CodinGame_Spring_Challenge_2023.PathFinding;

namespace CodinGame_Spring_Challenge_2023.Core;

public static class GameStateReader
{
    private static void ReadNumberOfCells(GameState gameState)
    {
        // amount of hexagonal cells in this map
        gameState.NumberOfCells = int.Parse(Console.ReadLine());
    }

    private static void ReadInitialCellState(GameState gameState)
    {
        for (var i = 0; i < gameState.NumberOfCells; i++)
        {
            var inputs = Console.ReadLine().Split(' ');
            var type = (CellType)int.Parse(inputs[0]); // 0 for empty, 1 for eggs, 2 for crystal
            var initialResources = int.Parse(inputs[1]); // the initial amount of eggs/crystals on this cell
            var neigh0 = int.Parse(inputs[2]); // the index of the neighbouring cell for each direction
            var neigh1 = int.Parse(inputs[3]);
            var neigh2 = int.Parse(inputs[4]);
            var neigh3 = int.Parse(inputs[5]);
            var neigh4 = int.Parse(inputs[6]);
            var neigh5 = int.Parse(inputs[7]);

            gameState.Cells.Add(new Cell
            {
                CellType = type,
                Resources = initialResources,
                Neighbours = new[] { neigh0, neigh1, neigh2, neigh3, neigh4, neigh5 }
            });

            if (type == CellType.Crystal)
            {
                gameState.Crystals.Add(new Crystal { CellIndex = i });
                gameState.CrystalLocations.Add(i);
            }

            if (type == CellType.Eggs)
            {
                gameState.Eggs.Add(new Egg { CellIndex = i });
                gameState.EggLocations.Add(i);
            }
        }
    }

    private static void ReadNumberOfBases(GameState gameState)
    {
        gameState.NumberOfBasesPerPlayer = int.Parse(Console.ReadLine());
    }

    private static void ReadBases(GameState gameState, ICollection<Base> bases, ICollection<int> baseLocations)
    {
        var inputs = Console.ReadLine().Split(' ');
        for (var i = 0; i < gameState.NumberOfBasesPerPlayer; i++)
        {
            var baseIndex = int.Parse(inputs[i]);
            bases.Add(new Base { CellIndex = baseIndex });
            baseLocations.Add(baseIndex);
        }
    }

    public static GameState ReadInitialState()
    {
        var gameState = new GameState();
        ReadNumberOfCells(gameState);
        ReadInitialCellState(gameState);
        ReadNumberOfBases(gameState);
        ReadBases(gameState, gameState.MyBases, gameState.MyBaseLocations);
        ReadBases(gameState, gameState.EnemyBases, gameState.EnemyBaseLocations);

        return gameState;
    }

    public static void ReadStateUpdate(GameState gameState)
    {
        for (var i = 0; i < gameState.NumberOfCells; i++)
        {
            var inputs = Console.ReadLine().Split(' ');
            var resources = int.Parse(inputs[0]); // the current amount of eggs/crystals on this cell
            var myAnts = int.Parse(inputs[1]); // the amount of your ants on this cell
            var oppAnts = int.Parse(inputs[2]); // the amount of opponent ants on this cell

            gameState.Cells[i].Resources = resources;
            gameState.Cells[i].NumMyAnts = myAnts;
            gameState.Cells[i].NumEnemyAnts = oppAnts;

            if (resources == 0 && gameState.CrystalLocations.Contains(i))
            {
                gameState.CrystalLocations.Remove(i);
                gameState.ContestedCrystalLocations.Remove(i);
                gameState.MyCrystalLocations.Remove(i);
                gameState.EnemyCrystalLocations.Remove(i);
                gameState.Crystals.RemoveAll(x => x.CellIndex == i);
            }

            if (resources == 0 && gameState.EggLocations.Contains(i))
            {
                gameState.EggLocations.Remove(i);
                gameState.ContestedEggLocations.Remove(i);
                gameState.MyEggLocations.Remove(i);
                gameState.EnemyEggLocations.Remove(i);
                gameState.Eggs.RemoveAll(x => x.CellIndex == i);
            }
        }
    }

    public static void DetermineOwnership(GameState gameState, PathFinder pathFinder)
    {
        var crystalContention = gameState.CrystalLocations.Select(index =>
        {
            var closestEnemyBase = pathFinder.ClosestOrDefault(index, gameState.EnemyBaseLocations);
            if (closestEnemyBase.index == -1)
            {
                return (index, isContested: true, isMine: false);
            }

            var closestMyBase = pathFinder.ClosestOrDefault(index, gameState.MyBaseLocations);
            if (closestMyBase.index == -1)
            {
                return (index, isContested: true, isMine: false);
            }

            return (index, isContested: closestEnemyBase.dist == closestMyBase.dist,
                isMine: closestMyBase.dist < closestEnemyBase.dist);
        });
        
        foreach (var (index, isContested, isMine) in crystalContention)
        {
            if (isContested)
            {
                gameState.ContestedCrystalLocations.Add(index);
            }
            else if (isMine)
            {
                gameState.MyCrystalLocations.Add(index);
            }
            else
            {
                gameState.EnemyCrystalLocations.Add(index);
            }
        }
        
        var eggContention = gameState.EggLocations.Select(index =>
        {
            var closestEnemyBase = pathFinder.ClosestOrDefault(index, gameState.EnemyBaseLocations);
            if (closestEnemyBase.index == -1)
            {
                return (index, isContested: true, isMine: false);
            }

            var closestMyBase = pathFinder.ClosestOrDefault(index, gameState.MyBaseLocations);
            if (closestMyBase.index == -1)
            {
                return (index, isContested: true, isMine: false);
            }

            return (index, isContested: closestEnemyBase.dist == closestMyBase.dist,
                isMine: closestMyBase.dist < closestEnemyBase.dist);
        });
        
        foreach (var (index, isContested, isMine) in eggContention)
        {
            if (isContested)
            {
                gameState.ContestedEggLocations.Add(index);
            }
            else if (isMine)
            {
                gameState.MyEggLocations.Add(index);
            }
            else
            {
                gameState.EnemyEggLocations.Add(index);
            }
        }
    }
}