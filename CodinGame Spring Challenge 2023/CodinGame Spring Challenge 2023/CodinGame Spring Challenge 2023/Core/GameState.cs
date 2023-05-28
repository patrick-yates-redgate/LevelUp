using System.Collections.Generic;
using CodinGame_Spring_Challenge_2023.Domain;

namespace CodinGame_Spring_Challenge_2023.Core;

public class GameState
{
    public int NumberOfCells { get; set; }
    public int NumberOfBasesPerPlayer { get; set; }
    
    public List<Base> MyBases { get; set; } = new List<Base>();
    public List<Base> EnemyBases { get; set; } = new List<Base>();
    public List<Cell> Cells { get; set; } = new List<Cell>();
    public List<Crystal> Crystals { get; set; } = new List<Crystal>();
    public List<Egg> Eggs { get; set; } = new List<Egg>();
    
    public List<int> MyBaseLocations { get; set; } = new List<int>();
    public List<int> EnemyBaseLocations { get; set; } = new List<int>();
    public List<int> CrystalLocations { get; set; } = new List<int>();
    public List<int> EggLocations { get; set; } = new List<int>();
    public List<int> MyCrystalLocations { get; set; } = new List<int>();
    public List<int> EnemyCrystalLocations { get; set; } = new List<int>();
    public List<int> MyEggLocations { get; set; } = new List<int>();
    public List<int> EnemyEggLocations { get; set; } = new List<int>();
    public List<int> ContestedCrystalLocations { get; set; } = new List<int>();
    public List<int> ContestedEggLocations { get; set; } = new List<int>();
}