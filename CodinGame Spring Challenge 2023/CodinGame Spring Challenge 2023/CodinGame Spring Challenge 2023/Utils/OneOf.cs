using System;

namespace CodinGame_Spring_Challenge_2023.Utils;

public class OneOf<T, U>
{
    private readonly T? _value1;
    private readonly U? _value2;
    
    public OneOf(T value)
    {
        _value1 = value;
    }
    
    public OneOf(U value)
    {
        _value2 = value;
    }
    
    public bool IsValue1 => _value1 != null;
    public bool IsValue2 => _value2 != null;
    
    public T Value1 => _value1!;
    public U Value2 => _value2!;
    
    public static implicit operator OneOf<T, U>(T value) => new (value);
    public static implicit operator OneOf<T, U>(U value) => new (value);
    
    public static implicit operator T(OneOf<T, U> oneOf) => oneOf.Value1;
    public static implicit operator U(OneOf<T, U> oneOf) => oneOf.Value2;
    
    public bool Is<TV>()
    {
        return typeof(TV) == typeof(T) == IsValue1;
    }
    
    public override string ToString()
    {
        return IsValue1 ? Value1.ToString() : Value2.ToString();
    }
    
    public override bool Equals(object? obj)
    {
        if (obj is OneOf<T, U> other)
        {
            if (IsValue1 && other.IsValue1)
            {
                return Value1.Equals(other.Value1);
            }
            else if (IsValue2 && other.IsValue2)
            {
                return Value2.Equals(other.Value2);
            }
        }
        
        return false;
    }
    
    public override int GetHashCode()
    {
        return IsValue1 ? Value1.GetHashCode() : Value2.GetHashCode();
    }
    
    public static bool operator ==(OneOf<T, U> left, OneOf<T, U> right)
    {
        return left.Equals(right);
    }
    
    public static bool operator !=(OneOf<T, U> left, OneOf<T, U> right)
    {
        return !(left == right);
    }
    
    public static bool operator ==(OneOf<T, U> left, T right)
    {
        return left.Equals(right);
    }
    
    public static bool operator !=(OneOf<T, U> left, T right)
    {
        return !(left == right);
    }
    
    public static bool operator ==(OneOf<T, U> left, U right)
    {
        return left.Equals(right);
    }
    
    public static bool operator !=(OneOf<T, U> left, U right)
    {
        return !(left == right);
    }
    
    public static bool operator ==(T left, OneOf<T, U> right)
    {
        return right.Equals(left);
    }
    
    public static bool operator !=(T left, OneOf<T, U> right)
    {
        return !(left == right);
    }
    
    public static bool operator ==(U left, OneOf<T, U> right)
    {
        return right.Equals(left);
    }
    
    public static bool operator !=(U left, OneOf<T, U> right)
    {
        return !(left == right);
    }
    
    public void Match(Action<T> value1Action, Action<U> value2Action)
    {
        if (IsValue1)
        {
            value1Action(Value1);
        }
        else
        {
            value2Action(Value2);
        }
    }
    
    public TV Match<TV>(Func<T, TV> value1Action, Func<U, TV> value2Action)
    {
        return IsValue1 ? value1Action(Value1) : value2Action(Value2);
    }
}