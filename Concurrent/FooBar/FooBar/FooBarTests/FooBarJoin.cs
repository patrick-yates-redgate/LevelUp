using static System.String;

namespace FooBarTests;

public class FooBarJoin : IFooBar
{
    private readonly string[] _outputString = new string[200];

    private void Foo()
    {
        for (var i=0; i<100; ++i)
        {
            _outputString[i * 2] = "foo";
        }
    }

    private void Bar()
    {
        for (var i=0; i<100; ++i)
        {
            _outputString[i * 2 + 1] = "bar" + Environment.NewLine;
        }
    }
    
    public async Task<string> RunAndReturnResult()
    {
        var foo = Task.Run(Foo);
        var bar = Task.Run(Bar);

        await Task.WhenAll(foo, bar);

        return Join(Empty, _outputString);
    }
}