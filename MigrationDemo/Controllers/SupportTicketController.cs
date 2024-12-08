using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MigrationDemo.Filters;
using MigrationDemo.Models;
using MigrationDemo.Services;

namespace MigrationDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportTicketController : ControllerBase
    {
        private readonly SupportTicketService _ticketService;

        public SupportTicketController(SupportTicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet("status/{status}")]
        [JwtValidation]
        public async Task<IActionResult> GetTicketsByStatus(string status)
        {
            var tickets = await _ticketService.GetTicketsByStatus(status);
            return Ok(tickets);
        }

        [HttpGet("customer/{customerId}")]
        [JwtValidation]
        public async Task<IActionResult> GetTicketsByCustomer(int customerId)
        {
            var tickets = await _ticketService.GetTicketsByCustomer(customerId);
            return Ok(tickets);
        }

        [HttpGet("assigned/{userId}")]
        [JwtValidation]
        public async Task<IActionResult> GetTicketsByAssignedUser(int userId)
        {
            var tickets = await _ticketService.GetTicketsByAssignedUser(userId);
            return Ok(tickets);
        }

        [HttpGet("overdue")]
        [JwtValidation]
        public async Task<IActionResult> GetOverdueTickets()
        {
            var tickets = await _ticketService.GetOverdueTickets();
            return Ok(tickets);
        }

        [HttpPost]
        [JwtValidation]
        public async Task<IActionResult> AddTicket([FromBody] SupportTicket ticket)
        {
            await _ticketService.AddTicket(ticket);
            return Ok(new { Message = "Support ticket created successfully." });
        }

        [HttpPut]
        [JwtValidation]
        public async Task<IActionResult> UpdateTicket([FromBody] SupportTicket ticket)
        {
            var updated = await _ticketService.UpdateTicket(ticket);
            if (updated)
                return Ok(new { Message = "Ticket updated successfully." });
            return BadRequest(new { Message = "Failed to update ticket." });
        }

        [HttpPut("status/{ticketId}")]
        [JwtValidation]
        public async Task<IActionResult> UpdateTicketStatus(int ticketId, [FromBody] string status)
        {
            var updated = await _ticketService.UpdateTicketStatus(ticketId, status);
            if (updated)
                return Ok(new { Message = "Ticket status updated successfully." });
            return NotFound(new { Message = "Ticket not found." });
        }

        [HttpDelete("{ticketId}")]
        [JwtValidation]
        public async Task<IActionResult> DeleteTicket(int ticketId)
        {
            var deleted = await _ticketService.DeleteTicket(ticketId);
            if (deleted)
                return Ok(new { Message = "Ticket deleted successfully." });
            return NotFound(new { Message = "Ticket not found." });
        }

    }
}
