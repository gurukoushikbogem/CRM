using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MigrationDemo.Filters;
using MigrationDemo.Models;
using MigrationDemo.Services;

namespace MigrationDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadController : ControllerBase
    {
        private readonly LeadService _leadService;

        public LeadController(LeadService leadService)
        {
            _leadService = leadService;
        }

        [HttpGet]
        [JwtValidation]
        public async Task<IActionResult> GetAllLeads()
        {
            var leads = await _leadService.GetAllLeads();
            return Ok(leads);
        }

        [HttpGet("{id}")]
        [JwtValidation]
        public async Task<IActionResult> GetLeadById(int id)
        {
            var lead = await _leadService.GetLeadById(id);
            return Ok(lead);
        }

        [HttpGet("status/{status}")]
        [JwtValidation]
        public async Task<IActionResult> GetLeadsByStatus(string status)
        {
            var leads = await _leadService.GetLeadsByStatus(status);
            return Ok(leads);
        }

        [HttpPost]
        [JwtValidation]
        public async Task<IActionResult> AddLead(Lead lead)
        {
            await _leadService.AddLead(lead);
            return Ok(new { Message = "Lead added successfully." });
        }

        [HttpPut]
        [JwtValidation]
        public async Task<IActionResult> UpdateLead(Lead lead)
        {
            var updated = await _leadService.UpdateLead(lead);
            if (updated)
                return Ok(new { Message = "Lead updated successfully." });
            return BadRequest(new { Message = "Failed to update lead." });
        }

        [HttpPatch("{id}/status")]
        [JwtValidation]
        public async Task<IActionResult> UpdateLeadStatus(int id,string status)
        {
            var updated = await _leadService.UpdateLeadStatus(id, status);
            if (updated)
                return Ok(new { Message = "Lead status updated successfully." });
            return NotFound(new { Message = "Lead not found." });
        }

        [HttpDelete("{id}")]
        [JwtValidation]
        public async Task<IActionResult> DeleteLead(int id)
        {
            var deleted = await _leadService.DeleteLead(id);
            if (deleted)
                return Ok(new { Message = "Lead deleted successfully." });
            return NotFound(new { Message = "Lead not found." });
        }

        [HttpPost("{id}/convert-to-pipeline")]
        [JwtValidation]
        public async Task<IActionResult> ConvertToPipeline(int id)
        {
            try
            {
                var result = await _leadService.ConvertLeadToPipeline(id);

                if (result)
                    return Ok(new { Message = "Lead successfully converted to pipeline." });

                return BadRequest(new { Message = "Failed to convert lead." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Lead not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
            }
        }

        [HttpGet("unassigned")]
        [JwtValidation]
        public async Task<IActionResult> GetUnassignedLeads()
        {
            var leads = await _leadService.GetUnassignedLeads();
            return Ok(leads);
        }

        [HttpGet("name/{name}")]
        [JwtValidation]
        public async Task<IActionResult> GetLeadByName(string name)
        {
            var lead = await _leadService.GetLeadByName(name);
            return Ok(lead);
        }
    }
}
