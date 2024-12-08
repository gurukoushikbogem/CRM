using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MigrationDemo.Filters;
using MigrationDemo.Models;
using MigrationDemo.Services;

namespace MigrationDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesPipelineController : ControllerBase
    {
        private readonly SalesPipelineService _pipelineService;

        public SalesPipelineController(SalesPipelineService pipelineService)
        {
            _pipelineService = pipelineService;
        }

        [HttpGet]
        [JwtValidation]
        public async Task<IActionResult> GetAllPipelines()
        {
            var pipelines = await _pipelineService.GetAllPipelines();
            return Ok(pipelines);
        }

        [HttpGet("{id}")]
        [JwtValidation]
        public async Task<IActionResult> GetPipelineById(int id)
        {
            var pipeline = await _pipelineService.GetPipelineById(id);
            return Ok(pipeline);
        }

        [HttpPost]
        [JwtValidation]
        public async Task<IActionResult> AddPipeline([FromBody] SalesPipeline pipeline)
        {
            await _pipelineService.AddPipeline(pipeline);
            return Ok(new { Message = "Pipeline added successfully." });
        }

        [HttpPut]
        [JwtValidation]
        public async Task<IActionResult> UpdatePipeline([FromBody] SalesPipeline pipeline)
        {
            var updated = await _pipelineService.UpdatePipeline(pipeline);
            if (updated)
                return Ok(new { Message = "Pipeline updated successfully." });
            return BadRequest(new { Message = "Failed to update pipeline." });
        }

        [HttpPatch("{id}/stage")]
        [JwtValidation]
        public async Task<IActionResult> UpdatePipelineStage(int id, [FromBody] string stage)
        {
            var updated = await _pipelineService.UpdatePipelineStage(id, stage);
            if (updated)
                return Ok(new { Message = "Pipeline stage updated successfully." });
            return NotFound(new { Message = "Pipeline not found." });
        }

        [HttpPatch("{id}/status")]
        [JwtValidation]
        public async Task<IActionResult> UpdatePipelineStatus(int id, [FromBody] string status)
        {
            var updated = await _pipelineService.UpdatePipelineStatus(id, status);
            if (updated)
                return Ok(new { Message = "Pipeline status updated successfully." });
            return NotFound(new { Message = "Pipeline not found." });
        }

        [HttpDelete("{id}")]
        [JwtValidation]
        public async Task<IActionResult> DeletePipeline(int id)
        {
            var deleted = await _pipelineService.DeletePipeline(id);
            if (deleted)
                return Ok(new { Message = "Pipeline deleted successfully." });
            return NotFound(new { Message = "Pipeline not found." });
        }

        [HttpPost("pipeline/{pipelineId}/create-opportunity")]
        [JwtValidation]
        public async Task<IActionResult> CreateOpportunityFromPipeline(int pipelineId)
        {
            var createdOpportunity = await _pipelineService.CreateOpportunityFromPipeline(pipelineId);
            return Ok(new { Message = "Opportunity created successfully.", Opportunity = createdOpportunity });
        }

    }
}
