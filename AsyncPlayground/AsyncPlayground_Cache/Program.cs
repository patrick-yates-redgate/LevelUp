// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

var cache = new Dictionary<string, string>();

Console.WriteLine(await GetOrCreateAsync("Something"));

async ValueTask<string> GetOrCreateAsync(string resource)
{
    if (cache.TryGetValue(resource, out var cacheResult))
    {
        return cacheResult;
    }

    return await CreateAsync(resource);
}

async Task<string> CreateAsync(string resource)
{
    await Task.Delay(1000);

    return resource;
}