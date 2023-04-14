namespace FooBarTests;

public class FooBar : IFooBar
{
    private object lockOuter = new object();
    private object lockInner = new object();
    
    private string outputString = String.Empty;
    
    private async void Foo()
    {
        for (var i = 0; i < 100; ++i)
        {
            lock (lockOuter)
            {
                Console.Write("foo");  
                outputString += "foo";
            }
        }
    }

    private async void Bar()
    {
        for (var i = 0; i < 100; ++i)
        {
            lock (lockOuter)
            {
                Console.WriteLine("bar"); 
                outputString += "bar" + Environment.NewLine;
            }
        }
    }

    public async Task<string> RunAndReturnResult()
    {
        var foo = Task.Run(Foo);
        var bar = Task.Run(Bar);

        await Task.WhenAll(foo, bar);

        return outputString;
    }
}