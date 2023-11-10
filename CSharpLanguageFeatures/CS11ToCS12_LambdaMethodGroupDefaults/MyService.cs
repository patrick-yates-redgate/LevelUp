namespace CS11ToCS12_LambdaMethodGroupDefaults;

public static class MyService
{
    public static async Task<string> GetResult(string input)
    {
        await Task.Delay(1000);

        return $"Result: {input}";
    }
}