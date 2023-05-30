using System.Collections.Generic;
using CodinGame_Spring_Challenge_2023.Domain;

namespace CodinGame_Spring_Challenge_2023.Core;

public class GameState
{
    public int NumberOfCells { get; set; }
    public int NumberOfBasesPerPlayer { get; set; }
    
    public int MyAntCount { get; set; }
    public int EnemyAntCount { get; set; }
    
    public int MyCrystalCount { get; set; }
    public int EnemyCrystalCount { get; set; }
    
    public List<Base> MyBases { get; set; } = new ();
    public List<Base> EnemyBases { get; set; } = new ();
    public List<Cell> Cells { get; set; } = new ();
    public List<Crystal> Crystals { get; set; } = new ();
    public List<Egg> Eggs { get; set; } = new ();
    
    public List<int> MyBaseLocations { get; set; } = new ();
    public List<int> EnemyBaseLocations { get; set; } = new ();
    public List<int> CrystalLocations { get; set; } = new ();
    public List<int> EggLocations { get; set; } = new ();
    public List<int> MyCrystalLocations { get; set; } = new ();
    public List<int> EnemyCrystalLocations { get; set; } = new ();
    public List<int> MyEggLocations { get; set; } = new ();
    public List<int> EnemyEggLocations { get; set; } = new ();
    public List<int> ContestedCrystalLocations { get; set; } = new ();
    public List<int> ContestedEggLocations { get; set; } = new ();

    public Dictionary<int, int> MyAntLookup { get; set; } = new ();

    public Dictionary<int, int> EnemyAntLookup { get; set; } = new ();
}