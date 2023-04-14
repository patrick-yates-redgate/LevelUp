using static System.String;

namespace FooBarTests;

public class FooBarSingleEventWaitHandleSyncStart : IFooBar
{
    private string _outputString = Empty;
    private static readonly EventWaitHandle WaitHandleStartedFoo = new ManualResetEvent(false);

    private static readonly EventWaitHandle WaitFoo = new AutoResetEvent(false);
    private static readonly EventWaitHandle WaitBar = new AutoResetEvent(false);

    private void Foo()
    {
        WaitHandleStartedFoo.Set();

        for (var i = 0; i < 100; ++i)
        {
            WaitFoo.WaitOne();

            Console.Write("foo");
            _outputString += "foo";

            WaitBar.Set();
        }
    }

    private void Bar()
    {
        for (var i = 0; i < 100; ++i)
        {
            WaitBar.WaitOne();

            Console.WriteLine("bar");
            _outputString += "bar" + Environment.NewLine;

            WaitFoo.Set();
        }
    }

    public async Task<string> RunAndReturnResult()
    {
        var foo = Task.Run(Foo);
        WaitHandleStartedFoo.WaitOne();
        WaitFoo.Set();

        var bar = Task.Run(Bar);

        await Task.WhenAll(foo, bar);

        return _outputString;
    }
}