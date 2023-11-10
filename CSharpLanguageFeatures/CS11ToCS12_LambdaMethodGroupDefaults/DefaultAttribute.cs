namespace CS11ToCS12_LambdaMethodGroupDefaults;

public static class DefaultAttribute
{
    public static void Example()
    {
        var increment = (int number) => number + 1;
        var incrementBy = (int number, int by) => number + by;

        Console.WriteLine(increment(5)); // 6
        Console.WriteLine(incrementBy(5, 2)); // 7
    } 
}