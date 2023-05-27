using System.Collections.Generic;

public class GameState
{
    public int NumberOfCells { get; set; }
    public int NumberOfBasesPerPlayer { get; set; }
    public List<Base> MyBases { get; set; } = new List<Base>();
    public List<Base> EnemyBases { get; set; } = new List<Base>();
    public List<Cell> Cells { get; set; } = new List<Cell>();
    
    public List<int> CrystalLocations { get; set; } = new List<int>();
    public List<int> EggLocations { get; set; } = new List<int>();
}