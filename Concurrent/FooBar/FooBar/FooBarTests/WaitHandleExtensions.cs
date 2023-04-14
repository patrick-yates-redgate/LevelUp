namespace FooBarTests;

public static class WaitHandleExtensions
{
    public static async Task WaitOneAsync(this WaitHandle waitHandle)
    {
        await Task.Run(() => waitHandle.WaitOne());
    }
}