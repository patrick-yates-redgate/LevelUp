namespace GameOfLife;

public interface IGameOfLifeCellState
{
    IEnumerable<bool> GetCellStates(IEnumerable<(int x, int y)> cells);
}