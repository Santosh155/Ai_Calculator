using Ai_Calculator.Services;
using Ai_Calculator.Agents;
using Ai_Calculator.Tools;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient<OllamaService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:11434");
});
builder.Services.AddScoped<AgentService>();
builder.Services.AddScoped<IAgentTool, CalculatorTool>();
builder.Services.AddScoped<ToolRegistry>();


builder.Services.AddControllers();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();
app.UseHttpsRedirection();
app.Run();
