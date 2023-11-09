namespace CS10ToCS11_GenericAttributes;

[ConsoleWriter(typeof(PersonConsoleWriter))]
public class Person
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}

[ConsoleWriter(typeof(AddressConsoleWriter))]
public class Address
{
    public string? City { get; set; }
}

[ConsoleWriter(typeof(DepartmentConsoleWriter))]
public class Department
{
    public string? Name { get; set; }
}