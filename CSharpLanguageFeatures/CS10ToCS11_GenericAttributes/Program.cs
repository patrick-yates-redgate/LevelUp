//Taken from https://www.thomasclaudiushuber.com/2023/01/17/csharp-11-generic-attributes/

using CS10ToCS11_GenericAttributes;

var person = new Person { FirstName = "Thomas", LastName = "Huber" };
var address = new Address { City = "Frankfurt" };
var department = new Department { Name = "Marketing" };

WriteObjectToConsole(person);
WriteObjectToConsole(address);
WriteObjectToConsole(department);

Console.ReadLine();

void WriteObjectToConsole(object obj)
{
    var wasWritten = false;
    var attributes = obj.GetType().GetCustomAttributes(
        typeof(ConsoleWriterAttribute), inherit: false);

    if (attributes.Length == 1
        && attributes[0] is ConsoleWriterAttribute consoleWriterAttribute)
    {
        var consoleWriterType = consoleWriterAttribute.ConsoleWriterType;
        var consoleWriter = Activator.CreateInstance(consoleWriterType)
            as IConsoleWriter;
        if (consoleWriter is not null)
        {
            consoleWriter.Write(obj);
            wasWritten = true;
        }
    }

    if (!wasWritten)
    {
        Console.WriteLine(obj);
    }
}