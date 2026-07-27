# AI Calculator Agent (.NET 10 + Ollama + Qwen)

A simple agentic AI application built from scratch using **ASP.NET Core .NET 10**, **Ollama**, and **Qwen2.5-Coder-7B-Instruct**.

The purpose of this project is to understand the fundamentals of an AI agent:

- Connecting an LLM with an API
- Letting the AI decide when to use a tool
- Executing tools dynamically
- Building an extensible agent architecture

## Tech Stack

- .NET 10
- ASP.NET Core Web API
- C#
- Ollama
- Qwen2.5-Coder-7B-Instruct

## Architecture

Current flow:

```
User
 |
 v
ChatController
 |
 v
AgentService
 |
 v
ToolRegistry
 |
 v
CalculatorTool
 |
 v
Result
 |
 v
Qwen
 |
 v
Final Answer
```

## Features Implemented

### AI Agent

`AgentService` handles the agent workflow:

- Sends user questions to Qwen
- Detects when a tool is needed
- Reads the tool request from the AI response
- Executes the correct tool
- Sends the result back to the AI for a final response

### Tool System

Tools implement a common interface:

```csharp
public interface IAgentTool
{
    string Name { get; }
    string Description { get; }
    string Execute(string input);
}
```

A `ToolRegistry` stores available tools and allows the agent to find them dynamically.

### Calculator Tool

The current agent has one tool:

**CalculatorTool**

Supports:

- `+`
- `-`
- `*`
- `/`

Example AI tool request:

```json
{
    "tool": "calculator",
    "a": 25,
    "b": 8,
    "operation": "*"
}
```

Result:

```
200
```

## Project Structure

```
Ai_Calculator

├── Agents
│   └── AgentService.cs
│
├── Controllers
│   └── ChatController.cs
│
├── Models
│   └── ToolRequest.cs
│
├── Services
│   └── OllamaService.cs
│
├── Tools
│   ├── IAgentTool.cs
│   ├── CalculatorTool.cs
│   └── ToolRegistry.cs
│
└── Program.cs
```

## Running the Project

### Requirements

Install:

- [.NET 10 SDK](https://dotnet.microsoft.com/)
- [Ollama](https://ollama.com/)

Download the model:

```bash
ollama pull qwen2.5-coder:7b-instruct
```

Start Ollama:

```bash
ollama serve
```

Run the API:

```bash
dotnet run
```

### Testing

Example request:

```
GET /chat?prompt=What is 25 * 8?
```

Expected response:

```
25 multiplied by 8 equals 200.
```

## Current Limitations

Currently the agent supports:

- ✅ Local LLM execution
- ✅ Calculator tool usage
- ✅ Tool registry architecture

Not implemented yet:

- Conversation memory
- Database integration
- Multiple tools
- Tool chaining
- Advanced planning

## Goal

This project is a learning journey toward building a complete agentic AI system where an AI model can reason, select tools, execute actions, and interact with external systems.
