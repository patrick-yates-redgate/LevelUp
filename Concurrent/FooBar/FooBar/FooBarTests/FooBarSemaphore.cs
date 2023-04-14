using static System.String;

namespace FooBarTests;

public class FooBarSemaphore : IFooBar
{
    private string _outputString = Empty;
    private static readonly Semaphore SemaFoo = new Semaphore(1, 1);
    private static readonly Semaphore SemaBar = new Semaphore(0, 1);

    private void Foo()
    {
        for (var i=0; i<100; ++i)
        {
            SemaFoo.WaitOne();
            Console.Write("foo");  
            _outputString += "foo";
            SemaBar.Release();
        }
    }

    private void Bar()
    {
        for (var i=0; i<100; ++i)
        {
            SemaBar.WaitOne();
            Console.WriteLine("bar");  
            _outputString += "bar" + Environment.NewLine;
            SemaFoo.Release();
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