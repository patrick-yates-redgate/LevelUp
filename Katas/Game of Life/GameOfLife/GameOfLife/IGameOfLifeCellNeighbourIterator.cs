namespace GameOfLife;

public interface IGameOfLifeCellNeighbourIterator
{
    IEnumerable<(int x, int y)> GetNeighbours((int x, int y) pos);
}