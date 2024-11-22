/*
Program to take the output of !dumpasync in windbg and convert it to a graph

0:034> !dumpasync
STACK 1
<< Awaiting: 000002d7bd9ccb20 00007ffbe98f9010 System.Runtime.CompilerServices.ConfiguredTaskAwaitable+ConfiguredTaskAwaiter >>
  000002d7bd9ccac0 00007ffbe99a85b0 (1) Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing.SelfDiagnostics.SelfDiagnosticsConfigRefresher+<Worker>d__12 @ 7ffbe9418ba0
    000002d7bd9cc6b8 00007ffbe99a39a8 ( ) System.Threading.Tasks.UnwrapPromise<System.Threading.Tasks.VoidTaskResult>

 */


using System.Text;
using System.Text.Json;

var readAllLinesFromInputFile =  File.ReadAllLines("../../../input.txt");

var stacks = new List<AsyncStack>();

AsyncStack? previousAsyncStack = null;

var index = 0;

//Loop through each row and parse if it does not start with STACK
foreach (var line in readAllLinesFromInputFile)
{
    ++index;
    
    if (line.StartsWith("STACK") || string.IsNullOrWhiteSpace(line))
    {
        if (previousAsyncStack != null)
        {
            stacks.Add(previousAsyncStack);
            previousAsyncStack = null;
        }
        
        continue;
    }
    
    //If line starts with << Awaiting: then set isAwaiting to true
    var isAwaiting = line.StartsWith("<< Awaiting:");
    var trimmedLine = (isAwaiting ? line.Substring("<< Awaiting:".Length) : line).Trim();
    var split = trimmedLine.Split(' ');
    var objectAddress = split[0];
    var methodAddress = split[1];
    var description = trimmedLine.IndexOf(')') == -1 ? split[3] : trimmedLine.Substring(trimmedLine.IndexOf(')') + 2);
    var asyncAddressStartIndex = trimmedLine.IndexOf("@ ", StringComparison.Ordinal);
    var asyncAddress = asyncAddressStartIndex > -1 ? trimmedLine.Substring(asyncAddressStartIndex + 2) : null;
    var record = new AsyncStack(objectAddress, methodAddress, description, asyncAddress, isAwaiting, previousAsyncStack);
        
    previousAsyncStack = record;
}

//Serialize to JSON and write to output file
var json = JsonSerializer.Serialize(stacks, new JsonSerializerOptions { WriteIndented = true });
File.WriteAllText("../../../output.json", json);

record AsyncStack(string objectAddress, string methodAddress, string description, string? asyncAddress, bool isAwaiting, AsyncStack? parent);