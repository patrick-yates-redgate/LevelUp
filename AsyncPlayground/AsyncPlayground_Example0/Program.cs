Console.WriteLine("Starting Application");
Console.WriteLine(Environment.StackTrace);

Task.Run(async () =>
{
   await Task.Delay(100);
   Console.WriteLine("Waited for a delay");
   Console.WriteLine(Environment.StackTrace);
});

Console.WriteLine("About to wait for user input");
Console.WriteLine(Environment.StackTrace);
Console.ReadLine();