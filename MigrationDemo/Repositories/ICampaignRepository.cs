using MigrationDemo.Models;

namespace MigrationDemo.Repositories
{
    public interface ICampaignRepository
    {
        Task<List<Campaign>> GetAllCampaigns();
        Task<List<Campaign>> GetCampaignsByStatus(string status);
        Task AddCampaign(Campaign campaign);
        Task<bool> UpdateCampaign(Campaign campaign);
        Task<Campaign> GetCampaignById(int campaignId);
        Task<bool> DeleteCampaign(int campaignId);
        Task<List<Campaign>> GetCampaignsByTargetSegment(string targetSegment);
    }
}
