// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

var something = new Something() as ISomething;
something.Run();
await something.RunAsync();
var valueA = something.Get();
var valueB = await something.GetAsync();


public interface ISomething
{
    void Run();
    Task RunAsync();

    string Get();
    Task<string> GetAsync();
}

public class Something : ISomething
{
    public void Run()
    {
    }

    public Task RunAsync()
    {
        throw new NotImplementedException();
    }

    public string Get()
    {
        throw new NotImplementedException();
    }

    public async Task<string> GetAsync()
    {
        await DoSomethingAsync();
        return await CalculateSomethingAsync();
    }

    private string GetString()
        => "String";

    private async Task DoSomethingAsync()
        => await DoSomethingElseAsync(GetString());
    
    private async Task DoSomethingElseAsync(string argument) {}

    private async Task<string> CalculateSomethingAsync()
        => "Result";
}