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

    Task<string> PerformLongRunningOperationAsync();
}

internal class Library : ILibrary
{
    private async Task<string> GetCoreAsync(bool sync)
    {
        var server = new ThirdPartyLibrary();

        return sync
            ? server.Get("This is a synchronous method")
            : await server.GetAsync("This is an async method");
    }

    public string PerformLongRunningOperation() => GetCoreAsync(false).GetAwaiter().GetResult();

    public Task<string> PerformLongRunningOperationAsync() => GetCoreAsync(true);
}

internal static class LibraryExtensions
{
    public static string PerformLongRunningOperation(this ILibrary library) =>
        library.PerformLongRunningOperationAsync().GetAwaiter().GetResult();
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
    public Task<string> RunAsync() => blockingLibrary.PerformLongRunningOperationAsync();
}

internal class ThirdPartyLibrary
{
    public string Get(string message) => message;
    public Task<string> GetAsync(string message) => Task.FromResult(message);
}