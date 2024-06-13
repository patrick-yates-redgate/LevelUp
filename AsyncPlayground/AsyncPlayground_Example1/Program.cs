
Console.WriteLine("Starting Application");

//Having the await in here means
// that this will execute an async main method
Console.WriteLine(Environment.StackTrace);

Console.WriteLine("Waiting for 1s");
await Task.Delay(1000);
Console.WriteLine("Wait complete");

//The stack here no longer shows the same stack,
// it is a continuation from the task delay
Console.WriteLine(Environment.StackTrace);

Console.ReadLine();