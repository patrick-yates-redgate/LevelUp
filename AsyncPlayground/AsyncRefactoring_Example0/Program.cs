// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
ILibrary library = new Library();

var blockingCode = new BlockingCode(library);
Console.WriteLine(blockingCode.Run());

var asyncCode = new AsyncCode(library);
Console.WriteLine(await asyncCode.RunAsync());

internal interface ILibrary
{
    string PerformLongRunningOperation();
}

internal class Library : ILibrary
{
    public string PerformLongRunningOperation()
    {
        Thread.Sleep(1000);
        return "Honestly, this wait is necessary!";
    }
}

internal class BlockingCode(ILibrary library)
{
    public string Run()
    {
        return library.PerformLongRunningOperation();
    }
}

internal class AsyncCode(ILibrary blockingLibrary)
{
    public Task<string> RunAsync()
    {
        var result = blockingLibrary.PerformLongRunningOperation();

        return Task.FromResult(result);
    }
}