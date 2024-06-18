Console.WriteLine("Hello, Level Up!");

async Task MethodA() => await MethodB();
Task MethodB() => MethodC();
async Task MethodC() => await MethodD();
Task MethodD() => MethodE();

async Task MethodE()
{
    await Task.Delay(1000);
    
    Console.WriteLine("Done Inside Method E");
}

await MethodA();

Console.WriteLine("Done");