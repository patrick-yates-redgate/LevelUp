namespace GameOfLife;

public class GameOfLife
{
    private bool[][] Board { get; init; }
    
    public GameOfLife(bool[][] board)
    {
        Board = board;
    }

    public int Width => Board.Length > 0 ? Board[0].Length : 0;
    public int Height => Board.Length;
    public bool[][] Values => Board;

    public bool Value(int x, int y) => Values[y][x];
    public bool? SafeValue(int x, int y) => x >= 0 && y >= 0 && Values.Length > y && Values[y].Length > x ? Values[y][x] : null;

    public void NextGen()
    {
        
    }
}