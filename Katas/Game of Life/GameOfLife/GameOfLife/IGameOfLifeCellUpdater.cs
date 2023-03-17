namespace GameOfLife;

public interface IGameOfLifeCellUpdater
{
    bool GetNextState(bool currentState, int neighbourCount);
}