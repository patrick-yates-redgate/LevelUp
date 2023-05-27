using System;
using System.Collections.Generic;

public class GameActions
{
    private const bool ShowDebug = true;
    
    private List<string> Actions { get; set; } = new List<string>();

    public GameActions()
    {
    }

    public void FlushMoves()
    {
        if (Actions.Count == 0)
        {
            Wait();
        }
        
        Console.WriteLine(string.Join(";", Actions));
        
        Actions.Clear();
    }
    
    // WAIT | LINE <sourceIdx> <targetIdx> <strength> | BEACON <cellIdx> <strength> | MESSAGE <text>
    public void Wait()
    {
        Actions.Add("WAIT");
    }
        
    public void Line(int sourceIdx, int targetIdx, int strength, string message = null)
    {
        DebugMessage(message);
        Actions.Add($"LINE {sourceIdx} {targetIdx} {strength}");
    }
        
    public void Beacon(int cellIdx, int strength, string message = null)
    {
        DebugMessage(message);
        Actions.Add($"BEACON {cellIdx} {strength}");
    }
        
    public void Message(string text)
    {
        Actions.Add($"MESSAGE {text}");
    }

    public void DebugMessage(string message)
    {
        if (!ShowDebug || message == null) return;
        
        Message(message);
    }
}