using MigrationDemo.Data;
using MigrationDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace MigrationDemo.Repositories
{
    public class CampaignRepository : ICampaignRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public CampaignRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Campaign>> GetAllCampaigns()
        {
            return await _dbContext.Campaigns.ToListAsync();
        }

        public async Task<List<Campaign>> GetCampaignsByStatus(string status)
        {
            return await _dbContext.Campaigns.Where(c => c.Status == status).ToListAsync();
        }

        public async Task AddCampaign(Campaign campaign)
        {
            _dbContext.Campaigns.Add(campaign);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateCampaign(Campaign campaign)
        {
            _dbContext.Campaigns.Update(campaign);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<Campaign> GetCampaignById(int campaignId)
        {
            return await _dbContext.Campaigns.FindAsync(campaignId);
        }

        public async Task<bool> DeleteCampaign(int campaignId)
        {
            var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
            if (campaign == null) return false;

            _dbContext.Campaigns.Remove(campaign);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Campaign>> GetCampaignsByTargetSegment(string targetSegment)
        {
            return await _dbContext.Campaigns.Where(c => c.TargetSegment.Contains(targetSegment)).ToListAsync();
        }

    }
}
