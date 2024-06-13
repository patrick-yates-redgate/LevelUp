// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Task<int> GetNumberAsync() => Task.FromResult(10);

var result = await GetNumberAsync();
