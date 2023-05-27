
public class Cell
{
    public CellType CellType { get; set; }
    public int Resources { get; set; }
    
    public int[] Neighbours { get; set; } = new int[6];

    public int NumAnts => NumMyAnts + NumEnemyAnts;
    public int NumMyAnts { get; set; }
    public int NumEnemyAnts { get; set; }
}