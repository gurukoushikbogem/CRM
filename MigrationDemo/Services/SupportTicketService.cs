using MigrationDemo.Models;
using MigrationDemo.Repositories;

namespace MigrationDemo.Services
{
    public class SupportTicketService
    {
        private readonly ISupportTicketRepository _ticketRepository;

        public SupportTicketService(ISupportTicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<List<SupportTicket>> GetTicketsByStatus(string status)
        {
            return await _ticketRepository.GetTicketsByStatus(status);
        }

        public async Task<List<SupportTicket>> GetTicketsByCustomer(int customerId)
        {
            return await _ticketRepository.GetTicketsByCustomer(customerId);
        }

        public async Task<List<SupportTicket>> GetTicketsByAssignedUser(int userId)
        {
            return await _ticketRepository.GetTicketsByAssignedUser(userId);
        }

        public async Task<List<SupportTicket>> GetOverdueTickets()
        {
            return await _ticketRepository.GetOverdueTickets();
        }

        public async Task AddTicket(SupportTicket ticket)
        {
            if (string.IsNullOrWhiteSpace(ticket.IssueDescription))
                throw new ArgumentException("Issue description cannot be empty.");

            if (ticket.Priority == "Critical")
                ticket.SLADeadline = DateTime.UtcNow.AddHours(4); 

            await _ticketRepository.AddTicket(ticket);
        }

        public async Task<bool> UpdateTicket(SupportTicket ticket)
        {
            return await _ticketRepository.UpdateTicket(ticket);
        }

        public async Task<bool> UpdateTicketStatus(int ticketId, string status)
        {
            return await _ticketRepository.UpdateTicketStatus(ticketId, status);
        }

        public async Task<SupportTicket> GetTicketById(int ticketId)
        {
            return await _ticketRepository.GetTicketById(ticketId)
                ?? throw new KeyNotFoundException("Ticket not found.");
        }

        public async Task<bool> DeleteTicket(int ticketId)
        {
            return await _ticketRepository.DeleteTicket(ticketId);
        }
    }
}
