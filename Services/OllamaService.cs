using System.Text.Json;

namespace Ai_Calculator.Services
{
    public class OllamaService
    {
        private readonly HttpClient _httpClient;
        public OllamaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GenerateAsync(string prompt)
        {
            var request = new
            {
                model = "qwen2.5-coder-7b-instruct:latest",
                prompt = prompt,
                stream = false
            };
            var response = await _httpClient.PostAsJsonAsync("/api/generate", request);
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            using var document = JsonDocument.Parse(json);

            return document.RootElement.GetProperty("response").GetString()!;
        }
        
    }
}