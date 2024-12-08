using MigrationDemo.Models;
using MigrationDemo.Repositories;

namespace MigrationDemo.Services
{
    public class LeadService
    {
        private readonly ILeadRepository _leadRepository;

        private readonly ISalesPipelineRepository _pipelineRepository;

        public LeadService(ILeadRepository leadRepository, ISalesPipelineRepository pipelineRepository)
        {
            _leadRepository = leadRepository;
            _pipelineRepository = pipelineRepository;
        }

        public async Task<List<Lead>> GetAllLeads()
        {
            return await _leadRepository.GetAllLeads();
        }

        public async Task<Lead> GetLeadById(int leadId)
        {
            return await _leadRepository.GetLeadById(leadId)
                   ?? throw new KeyNotFoundException("Lead not found.");
        }

        public async Task<List<Lead>> GetLeadsByStatus(string status)
        {
            return await _leadRepository.GetLeadsByStatus(status);
        }

        public async Task AddLead(Lead lead)
        {
            if (string.IsNullOrWhiteSpace(lead.Name) || string.IsNullOrWhiteSpace(lead.ContactDetails))
                throw new ArgumentException("Name and ContactDetails are required fields.");

            await _leadRepository.AddLead(lead);
        }

        public async Task<bool> UpdateLead(Lead lead)
        {
            return await _leadRepository.UpdateLead(lead);
        }

        public async Task<bool> UpdateLeadStatus(int leadId, string status)
        {
            return await _leadRepository.UpdateLeadStatus(leadId, status);
        }

        public async Task<bool> DeleteLead(int leadId)
        {
            return await _leadRepository.DeleteLead(leadId);
        }


        public async Task<bool> ConvertLeadToPipeline(int leadId)
        {

            var lead = await _leadRepository.GetLeadById(leadId);

            if (lead == null || lead.LeadStatus != "Qualified")
                throw new InvalidOperationException("Lead not found or not qualified.");

            var pipeline = new SalesPipeline
            {
                LeadId = leadId,
                Stage = lead.LeadStatus,
                EstimatedValue = lead.PotentialValue ?? 0,
                Status = "Open",
                Notes="",
                CreatedAt = DateTime.UtcNow
            };

            await _pipelineRepository.AddPipeline(pipeline);

            await _leadRepository.UpdateLeadStatus(leadId, "Converted");

            return true;
        }
        public async Task<List<Lead>> GetUnassignedLeads()
        {
            return await _leadRepository.GetUnassignedLeads();
        }

        public async Task<Lead> GetLeadByName(string name)
        {
            return await _leadRepository.GetLeadByName(name);
        }
    }
}
