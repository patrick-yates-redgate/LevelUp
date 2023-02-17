namespace GameOfLife.Tests;

public static class TestUtils
{
    public static bool Value(GameOfLife gameOfLife, int x, int y) => gameOfLife.Values[y][x];

    public static GameOfLife Board(string boardDescription)
    {
        if (string.IsNullOrWhiteSpace(boardDescription))
        {
            return new GameOfLife(Array.Empty<bool[]>());
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
        
        return new GameOfLife(boardValues);
    }

    public static GameOfLifeCell Cell(GameOfLife gameOfLife, int x, int y) => new (gameOfLife, x, y);
    public static int CellNeighbours(GameOfLife gameOfLife, int x, int y) => new GameOfLifeCell(gameOfLife, x, y).CountNeighbours();
}