namespace RockPaperScissorsKata;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
public class WinsWithAttribute : Attribute
{
    public string VictoryCondition { get; }

    public WinsWithAttribute(string victoryCondition)
    {
        VictoryCondition = victoryCondition;
    }
}