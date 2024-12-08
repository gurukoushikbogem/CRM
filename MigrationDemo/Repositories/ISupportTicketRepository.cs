using MigrationDemo.Models;

namespace MigrationDemo.Repositories
{
    public interface ISupportTicketRepository
    {
        Task<List<SupportTicket>> GetTicketsByStatus(string status);
        Task<List<SupportTicket>> GetTicketsByCustomer(int customerId);
        Task<List<SupportTicket>> GetTicketsByAssignedUser(int userId);
        Task<List<SupportTicket>> GetOverdueTickets();
        Task AddTicket(SupportTicket ticket);
        Task<bool> UpdateTicket(SupportTicket ticket);
        Task<bool> UpdateTicketStatus(int ticketId, string status);
        Task<SupportTicket> GetTicketById(int ticketId);
        Task<bool> DeleteTicket(int ticketId);
    }
}
