# RAG Ollama FAQ Assistant

Minimal RAG FAQ assistant using .NET 10 and Ollama.

## Features
- Loads FAQ/sample data
- Embeds text (local embeddings model via Ollama)
- Find best paragraph via keyword match
- Chat endpoint combining user question + retrieved context
- Simple chat history in memory

## Prerequisites
- .NET 10 SDK
- Ollama installed and a local model pulled (e.g. `ollama pull llama3`)

## Run
```
dotnet run
```
Then ask questions in the console. The app retrieves relevant FAQ entries and feeds them to the model.