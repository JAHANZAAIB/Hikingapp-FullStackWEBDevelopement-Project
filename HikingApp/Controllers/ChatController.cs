using Microsoft.AspNetCore.Mvc;
using HikingApp.Services;

namespace HikingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IAIService _aiService;

        public ChatController(IAIService aiService)
        {
            _aiService = aiService;
        }

        [HttpPost]
        public async Task<IActionResult> AskBot([FromBody] ChatRequest request)
        {
            if (string.IsNullOrEmpty(request.Message)) return BadRequest("Message cannot be empty.");

            var botResponse = await _aiService.GetChatResponseAsync(request.Message);
            return Ok(new { response = botResponse });
        }
    }

    public class ChatRequest
    {
        public string Message { get; set; }
    }
}