using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MigrationDemo.Filters;
using MigrationDemo.Models;
using MigrationDemo.Services;

namespace MigrationDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly CampaignService _campaignService;

        public CampaignController(CampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        [HttpGet]
        [JwtValidation]
        public async Task<IActionResult> GetAllCampaigns()
        {
            var campaigns = await _campaignService.GetAllCampaigns();
            return Ok(campaigns);
        }

        [HttpGet("status/{status}")]
        [JwtValidation]
        public async Task<IActionResult> GetCampaignsByStatus(string status)
        {
            var campaigns = await _campaignService.GetCampaignsByStatus(status);
            return Ok(campaigns);
        }

        [HttpGet("segment/{targetSegment}")]
        [JwtValidation]
        public async Task<IActionResult> GetCampaignsByTargetSegment(string targetSegment)
        {
            var campaigns = await _campaignService.GetCampaignsByTargetSegment(targetSegment);
            return Ok(campaigns);
        }

        [HttpGet("{id}")]
        [JwtValidation]
        public async Task<IActionResult> GetCampaignById(int id)
        {
            var campaign = await _campaignService.GetCampaignById(id);
            return Ok(campaign);
        }

        [HttpPost]
        [JwtValidation]
        public async Task<IActionResult> AddCampaign([FromBody] Campaign campaign)
        {
            await _campaignService.AddCampaign(campaign);
            return Ok(new { Message = "Campaign created successfully." });
        }

        [HttpPut]
        [JwtValidation]
        public async Task<IActionResult> UpdateCampaign([FromBody] Campaign campaign)
        {
            var updated = await _campaignService.UpdateCampaign(campaign);
            if (updated)
                return Ok(new { Message = "Campaign updated successfully." });
            return BadRequest(new { Message = "Failed to update campaign." });
        }

        [HttpDelete("{id}")]
        [JwtValidation]
        public async Task<IActionResult> DeleteCampaign(int id)
        {
            var deleted = await _campaignService.DeleteCampaign(id);
            if (deleted)
                return Ok(new { Message = "Campaign deleted successfully." });
            return NotFound(new { Message = "Campaign not found." });
        }

    }
}
