using MigrationDemo.Data;
using MigrationDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace MigrationDemo.Repositories
{
    public class OpportunityRepository : IOpportunityRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public OpportunityRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Oppurtunity>> GetAllOpportunities()
        {
            return await _dbContext.Opportunities.ToListAsync();
        }

        public async Task<List<Oppurtunity>> GetOpportunitiesByAccountManager(int managerId)
        {
            return await _dbContext.Opportunities.Where(o => o.AccountManagerId == managerId).ToListAsync();
        }

        public async Task<List<Oppurtunity>> GetOpportunitiesByCustomer(int customerId)
        {
            return await _dbContext.Opportunities.Where(o => o.CustomerId == customerId).ToListAsync();
        }

        public async Task<List<Oppurtunity>> GetOpportunitiesByStage(string stage)
        {
            return await _dbContext.Opportunities.Where(o => o.Stage == stage).ToListAsync();
        }

        public async Task<List<Oppurtunity>> GetOpportunitiesByAccountHealth(string health)
        {
            return await _dbContext.Opportunities.Where(o => o.AccountHealth == health).ToListAsync();
        }

        public async Task AddOpportunity(Oppurtunity opportunity)
        {
            _dbContext.Opportunities.Add(opportunity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateOpportunity(Oppurtunity opportunity)
        {
            opportunity.UpdatedAt = DateTime.UtcNow; // Update the timestamp
            _dbContext.Opportunities.Update(opportunity);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<Oppurtunity> GetOpportunityById(int opportunityId)
        {
            return await _dbContext.Opportunities.FindAsync(opportunityId);
        }

        public async Task<bool> DeleteOpportunity(int opportunityId)
        {
            var opportunity = await _dbContext.Opportunities.FindAsync(opportunityId);
            if (opportunity == null) return false;

            _dbContext.Opportunities.Remove(opportunity);
            return await _dbContext.SaveChangesAsync() > 0;
        }

    }
}
