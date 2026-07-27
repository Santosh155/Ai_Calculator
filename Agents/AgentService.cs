using System.Text.Json;
using Ai_Calculator.Models;
using Ai_Calculator.Services;
using Ai_Calculator.Tools;

namespace Ai_Calculator.Agents
{
    public class AgentService
    {
        private readonly OllamaService _ollama;
        private readonly ToolRegistry _registry;
        public AgentService(OllamaService ollama, ToolRegistry registry)
        {
            _registry = registry;
            _ollama = ollama;
        } 

        public async Task<string> RunAsync(string userMessage)
        {
            var prompt = $$"""
            You are an AI assistant.

            Available tools: 

            calculator 

            It accepts: 
            a
            b
            operation
            If the user asks a math question, output ONLY raw JSON.
            Rules:
            - Do not use markdown.
            - Do not use ```json.
            - Do not add explanations.
            - Start with { and end with }.

            Example:

            {
            "tool":"calculator",
            "a":10,
            "b":5,
            "operation":"*"
            }

            Otherwise answer normally.

            User:
            {{userMessage}}
            """;
            
            var response = await _ollama.GenerateAsync(prompt);
            response = response
                .Replace("```json", "")
                .Replace("```", "")
                .Trim();

            if(response.Contains("\"tool\""))
            {
                var toolRequest = JsonSerializer.Deserialize<ToolRequest>(
                    response,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                if(toolRequest == null)
                {
                    return $"Tool '{toolRequest.Tool}' not found";
                }
                var tool = _registry.Get(toolRequest.Tool);
                var result = tool.Execute(response);
                var finalPrompt = $"""
                You are an AI assistant.

                User asked:

                {userMessage}

                Tool result:

                {result}

                Explain the answer naturally.
                """;
                return await _ollama.GenerateAsync(finalPrompt);
                

            }
            return response;
        }

       
    }
}