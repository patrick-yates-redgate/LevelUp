// See https://aka.ms/new-console-template for more information

Console.WriteLine("Starting");
await WrapSomethingAsync();
try
{
}
catch (Exception ex)
{
    Console.WriteLine($"Caught when we finally awaited {ex.Message}");
    Console.WriteLine(ex.StackTrace);
}

Console.WriteLine("Finished");

Task WrapSomethingAsync()
{
    try
    {
        return DoSomethingAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Caught {ex.Message}");
    }

    return Task.CompletedTask;
}

async Task DoSomethingAsync()
{
    await Task.Delay(100);
    throw new Exception("Catch me if you can");
}

