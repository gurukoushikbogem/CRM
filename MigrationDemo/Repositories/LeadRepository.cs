using MigrationDemo.Data;
using MigrationDemo.Models;
using Microsoft.EntityFrameworkCore;


namespace MigrationDemo.Repositories
{
    public class LeadRepository: ILeadRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public LeadRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Lead>> GetAllLeads()
        {
            return await _dbContext.Leads.ToListAsync();
        }

        public async Task<Lead> GetLeadById(int leadId)
        {
            return await _dbContext.Leads.FindAsync(leadId);
        }

        public async Task<List<Lead>> GetLeadsByStatus(string status)
        {
            return await _dbContext.Leads.Where(l => l.LeadStatus == status).ToListAsync();
        }

        public async Task AddLead(Lead lead)
        {
            _dbContext.Leads.Add(lead);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateLead(Lead lead)
        {
            lead.UpdatedAt = DateTime.UtcNow;
            _dbContext.Leads.Update(lead);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateLeadStatus(int leadId, string status)
        {
            var lead = await _dbContext.Leads.FindAsync(leadId);
            if (lead == null) return false;

            lead.LeadStatus = status;
            lead.UpdatedAt = DateTime.UtcNow;
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteLead(int leadId)
        {
            var lead = await _dbContext.Leads.FindAsync(leadId);
            if (lead == null) return false;

            _dbContext.Leads.Remove(lead);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Lead>> GetUnassignedLeads()
        {
            return await _dbContext.Leads.Where(l => l.AssignedTo == null).ToListAsync();
        }

        public async Task<Lead> GetLeadByName(string name)
        {
            return await _dbContext.Leads.FirstOrDefaultAsync(l => l.Name == name);
        }

    }
}
