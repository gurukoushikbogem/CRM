using MigrationDemo.Models;
using MigrationDemo.Repositories;

namespace MigrationDemo.Services
{
    public class CampaignService
    {
        private readonly ICampaignRepository _campaignRepository;

        public CampaignService(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task<List<Campaign>> GetAllCampaigns()
        {
            return await _campaignRepository.GetAllCampaigns();
        }

        public async Task<List<Campaign>> GetCampaignsByStatus(string status)
        {
            return await _campaignRepository.GetCampaignsByStatus(status);
        }

        public async Task<List<Campaign>> GetCampaignsByTargetSegment(string targetSegment)
        {
            return await _campaignRepository.GetCampaignsByTargetSegment(targetSegment);
        }

        public async Task AddCampaign(Campaign campaign)
        {
            if (campaign.EndDate <= campaign.StartDate)
                throw new ArgumentException("End date must be later than start date.");

            campaign.Status = "Planned"; // Default status when created
            await _campaignRepository.AddCampaign(campaign);
        }

        public async Task<bool> UpdateCampaign(Campaign campaign)
        {
            if (campaign.EndDate <= campaign.StartDate)
                throw new ArgumentException("End date must be later than start date.");

            return await _campaignRepository.UpdateCampaign(campaign);
        }

        public async Task<Campaign> GetCampaignById(int campaignId)
        {
            return await _campaignRepository.GetCampaignById(campaignId)
                ?? throw new KeyNotFoundException("Campaign not found.");
        }

        public async Task<bool> DeleteCampaign(int campaignId)
        {
            return await _campaignRepository.DeleteCampaign(campaignId);
        }

    }
}
