using System.Diagnostics;

namespace CS11ToCS12_LambdaMethodGroupDefaults;

public static class ParallelTasks
{
    public static async Task Example()
    {
        var stopwatch = Stopwatch.StartNew();

/*
 * Simple serialised async tasks
 */
        stopwatch.Start();
        var apple = await MyService.GetResult("Apple");
        var banana = await MyService.GetResult("Banana");
        Console.WriteLine(stopwatch.ElapsedMilliseconds + "ms");

        Console.WriteLine(apple);
        Console.WriteLine(banana);


/*
 * Parallel async tasks
 */
        stopwatch.Restart();
        var result = await Task.WhenAll(
            MyService.GetResult("Apple"),
            MyService.GetResult("Banana"));

        Console.WriteLine(stopwatch.ElapsedMilliseconds + "ms");
        result.ToList().ForEach(Console.WriteLine);


/*
 * Parallel async tasks using extension method
 */
        stopwatch.Restart();
        result = await(
            MyService.GetResult("Apple"),
            MyService.GetResult("Banana"));

        Console.WriteLine(stopwatch.ElapsedMilliseconds + "ms");
        result.ToList().ForEach(Console.WriteLine);


/*
 * Parallel async lambda
 */
        var parallelAsync = (params Task<string>[] tasks) => Task.WhenAll(tasks);
        stopwatch.Restart();
        result = await parallelAsync(
            MyService.GetResult("Apple"),
            MyService.GetResult("Banana"),
            MyService.GetResult("Coconut"));

        Console.WriteLine(stopwatch.ElapsedMilliseconds + "ms");
        result.ToList().ForEach(Console.WriteLine);
    }
}