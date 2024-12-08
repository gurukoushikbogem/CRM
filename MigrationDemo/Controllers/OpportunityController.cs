using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MigrationDemo.Filters;
using MigrationDemo.Models;
using MigrationDemo.Services;

namespace MigrationDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpportunityController : ControllerBase
    {
        private readonly OpportunityService _opportunityService;

        public OpportunityController(OpportunityService opportunityService)
        {
            _opportunityService = opportunityService;
        }

        [HttpGet]
        [JwtValidation] 
        public async Task<IActionResult> GetAllOpportunities()
        {
            var opportunities = await _opportunityService.GetAllOpportunities();
            return Ok(opportunities);
        }

        [HttpGet("manager/{managerId}")]
        [JwtValidation]
        public async Task<IActionResult> GetOpportunitiesByManager(int managerId)
        {
            var opportunities = await _opportunityService.GetOpportunitiesByManager(managerId);
            return Ok(opportunities);
        }

        [HttpGet("customer/{customerId}")]
        [JwtValidation]
        public async Task<IActionResult> GetOpportunitiesByCustomer(int customerId)
        {
            var opportunities = await _opportunityService.GetOpportunitiesByCustomer(customerId);
            return Ok(opportunities);
        }

        [HttpGet("stage/{stage}")]
        [JwtValidation]
        public async Task<IActionResult> GetOpportunitiesByStage(string stage)
        {
            var opportunities = await _opportunityService.GetOpportunitiesByStage(stage);
            return Ok(opportunities);
        }

        [HttpGet("health/{health}")]
        [JwtValidation]
        public async Task<IActionResult> GetOpportunitiesByHealth(string health)
        {
            var opportunities = await _opportunityService.GetOpportunitiesByHealth(health);
            return Ok(opportunities);
        }

        [HttpGet("{id}")]
        [JwtValidation]
        public async Task<IActionResult> GetOpportunityById(int id)
        {
            var opportunity = await _opportunityService.GetOpportunityById(id);
            return Ok(opportunity);
        }

        [HttpPost]
        [JwtValidation]
        public async Task<IActionResult> AddOpportunity([FromBody] Oppurtunity opportunity)
        {
            await _opportunityService.AddOpportunity(opportunity);
            return Ok(new { Message = "Opportunity added successfully." });
        }

        [HttpPut]
        [JwtValidation]
        public async Task<IActionResult> UpdateOpportunity([FromBody] Oppurtunity opportunity)
        {
            var updated = await _opportunityService.UpdateOpportunity(opportunity);
            if (updated)
                return Ok(new { Message = "Opportunity updated successfully." });
            return BadRequest(new { Message = "Failed to update opportunity." });
        }

        [HttpDelete("{id}")]
        [JwtValidation]
        public async Task<IActionResult> DeleteOpportunity(int id)
        {
            var deleted = await _opportunityService.DeleteOpportunity(id);
            if (deleted)
                return Ok(new { Message = "Opportunity deleted successfully." });
            return NotFound(new { Message = "Opportunity not found." });
        }

        [HttpPost("{id}/convert")]
        [JwtValidation]
        public async Task<IActionResult> ConvertOpportunityToCustomer(int id)
        {
            var customer = await _opportunityService.ConvertOpportunityToCustomer(id);
            return Ok(customer);
        }

    }
}
