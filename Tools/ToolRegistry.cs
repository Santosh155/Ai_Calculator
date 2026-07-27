namespace Ai_Calculator.Tools
{
    public class ToolRegistry
    {
        private readonly Dictionary<string, IAgentTool> _tools;
        public ToolRegistry(IEnumerable<IAgentTool> tools)
        {
            _tools = tools.ToDictionary(x=>x.Name, x=>x);
        }
        public IAgentTool? Get(string name)
        {
            _tools.TryGetValue(
                name, 
                out var tool
            );
            return tool;
        }
    }
}