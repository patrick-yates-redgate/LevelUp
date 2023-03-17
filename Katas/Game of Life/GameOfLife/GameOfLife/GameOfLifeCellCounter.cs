namespace GameOfLife;

public class GameOfLifeCellCounter : IGameOfLifeCellCounter
{
    public int CountNeighbours(IEnumerable<bool> cellStates)
    {
        return cellStates.Select(value => value ? 1 : 0).Aggregate((value, total) => total + value);
    }
}