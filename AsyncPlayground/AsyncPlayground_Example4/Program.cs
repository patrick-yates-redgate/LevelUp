// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

static void ThreadDebug(string name) => Console.WriteLine($"{name} => ManagedThreadId: {Environment.CurrentManagedThreadId} CurrentId: {Task.CurrentId}");

ThreadDebug("[Start]");

await Task.Run(async () =>
{
    ThreadDebug("    [Start]");

    await Task.Yield();

    ThreadDebug("    [After Yield]");

    await Task.Run(async () =>
    {
        ThreadDebug("        [Start]");

        await Task.Delay(1000);

        ThreadDebug("        [End]");
    });

    ThreadDebug("    [End]");
});

await Task.Run(async () =>
{
    ThreadDebug("    [Start]");

    await Task.Delay(1000);

    ThreadDebug("    [After Delay]");

    await Task.Run(async () =>
    {
        ThreadDebug("        [Start]");

        await Task.Delay(1000);

        ThreadDebug("        [End]");
    });

    ThreadDebug("    [End]");
});

ThreadDebug("[End]");