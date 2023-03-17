namespace GameOfLife;

public class GameOfLifeCellState : IGameOfLifeCellState
{
    private IGameOfLifeBoard m_board;
    
    public GameOfLifeCellState(IGameOfLifeBoard board)
    {
        m_board = board;
    }
    
    public IEnumerable<bool> GetCellStates(IEnumerable<(int x, int y)> cells)
    {
        foreach (var cell in cells)
        {
        }

        return new[] { false };
    }
}