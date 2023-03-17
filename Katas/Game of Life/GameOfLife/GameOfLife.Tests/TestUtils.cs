namespace GameOfLife.Tests;

public static class TestUtils
{
    public static bool Value(GameOfLife gameOfLife, int x, int y) => gameOfLife.Values[y][x];

    public static IGameOfLifeCellCounter Counter() => new GameOfLifeCellCounter();
    public static IGameOfLifeCellNeighbourIterator NeighbourIterator() => new GameOfLifeCellNeighbourIterator();

    public static int CountNeighbours(IGameOfLifeCellCounter counter, IEnumerable<bool> cellStates) =>
        counter.CountNeighbours(cellStates);

    public static IEnumerable<(int x, int y)> GetNeighbours(IGameOfLifeCellNeighbourIterator neighbourIterator,
        (int x, int y) position) =>
        neighbourIterator.GetNeighbours(position);

    public static IEnumerable<bool> GetCellStates(IGameOfLifeCellState cellState, IEnumerable<(int x, int y)> cells) =>
        cellState.GetCellStates(cells);

    public static int Width(IGameOfLifeBoard board) => board.Width;
    public static int Height(IGameOfLifeBoard board) => board.Height;
    public static bool[][] Values(IGameOfLifeBoard board) => board.Values;

    public static bool Value(IGameOfLifeBoard board, (int x, int y) position) => board.Value(position);

    public static IGameOfLifeBoard Board(string boardDescription)
    {
        if (string.IsNullOrWhiteSpace(boardDescription))
        {
            return new GameOfLifeBoard().UpdateBoard(Array.Empty<bool[]>());
        }

        var rows = boardDescription.Split(',');
        var boardValues = new bool[rows.Length][];
        var width = rows[0].Length;
        var height = rows.Length;

        for (var y = 0; y < height; ++y)
        {
            var values = new bool[width];

            for (var x = 0; x < width; ++x)
            {
                values[x] = rows[y][x] == 'X';
            }

            boardValues[y] = values;
        }

        return new GameOfLifeBoard().UpdateBoard(boardValues);
    }

    public static IGameOfLifeCellState CellState(Dictionary<(int x, int y), bool> expectedCalls)
    {
        var mockBoard = new Moq.Mock<IGameOfLifeBoard>();
        foreach (var key in expectedCalls.Keys)
        {
            mockBoard.SetupGet(x => x.Value(key)).Returns(expectedCalls[key]);
        }

        return new GameOfLifeCellState(mockBoard.Object);
    }

    public static GameOfLifeCell Cell(GameOfLife gameOfLife, int x, int y) => new(gameOfLife, x, y);

    public static int CellNeighbours(GameOfLife gameOfLife, int x, int y) =>
        new GameOfLifeCell(gameOfLife, x, y).CountNeighbours();
}