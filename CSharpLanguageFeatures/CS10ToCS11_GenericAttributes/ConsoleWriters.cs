namespace CS10ToCS11_GenericAttributes;

public interface IConsoleWriter
{
    void Write(object obj);
}

public class PersonConsoleWriter : IConsoleWriter
{
    public void Write(object obj)
    {
        var person = (Person)obj;
        Console.WriteLine($"Person => FirstName:{person.FirstName} LastName:{person.LastName}");
    }
}

public class AddressConsoleWriter : IConsoleWriter
{
    public void Write(object obj)
    {
        var address = (Address)obj;
        Console.WriteLine($"Address => City:{address.City}");
    }
}

public class DepartmentConsoleWriter : IConsoleWriter
{
    public void Write(object obj)
    {
        var department = (Department)obj;
        Console.WriteLine($"Department => Name:{department.Name}");
    }
}