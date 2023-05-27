using System;
using System.Collections.Generic;

public class GameStateReader
{
    private void ReadNumberOfCells(GameState gameState)
    {
        // amount of hexagonal cells in this map
        gameState.NumberOfCells = int.Parse(Console.ReadLine());
    }

    private void ReadInitialCellState(GameState gameState)
    {
        for (var i = 0; i < gameState.NumberOfCells; i++)
        {
            var inputs = Console.ReadLine().Split(' ');
            var type = (CellType) int.Parse(inputs[0]); // 0 for empty, 1 for eggs, 2 for crystal
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
                gameState.CrystalLocations.Add(i);
            }
            
            if (type == CellType.Eggs)
            {
                gameState.EggLocations.Add(i);
            }
        }
    }

    private void ReadNumberOfBases(GameState gameState)
    {
        gameState.NumberOfBasesPerPlayer = int.Parse(Console.ReadLine());
    }

    private void ReadBases(GameState gameState, ICollection<Base> bases)
    {
        var inputs = Console.ReadLine().Split(' ');
        for (var i = 0; i < gameState.NumberOfBasesPerPlayer; i++)
        {
            bases.Add(new Base() { CellIndex = int.Parse(inputs[i]) });
        }
    }

    public GameState ReadInitialState()
    {
        var gameState = new GameState();
        ReadNumberOfCells(gameState);
        ReadInitialCellState(gameState);
        ReadNumberOfBases(gameState);
        ReadBases(gameState, gameState.MyBases);
        ReadBases(gameState, gameState.EnemyBases);

        return gameState;
    }

    public void ReadStateUpdate(GameState gameState)
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
            }
            
            if (resources == 0 && gameState.EggLocations.Contains(i))
            {
                gameState.EggLocations.Remove(i);
            }
        }
    }
}