using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MigrationDemo.Filters;
using MigrationDemo.Models;
using MigrationDemo.Services;

namespace MigrationDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ReportService _reportService;

        public ReportController(ReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        [JwtValidation]
        public async Task<IActionResult> GetAllReports()
        {
            var reports = await _reportService.GetAllReports();
            return Ok(reports);
        }

        [HttpGet("{id}")]
        [JwtValidation]
        public async Task<IActionResult> GetReportById(int id)
        {
            var report = await _reportService.GetReportById(id);
            return Ok(report);
        }

        [HttpGet("type/{type}")]
        [JwtValidation]
        public async Task<IActionResult> GetReportsByType(string type)
        {
            var reports = await _reportService.GetReportsByType(type);
            return Ok(reports);
        }

        [HttpPost]
        [JwtValidation]
        public async Task<IActionResult> AddReport([FromBody] Report report)
        {
            await _reportService.AddReport(report);
            return Ok(new { Message = "Report created successfully." });
        }

        [HttpDelete("{id}")]
        [JwtValidation]
        public async Task<IActionResult> DeleteReport(int id)
        {
            var deleted = await _reportService.DeleteReport(id);
            if (deleted)
                return Ok(new { Message = "Report deleted successfully." });
            return NotFound(new { Message = "Report not found." });
        }

    }
}
