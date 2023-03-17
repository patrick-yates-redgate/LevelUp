namespace GameOfLife;

public class GameOfLifeCellNeighbourIterator : IGameOfLifeCellNeighbourIterator
{
    public IEnumerable<(int x, int y)> GetNeighbours((int x, int y) pos)
    {
        var (x, y) = pos;

        yield return (x - 1, y - 1);
        yield return (x - 1, y);
        yield return (x - 1, y + 1);
        yield return (x, y - 1);
        yield return (x, y + 1);
        yield return (x + 1, y - 1);
        yield return (x + 1, y);
        yield return (x + 1, y + 1);
    }
}