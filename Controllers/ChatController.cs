using Ai_Calculator.Agents;
using Microsoft.AspNetCore.Mvc;

namespace Ai_Calculator.Controllers
{
    [ApiController]
    [Route("chat")]
    public class ChatController: ControllerBase
    {
        private readonly AgentService _agent;
        public ChatController(AgentService agent) => _agent = agent;

        [HttpGet]
        public async Task<IActionResult> Ask(string prompt)
        {
            var answer = await _agent.RunAsync(prompt);
            return Ok(answer);
        }
    }
}