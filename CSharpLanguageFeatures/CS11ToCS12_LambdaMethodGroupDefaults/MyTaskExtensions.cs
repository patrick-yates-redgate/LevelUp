namespace CS11ToCS12_LambdaMethodGroupDefaults;

using System.Runtime.CompilerServices;

public static class MyTaskExtensions
{
    public static TaskAwaiter<T[]> GetAwaiter<T>(this (Task<T>, Task<T>) tasksTuple) =>
        Task.WhenAll(tasksTuple.Item1, tasksTuple.Item2).GetAwaiter();
}