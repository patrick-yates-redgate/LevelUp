namespace GameOfLife;

public class GameOfLifeBoard : IGameOfLifeBoard
{
    private bool[][] m_values;
    
    public IGameOfLifeBoard UpdateBoard(bool[][] values)
    {
        m_values = values;
        
        return this;
    }

    public bool Value((int x, int y) position) => m_values[position.y][position.x];
    
    public int Width => m_values.Length > 0 ? m_values[0].Length : 0;
    public int Height => m_values.Length;

    public bool[][] Values => m_values;
}