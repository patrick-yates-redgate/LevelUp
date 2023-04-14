using static System.String;

namespace FooBarTests;

public class FooBarSingleEventWaitHandleBroken : IFooBar
{
    private string _outputString = Empty;
    private static readonly EventWaitHandle WaitHandle = new AutoResetEvent(true);

    private void Foo()
    {
        for (var i=0; i<100; ++i)
        {
            WaitHandle.WaitOne();

            Console.Write("foo");  
            _outputString += "foo";

            WaitHandle.Set();
        }
    }

    private void Bar()
    {
        for (var i=0; i<100; ++i)
        {
            WaitHandle.WaitOne();
            
            Console.WriteLine("bar");  
            _outputString += "bar" + Environment.NewLine;
            
            WaitHandle.Set();
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