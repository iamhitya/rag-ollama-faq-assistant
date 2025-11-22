using OllamaSharp;
using RAG.Ollama.FAQ.Assistant;

Console.WriteLine("Local FAQ Assistant (C#) – Starting…");

// 1) Load data from SampleData class
var paragraphs = SampleData.CompanyPolicies
    .Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries)
    .Select(p => p.Trim())
    .ToList();

// 2) Initialize Ollama client
var client = new OllamaApiClient(new Uri("http://localhost:11434"));
client.SelectedModel = "gpt-oss:20b";

// 3) Chat loop
var chatHistory = new ChatHistory();
Console.WriteLine("\nAsk a company-related question (type 'Exit' to quit):");

while (true)
{
    Console.Write("You: ");
    var userInput = Console.ReadLine()?.Trim();
    if (string.IsNullOrWhiteSpace(userInput)) continue;
    if (userInput.Equals("Exit", StringComparison.OrdinalIgnoreCase)) break;

    // Retrieval step: find best paragraph via keyword match
    var bestParagraph = paragraphs.MaxBy(
        p => Helpers.CountMatches(p, userInput)) ?? "";

    // Build prompt with context + chat history + question
    var prompt = $@"
        You are a helpful assistant. Use the following context to answer the question.
        If you don’t know the answer from the context, respond: 'I don’t know.'

        Context:
        {bestParagraph}

        Chat History:
        {chatHistory.GetHistoryAsContext()}

        Question:
        {userInput}

        Answer:
    ";

    // Generate answer
    string answerText = "";
    await foreach (var chunk in client.GenerateAsync(prompt))
    {
        answerText += chunk.Response;
        Console.Write(chunk.Response);
    }
    Console.WriteLine();

    // Save to chat history
    chatHistory.AddUserMessage(userInput);
    chatHistory.AddAssistantMessage(answerText.Trim());

    Console.WriteLine($"Assistant: {answerText.Trim()}\n");
}

Console.WriteLine("Goodbye!");