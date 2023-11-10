namespace CS11ToCS12_LambdaMethodGroupDefaults;

public static class DefaultAttribute
{
    public static void Example()
    {
        var increment = (int number, int by = 1) => number + by;

        Console.WriteLine(increment(5)); // 6
        Console.WriteLine(increment(5, 2)); // 7
    } 
}