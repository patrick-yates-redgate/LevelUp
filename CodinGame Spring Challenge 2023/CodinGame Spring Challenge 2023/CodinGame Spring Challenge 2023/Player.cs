using System;

public static class Player
{
    static void Main(string[] args)
    {
        var gameState = new GameState();
        gameState.ReadInitialState();

        var gameActions = new GameActions();
        
        // game looping
        while (true)
        {
            gameState.ReadStateUpdate();
            
            // Write an action us Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");
            gameActions.FlushMoves();
        }
    }
}