// See https://aka.ms/new-console-template for more information

using Nito.AsyncEx;

Console.WriteLine("Hello, World!");

AsyncContext.Run(async () =>
{
    var myClass = new MyClass();

    myClass.PrintValues();

    await Task.Delay(1000).ConfigureAwait(false);

    myClass.PrintValues(); 
});

class MyClass
{
    [ThreadStatic] private static string? _threadStatic;
    private readonly AsyncLocal<string> _asyncLocal = new();

    public MyClass()
    {
        _threadStatic ??= $"ThreadStatic[{Thread.CurrentThread.ManagedThreadId}]";
        _asyncLocal.Value = $"AsyncLocal[{Thread.CurrentThread.ManagedThreadId}]";
    }

    private readonly ThreadLocal<string> _threadLocal =
        new(() => $"ThreadLocal[{Thread.CurrentThread.ManagedThreadId}]");

    public void PrintValues()
        => Console.WriteLine(
            $"""
             [ThreadStatic]: {_threadStatic}
             ThreadLocal<T>: {_threadLocal.Value}
             AsyncLocal<T>: {_asyncLocal.Value}

             """);
}