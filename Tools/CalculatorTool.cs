using System.Text.Json;
using Ai_Calculator.Models;
namespace Ai_Calculator.Tools
{
    public class CalculatorTool: IAgentTool
    {
        public string Name => "calculator";
        public string Description => "Performs arithmetic operations";
        public string Execute(string input)
        {
            var request = JsonSerializer.Deserialize<ToolRequest>(
                input, 
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if(request == null) throw new Exception("Invalid calculator request.");

            var result = Execute(
                request.A,
                request.B,
                request.Operation
            );
            return result.ToString();
        }

        private double Execute(
            double a, 
            double b, 
            string operation)
        {
             return operation switch
            {
                "+" => a + b,
                "-" => a - b,
                "*" => a * b,
                "/" => Divide(a, b),
                _ => throw new Exception($"Unsupported operator '{operation}'.")
            };
        }
        private double Divide(double a, double b)
        {
            if(b == 0) throw new Exception("Cannot divide by zero");
            return a / b;
        }
    }
}