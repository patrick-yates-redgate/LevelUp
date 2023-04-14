using static System.String;

namespace FooBarTests;

public class FooBarCallback : IFooBar
{
    private string _outputString = Empty;

    private void Foo(Action callback)
    {
        for (var i=0; i<100; ++i)
        {
            Console.Write("foo");  
            _outputString += "foo";
            callback();
        }
    }

    private void Bar()
    {
        Console.WriteLine("bar"); 
        _outputString += "bar" + Environment.NewLine;
    }
    
    public async Task<string> RunAndReturnResult()
    {
        await Task.Run(() => Foo(Bar));
        return _outputString;
    }
}