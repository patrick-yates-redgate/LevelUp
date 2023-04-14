using static System.String;

namespace FooBarTests;

public class FooBarSingleEventWaitHandle : IFooBar
{
    private string _outputString = Empty;
    private static readonly EventWaitHandle WaitHandle = new AutoResetEvent(false);
    private static readonly EventWaitHandle WaitHandleFoo = new AutoResetEvent(false);
    private static readonly EventWaitHandle WaitHandleBar = new AutoResetEvent(false);
    private readonly object lockObj = new();
    private bool _isItFoo = true;
    
    private void Foo()
    {
        WaitHandleFoo.WaitOne();
        WaitHandleBar.Set();
        
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
        WaitHandleFoo.Set();
        WaitHandleBar.WaitOne();
        
        for (var i=0; i<100; ++i)
        {
            WaitHandle.Set();
            WaitHandle.WaitOne();
            
            Console.WriteLine("bar");  
            _outputString += "bar" + Environment.NewLine;
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