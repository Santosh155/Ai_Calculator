namespace Ai_Calculator.Models
{
    public class ToolRequest
    {
        public string Tool {get; set;} = "";
        public double A {get; set;} 
        public double B {get; set;}
        public string Operation {get; set;} = "";
    }
}