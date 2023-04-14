using static System.String;

namespace FooBarTests;

public class FooBarEventWaitHandleAsync : IFooBar
{
    private string _outputString = Empty;
    private static readonly EventWaitHandle WaitFoo = new AutoResetEvent(true);
    private static readonly EventWaitHandle WaitBar = new AutoResetEvent(false);

    private async Task Foo()
    {
        for (var i=0; i<100; ++i)
        {
            await WaitFoo.WaitOneAsync();
            Console.Write("foo");  
            _outputString += "foo";
            WaitBar.Set();
        }
    }

    private async Task Bar()
    {
        for (var i=0; i<100; ++i)
        {
            await WaitBar.WaitOneAsync();
            Console.WriteLine("bar");  
            _outputString += "bar" + Environment.NewLine;
            WaitFoo.Set();
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