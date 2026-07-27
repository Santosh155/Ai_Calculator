namespace Ai_Calculator.Tools
{
    public interface IAgentTool
    {
        string Name { get; }
        string Description { get; }
        string Execute(string input);
    }
}