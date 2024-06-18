// See https://aka.ms/new-console-template for more information

async Task ExecuteAsync(Func<Task> doSomethingAsync)
{
    Console.WriteLine("Starting Execute");
    await doSomethingAsync();
    Console.WriteLine("Finished Execute");
}

await ExecuteAsync(() => Task.Delay(1000));
await ExecuteAsync(async () => await Task.Delay(1000));


