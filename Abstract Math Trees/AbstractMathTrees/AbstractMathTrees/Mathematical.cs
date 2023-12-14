using OneOf;

public static class Mathematical
{
    public static Operation? parse(string input)
    {
        var stack = CreateStack(input);

        var operation = stack.Pop() as OperationToken;
        switch (operation.Value)
        {
            case "+":
                var right = (stack.Pop() as OperandToken)!;
                var left = (stack.Pop() as OperandToken)!;

                return new Operation(new Add(), left, right);
            default:
                throw new Exception("Unknown operation");
        }

        return null;
    }

    private static Stack<IToken> CreateStack(string input)
    {
        var stack = new Stack<IToken>();
        var splitInput = input.Split(' ');

        foreach (var character in splitInput)
        {
            stack.Push(ConvertTo(character));
        }

        return stack;
    }

    private static IToken ConvertTo(string character)
    {
        if (int.TryParse(character, out var value))
        {
            return new OperandToken(value);
        }

        return new OperationToken(character);
    }
}

public record Operation(IMethod Method, OneOf<OperandToken, Operation> Left, OneOf<OperandToken, Operation> Right);

public interface IMethod
{
}

public class Add : IMethod
{
}

public class Mul : IMethod
{
}

public interface IToken
{
}

public class OperandToken(int value) : IToken
{
    public int Value { get; set; } = value;
}

public class OperationToken(string value) : IToken
{
    public string Value { get; } = value;

    public override string ToString() => $"({Value})";
}