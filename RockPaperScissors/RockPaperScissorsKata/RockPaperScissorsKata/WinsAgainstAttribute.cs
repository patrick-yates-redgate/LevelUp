namespace RockPaperScissorsKata;

// Attribute that can be added to a RockPaperScissor enum in order to specify which enum values it wins against

// Attribute that can be added to a RockPaperScissor enum in order to specify which enum values it wins against
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
public class WinsAgainstAttribute<T> : Attribute where T : Enum
{
    private T WinsAgainst { get; }
    private string VictoryCondition { get; }

    public WinsAgainstAttribute(T winsAgainst, string victoryCondition)
    {
        WinsAgainst = winsAgainst;
        VictoryCondition = victoryCondition;
    }
    
    public string? GetVictoryCondition(T winsAgainst)
    {
        return EqualityComparer<T>.Default.Equals(winsAgainst, WinsAgainst) ? VictoryCondition : null;
    }
}