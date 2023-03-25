using System.Reflection;

namespace RockPaperScissorsKata;

public class RockPaperScissors<T> where T : Enum
{
    public T Play(RpsChoices<T> choices)
    {
        var enumValues = Enum.GetValues(typeof(T)) as T[] ??
                         throw new ArgumentException("Expect specified enum to have values");

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

        var result = ((a - b) + numChoices) % numChoices;
        return result % 2 == 1 ? choices.A : choices.B;
    }

    private int IndexOf(T value, T[] values)
    {
        for (var i = 0; i < values.Length; ++i)
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

    public string VictoryCondition(T winner, T loser)
    {
        var enumType = typeof(T);
        var memberInfos =
            enumType.GetMember(Enum.GetName(typeof(T), winner) ?? string.Empty);
        var enumValueMemberInfo = memberInfos.FirstOrDefault(m =>
            m.DeclaringType == enumType);

        var winsAgainstAttributes =
            enumValueMemberInfo?.GetCustomAttributes<WinsAgainstAttribute<T>>();

        if (winsAgainstAttributes == null) return "beats";

        foreach (var winsAgainst in winsAgainstAttributes)
        {
            var victoryCondition = winsAgainst.GetVictoryCondition(loser);
            if (victoryCondition != null)
            {
                return victoryCondition;
            }
        }

        var winsWithAttribute = enumValueMemberInfo?.GetCustomAttribute<WinsWithAttribute>();
        return winsWithAttribute == null ? "beats" : winsWithAttribute.VictoryCondition;
    }
}

public record RpsChoices<T>(T A, T B) where T : Enum;