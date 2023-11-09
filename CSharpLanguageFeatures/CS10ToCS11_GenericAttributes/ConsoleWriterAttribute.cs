namespace CS10ToCS11_GenericAttributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ConsoleWriterAttribute<T> : Attribute where T : IConsoleWriter { }