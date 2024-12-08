using MigrationDemo.Data;
using MigrationDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace MigrationDemo.Repositories
{
    public class SupportTicketRepository: ISupportTicketRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public SupportTicketRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<SupportTicket>> GetTicketsByStatus(string status)
        {
            return await _dbContext.SupportTickets.Where(t => t.TicketStatus == status).ToListAsync();
        }

        public async Task<List<SupportTicket>> GetTicketsByCustomer(int customerId)
        {
            return await _dbContext.SupportTickets.Where(t => t.CustomerId == customerId).ToListAsync();
        }

        public async Task<List<SupportTicket>> GetTicketsByAssignedUser(int userId)
        {
            return await _dbContext.SupportTickets.Where(t => t.AssignedTo == userId).ToListAsync();
        }

        public async Task<List<SupportTicket>> GetOverdueTickets()
        {
            return await _dbContext.SupportTickets
                .Where(t => t.SLADeadline.HasValue && t.SLADeadline < DateTime.UtcNow && t.TicketStatus != "Resolved")
                .ToListAsync();
        }

        public async Task AddTicket(SupportTicket ticket)
        {
            _dbContext.SupportTickets.Add(ticket);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateTicket(SupportTicket ticket)
        {
            ticket.UpdatedAt = DateTime.UtcNow;
            _dbContext.SupportTickets.Update(ticket);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateTicketStatus(int ticketId, string status)
        {
            var ticket = await _dbContext.SupportTickets.FindAsync(ticketId);
            if (ticket == null) return false;

            ticket.TicketStatus = status;
            ticket.UpdatedAt = DateTime.UtcNow;

            if (status == "Resolved" || status == "Closed")
                ticket.ResolutionDate = DateTime.UtcNow;

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<SupportTicket> GetTicketById(int ticketId)
        {
            return await _dbContext.SupportTickets.FindAsync(ticketId);
        }

        public async Task<bool> DeleteTicket(int ticketId)
        {
            var ticket = await _dbContext.SupportTickets.FindAsync(ticketId);
            if (ticket == null) return false;

            _dbContext.SupportTickets.Remove(ticket);
            return await _dbContext.SaveChangesAsync() > 0;
        }

    }
}
