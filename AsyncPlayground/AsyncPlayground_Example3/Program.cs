// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

async Task<int> GetThreadIdAsync()
{
    //await Task.Delay(1000); //Without the delay everything runs on the 1st thread
    return Thread.CurrentThread.ManagedThreadId;
}

var firstTask = GetThreadIdAsync();
var secondResult = await GetThreadIdAsync();
var firstResult = await firstTask;

Console.WriteLine(firstResult);
Console.WriteLine(secondResult);