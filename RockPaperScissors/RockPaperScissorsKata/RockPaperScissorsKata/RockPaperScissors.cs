namespace RockPaperScissorsKata;

public class RockPaperScissors<T> where T : Enum
{
    private Dictionary<T, string>? VictoryMessages { get; set; }
    
    public RockPaperScissors(Dictionary<T, string>? victoryMessages = null)
    {
        VictoryMessages = victoryMessages;
    }

    public T Play(RpsChoices<T> choices)
    {
        var enumValues = Enum.GetValues(typeof(T)) as T[] ?? throw new ArgumentException("Expect specified enum to have values");
        
        if (Equals(choices.A, choices.B))
        {
            return enumValues[0];
        }
        
        var numChoices = enumValues.Length - 1; //Includes draw in the enum values
        var a = IndexOf(choices.A, enumValues);
        var b = IndexOf(choices.B, enumValues);
        
        if (a == 0 || b == 0)
        {
            throw new ArgumentException("Please do not select Draw!");
        }

        var result = (a - b + numChoices) % numChoices;
        return result % 2 == 1 ? choices.A : choices.B;
    }

    public string OutcomeMessage(RpsChoices<T> choices, T winner)
    {
        if (Equals(choices.A, choices.B))
        {
            return "Draw!";
        }
        
        var loser = Equals(choices.A, winner) ? choices.B : choices.A;
        var victoryType = VictoryMessages != null && VictoryMessages.ContainsKey(winner) ? VictoryMessages[winner] : "beats";
        
        return $"{winner} {victoryType} {loser}!";
    }

    private static int IndexOf(T value, IReadOnlyList<T> values)
    {
        for (var i = 0; i < values.Count; ++i)
        {
            if (Equals(value, values[i]))
            {
                return i;
            }
        }

        return -1;
    }

    private static bool Equals(T value, T compareValue)
    {
        return EqualityComparer<T>.Default.Equals(value, compareValue);
    }
}

public record RpsChoices<T>(T A, T B) where T : Enum;