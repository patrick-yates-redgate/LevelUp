namespace CS11ToCS12_LambdaMethodGroupDefaults;

public static class LambdaParams
{
    public static void Example()
    {
        var createAdult = (string name) => new Adult(name, Array.Empty<Child>());
        var createAdultWithChildren = (string name, Child[] children) => new Adult(name, children);

        var adultB = createAdult("Julia");
        var adultA = createAdultWithChildren("Thomas", new []{ new Child("Max"), new Child("Lisa") });

        Console.WriteLine(adultA);
        Console.WriteLine(adultB);
    }
    
    record Human(string Name);
    record Child(string Name) : Human(Name);

    record Adult(string Name, Child[] Children) : Human(Name)
    {
        public override string ToString() => $"{Name} has {Children.Length} children";
    }
}