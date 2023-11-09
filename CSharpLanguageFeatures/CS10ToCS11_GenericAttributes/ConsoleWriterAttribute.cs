namespace CS10ToCS11_GenericAttributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ConsoleWriterAttribute : Attribute
{
    public ConsoleWriterAttribute(Type consoleWriterType)
    {
        ConsoleWriterType = consoleWriterType;
    }

    public Type ConsoleWriterType { get; }
}