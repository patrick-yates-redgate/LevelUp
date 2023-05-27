using System;
using System.Collections.Generic;

public class GameActions
{
    private List<string> Actions { get; set; } = new List<string>();

    public GameActions()
    {
    }

    public void FlushMoves()
    {
        Console.WriteLine(string.Join(";", Actions));
    }
    
    // WAIT | LINE <sourceIdx> <targetIdx> <strength> | BEACON <cellIdx> <strength> | MESSAGE <text>
    public void Wait()
    {
        Actions.Add("WAIT");
    }
        
    public void Line(int sourceIdx, int targetIdx, int strength)
    {
        Actions.Add($"LINE {sourceIdx} {targetIdx} {strength}");
    }
        
    public void Beacon(int cellIdx, int strength)
    {
        Actions.Add($"BEACON {cellIdx} {strength}");
    }
        
    public void Message(string text)
    {
        Actions.Add($"MESSAGE {text}");
    }
}