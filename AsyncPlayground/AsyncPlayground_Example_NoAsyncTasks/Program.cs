// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

Task<string> DoSomethingAsync()
{
    Console.WriteLine("I am doing something async I think?");

    return Task.FromResult("Return value");
}


Console.WriteLine("Before async");
var task = DoSomethingAsync();
Console.WriteLine("After async");


Console.WriteLine($"Awaited Task value: {await task}");
Console.WriteLine($"We can await that value again: {await task}");
Console.WriteLine($"Or access via GetAwaiter(): {task.GetAwaiter().GetResult()}");

Console.ReadLine();