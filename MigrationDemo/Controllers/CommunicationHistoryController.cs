using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MigrationDemo.Filters;
using MigrationDemo.Models;
using MigrationDemo.Services;

namespace MigrationDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunicationHistoryController : ControllerBase
    {
        private readonly CommunicationHistoryService _historyService;

        public CommunicationHistoryController(CommunicationHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpGet]
        [JwtValidation]
        public async Task<IActionResult> GetAllInteractions()
        {
            var interactions = await _historyService.GetAllInteractions();
            return Ok(interactions);
        }

        [HttpGet("{id}")]
        [JwtValidation]
        public async Task<IActionResult> GetInteractionById(int id)
        {
            var interaction = await _historyService.GetInteractionById(id);
            return Ok(interaction);
        }

        [HttpGet("customer/{customerId}")]
        [JwtValidation]
        public async Task<IActionResult> GetInteractionsByCustomerId(int customerId)
        {
            var interactions = await _historyService.GetInteractionsByCustomerId(customerId);
            return Ok(interactions);
        }

        [HttpGet("follow-ups")]
        [JwtValidation]
        public async Task<IActionResult> GetPendingFollowUps()
        {
            var pendingFollowUps = await _historyService.GetPendingFollowUps(DateTime.UtcNow);
            return Ok(pendingFollowUps);
        }

        [HttpPost]
        [JwtValidation]
        public async Task<IActionResult> AddInteraction([FromBody] CommunicationHistory interaction)
        {
            await _historyService.AddInteraction(interaction);
            return Ok(new { Message = "Interaction added successfully." });
        }

        [HttpPut]
        [JwtValidation]
        public async Task<IActionResult> UpdateInteraction([FromBody] CommunicationHistory interaction)
        {
            var updated = await _historyService.UpdateInteraction(interaction);
            if (updated)
                return Ok(new { Message = "Interaction updated successfully." });
            return BadRequest(new { Message = "Failed to update interaction." });
        }

        [HttpDelete("{id}")]
        [JwtValidation]
        public async Task<IActionResult> DeleteInteraction(int id)
        {
            var deleted = await _historyService.DeleteInteraction(id);
            if (deleted)
                return Ok(new { Message = "Interaction deleted successfully." });
            return NotFound(new { Message = "Interaction not found." });
        }

    }
}
