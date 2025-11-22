namespace RAG.Ollama.FAQ.Assistant;

public static class Helpers
{
    public static int CountMatches(string paragraph, string query)
    {
        return paragraph.ToLower().Contains(query.ToLower()) ? 1 : 0;
    }
}
