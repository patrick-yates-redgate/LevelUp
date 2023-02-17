namespace GameOfLife;

public class GameOfLifeCell
{
    private GameOfLife Board { get; }
    private (int x, int y) Pos { get; }
    
    public GameOfLifeCell(GameOfLife board, int x, int y)
    {
        Board = board;
        Pos = (x, y);
    }

    public int CountNeighbours()
    {
        var count = 0;
        for (var x = -1; x <= 1; x += 2)
        {
            count += Board.SafeValue(Pos.x + x, Pos.y) == true ? 1 : 0;
        }
        
        for (var y = -1; y <= 1; y += 2)
        {
            count += Board.SafeValue(Pos.x, Pos.y + y) == true ? 1 : 0;
        }

        return count;
    }
}