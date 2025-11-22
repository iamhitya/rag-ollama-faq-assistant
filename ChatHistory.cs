namespace RAG.Ollama.FAQ.Assistant;

public class ChatHistory
{
    private readonly List<string> _messages = new();
    public void AddUserMessage(string msg) => _messages.Add($"User: {msg}");
    public void AddAssistantMessage(string msg) => _messages.Add($"Assistant: {msg}");
    public string GetHistoryAsContext(int max = 10)
        => string.Join(Environment.NewLine, _messages.Skip(Math.Max(0, _messages.Count - max)));
}