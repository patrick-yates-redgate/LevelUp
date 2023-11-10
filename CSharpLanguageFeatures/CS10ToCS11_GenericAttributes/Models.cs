namespace CS10ToCS11_GenericAttributes;

[ConsoleWriter<PersonConsoleWriter>]
public class Person
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}

[ConsoleWriter<AddressConsoleWriter>]
public class Address
{
    public string? City { get; set; }
}

[ConsoleWriter<DepartmentConsoleWriter>]
public class Department
{
    public string? Name { get; set; }
}