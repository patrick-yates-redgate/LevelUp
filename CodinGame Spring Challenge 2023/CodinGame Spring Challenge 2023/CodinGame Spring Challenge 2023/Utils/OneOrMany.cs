using System;
using System.Collections.Generic;
using System.Linq;

namespace CodinGame_Spring_Challenge_2023.Utils;

public class OneOrMany<T> : OneOf<T, IList<T>> where T : IEquatable<T>
{
    public OneOrMany(T value) : base(value)
    {
    }

    public OneOrMany(IList<T> value) : base(value)
    {
    }
    
    public static implicit operator OneOrMany<T>(T value) => new (value);

    public static OneOrMany<T> FromIList(IList<T> value) => new (value);

    public static implicit operator T(OneOrMany<T> oneOf) => oneOf.Value1;

    public static IList<T> ToIList(OneOrMany<T> oneOf) => oneOf.Value2;

    public OneOrMany<T> Merge(OneOrMany<T> other)
    {
        var combinedList = new List<T>();
        Match(
            mySingleResult => combinedList.Add(mySingleResult),
            multipleResult => combinedList.AddRange(multipleResult));

        other.Match(
            mySingleResult =>
            {
                if (combinedList.Contains(mySingleResult)) return;
                combinedList.Add(mySingleResult);
            },
            multipleResult => combinedList
                .AddRange(multipleResult.Where(x => !combinedList.Contains(x))));

        return combinedList.Count == 1 ? new OneOrMany<T>(combinedList[0]) : new OneOrMany<T>(combinedList);
    }

    public bool IsEquivalent(OneOrMany<T> other)
    {
        if (IsValue1 != other.IsValue1) return false;

        return Match(
            singleResult => singleResult.Equals(other.Value1),
            multipleResult => multipleResult.Count == other.Value2.Count && multipleResult.All(x => other.Value2.Contains(x)));
    }
    
    public override string ToString()
    {
        return IsValue1 ? "One["+ Value1 +"]" : "Many[" + string.Join(",", Value2.Select(x => x.ToString())) + "]";
    }
}