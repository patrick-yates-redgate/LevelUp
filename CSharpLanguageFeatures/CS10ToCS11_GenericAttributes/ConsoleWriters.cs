namespace CS10ToCS11_GenericAttributes;

public interface IConsoleWriter
{
}

public interface IConsoleWriter<in T> : IConsoleWriter
{
    static abstract void Write(T obj);
}

public class PersonConsoleWriter : IConsoleWriter<Person>
{
    public static void Write(Person person)
    {
        Console.WriteLine($"Person => FirstName:{person.FirstName} LastName:{person.LastName}");
    }
}

public class AddressConsoleWriter : IConsoleWriter<Address>
{
    public static void Write(Address address)
    {
        Console.WriteLine($"Address => City:{address.City}");
    }
}

public class DepartmentConsoleWriter : IConsoleWriter<Department>
{
    public static void Write(Department department)
    {
        Console.WriteLine($"Department => Name:{department.Name}");
    }
}