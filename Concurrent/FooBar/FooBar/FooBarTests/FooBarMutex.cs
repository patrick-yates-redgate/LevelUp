using static System.String;

namespace FooBarTests;

public class FooBarMutex : IFooBar
{
    private string _outputString = Empty;
    private static readonly Mutex _mutex = new Mutex();

    private void Foo()
    {
        for (var i=0; i<100; ++i)
        {
            _mutex.WaitOne();
            Console.Write("foo");  
            _outputString += "foo";
            _mutex.ReleaseMutex();
        }
    }

    private void Bar()
    {
        for (var i=0; i<100; ++i)
        {
            Console.WriteLine("bar");  
            _outputString += "bar" + Environment.NewLine;
            _mutex.ReleaseMutex();
            _mutex.WaitOne();
        }
    }
    
    public async Task<string> RunAndReturnResult()
    {
        var foo = Task.Run(Foo);
        var bar = Task.Run(Bar);

        await Task.WhenAll(foo, bar);

        return _outputString;
    }
}