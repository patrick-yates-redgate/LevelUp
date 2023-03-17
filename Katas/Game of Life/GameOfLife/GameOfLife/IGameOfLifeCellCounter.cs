namespace GameOfLife;

public interface IGameOfLifeCellCounter
{
    int CountNeighbours(IEnumerable<bool> cellStates);
}