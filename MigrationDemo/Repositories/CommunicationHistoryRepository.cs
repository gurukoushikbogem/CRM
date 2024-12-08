using MigrationDemo.Data;
using MigrationDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace MigrationDemo.Repositories
{
    public class CommunicationHistoryRepository : ICommunicationHistoryRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public CommunicationHistoryRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CommunicationHistory>> GetAllInteractions()
        {
            return await _dbContext.CommunicationHistories.ToListAsync();
        }

        public async Task<CommunicationHistory> GetInteractionById(int interactionId)
        {
            return await _dbContext.CommunicationHistories.FindAsync(interactionId);
        }

        public async Task<List<CommunicationHistory>> GetInteractionsByCustomerId(int customerId)
        {
            return await _dbContext.CommunicationHistories
                .Where(ch => ch.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<List<CommunicationHistory>> GetPendingFollowUps(DateTime currentDate)
        {
            return await _dbContext.CommunicationHistories
                .Where(ch => ch.FollowUpRequired && ch.FollowUpDate <= currentDate && ch.FollowUpStatus == "Pending")
                .ToListAsync();
        }

        public async Task AddInteraction(CommunicationHistory interaction)
        {
            _dbContext.CommunicationHistories.Add(interaction);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateInteraction(CommunicationHistory interaction)
        {
            _dbContext.CommunicationHistories.Update(interaction);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteInteraction(int interactionId)
        {
            var interaction = await _dbContext.CommunicationHistories.FindAsync(interactionId);
            if (interaction == null) return false;

            _dbContext.CommunicationHistories.Remove(interaction);
            return await _dbContext.SaveChangesAsync() > 0;
        }

    }
}
