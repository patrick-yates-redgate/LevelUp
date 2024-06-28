// See https://aka.ms/new-console-template for more information

using AiEuro2024App;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

var intelligentAnswersPlease = false;
var model = intelligentAnswersPlease ? "gpt-4o" : "gpt-3.5-turbo";

var builder = Kernel
    .CreateBuilder()
    .AddOpenAIChatCompletion(model, Environment.GetEnvironmentVariable("ApiKey"));
    
builder.Services.AddLogging(services => services.AddConsole().SetMinimumLevel(LogLevel.Trace));

//Build the kernel
var kernel = builder.Build();
var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();


//Add a plugin (defined in this project)
kernel.Plugins.AddFromType<EuroPlugin>("Euros");

//Enable planning
OpenAIPromptExecutionSettings openAiPromptExecutionSettings = new()
{
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
};


var history = new ChatHistory();

string? userInput;
do
{
    Console.Write("User >");
    userInput = Console.ReadLine();
    
    history.AddUserMessage(userInput);

    var result = await chatCompletionService.GetChatMessageContentAsync(
        history,
        openAiPromptExecutionSettings,
        kernel);

    //Print the results
    Console.WriteLine("Euro Assistant > " + result);
    
    history.AddMessage(result.Role, result.Content ?? string.Empty);
    
} while (userInput is not null);