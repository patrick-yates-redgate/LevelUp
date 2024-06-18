// See https://aka.ms/new-console-template for more information

Nito.AsyncEx.AsyncContext.Run(async () =>
{
    Console.WriteLine($"[{Nito.AsyncEx.AsyncContext.Current?.Id}] [{Thread.CurrentThread.ManagedThreadId}]");
    await Task.Delay(1000); //ConfigureAwait(true); by default
    Console.WriteLine($"[{Nito.AsyncEx.AsyncContext.Current?.Id}] [{Thread.CurrentThread.ManagedThreadId}]");
    await Task.Delay(1000).ConfigureAwait(false);
    Console.WriteLine($"[{Nito.AsyncEx.AsyncContext.Current?.Id}] [{Thread.CurrentThread.ManagedThreadId}]");
});