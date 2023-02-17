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
        for (var x = -1; x <= 1; x += 2)
        {
            if (Board.SafeValue(Pos.x + x, Pos.y) == true)
            {
                return 1;
            }
        }

        return 0;
    }
}