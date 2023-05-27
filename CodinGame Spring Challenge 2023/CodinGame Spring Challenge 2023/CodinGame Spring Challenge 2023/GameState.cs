using System;
using System.Collections.Generic;

public class GameState
{
    private int _numberOfCells;
    private int _numberOfBasesPerPlayer;
    private readonly List<Base> _myBases = new List<Base>();
    private readonly List<Base> _enemyBases = new List<Base>();
    private readonly List<Cell> _cells = new List<Cell>();

    private void ReadNumberOfCells()
    {
        // amount of hexagonal cells in this map
        _numberOfCells = int.Parse(Console.ReadLine());
    }

    private void ReadInitialCellState()
    {
        for (var i = 0; i < _numberOfCells; i++)
        {
            var inputs = Console.ReadLine().Split(' ');
            var type = int.Parse(inputs[0]); // 0 for empty, 1 for eggs, 2 for crystal
            var initialResources = int.Parse(inputs[1]); // the initial amount of eggs/crystals on this cell
            var neigh0 = int.Parse(inputs[2]); // the index of the neighbouring cell for each direction
            var neigh1 = int.Parse(inputs[3]);
            var neigh2 = int.Parse(inputs[4]);
            var neigh3 = int.Parse(inputs[5]);
            var neigh4 = int.Parse(inputs[6]);
            var neigh5 = int.Parse(inputs[7]);

            _cells.Add(new Cell
            {
                CellType = (CellType)type,
                Resources = initialResources,
                Neighbours = new[] { neigh0, neigh1, neigh2, neigh3, neigh4, neigh5 }
            });
        }
    }

    private void ReadNumberOfBases()
    {
        _numberOfBasesPerPlayer = int.Parse(Console.ReadLine());
    }

    private void ReadBases(ICollection<Base> bases)
    {
        var inputs = Console.ReadLine().Split(' ');
        for (var i = 0; i < _numberOfBasesPerPlayer; i++)
        {
            bases.Add(new Base() { CellIndex = int.Parse(inputs[i]) });
        }
    }

    public void ReadInitialState()
    {
        ReadNumberOfCells();
        ReadInitialCellState();
        ReadNumberOfBases();
        ReadBases(_myBases);
        ReadBases(_enemyBases);
    }

    public void ReadStateUpdate()
    {
        for (var i = 0; i < _numberOfCells; i++)
        {
            var inputs = Console.ReadLine().Split(' ');
            var resources = int.Parse(inputs[0]); // the current amount of eggs/crystals on this cell
            var myAnts = int.Parse(inputs[1]); // the amount of your ants on this cell
            var oppAnts = int.Parse(inputs[2]); // the amount of opponent ants on this cell
            
            _cells[i].Resources = resources;
            _cells[i].NumMyAnts = myAnts;
            _cells[i].NumEnemyAnts = oppAnts;
        }
    }
}