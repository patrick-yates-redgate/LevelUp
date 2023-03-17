namespace GameOfLife;

public interface IGameOfLifeBoard
{
    IGameOfLifeBoard UpdateBoard(bool[][] values);
    bool Value((int x, int y) position);
    
    int Width { get; }
    int Height { get; }
    bool[][] Values { get; }
}