using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

ChatHistory history = new();

string model = "gpt-3.5-turbo-0125";
string apiKey = "your-api-key";

IKernelBuilder builder = Kernel
    .CreateBuilder()
    .AddOpenAIChatCompletion(model, apiKey);

Kernel kernel = builder.Build();

IChatCompletionService chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

Console.Write("User > ");
string? userInput;

while ((userInput = Console.ReadLine()) != null)
{
    history.AddUserMessage(userInput);

    string aiOutput = string.Empty;

    Console.Write("Assistant >");

    await foreach (var content in chatCompletionService.GetStreamingChatMessageContentsAsync(history))
    {
        Console.Write(content);
        aiOutput += content;
    }

    history.AddUserMessage(aiOutput);

    Console.Write("\nUser > ");
}
