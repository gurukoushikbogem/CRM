using MigrationDemo.Models;
using MigrationDemo.Repositories;

namespace MigrationDemo.Services
{
    public class SalesPipelineService
    {
        private readonly ISalesPipelineRepository _pipelineRepository;

        private readonly IOpportunityRepository _opportunityRepository;

        public SalesPipelineService(IOpportunityRepository opportunityRepository,ISalesPipelineRepository pipelineRepository)
        {
            _pipelineRepository = pipelineRepository;
            _opportunityRepository = opportunityRepository;
        }

        public async Task<List<SalesPipeline>> GetAllPipelines()
        {
            return await _pipelineRepository.GetAllPipelines();
        }

        public async Task<SalesPipeline> GetPipelineById(int pipelineId)
        {
            return await _pipelineRepository.GetPipelineById(pipelineId)
                   ?? throw new KeyNotFoundException("Pipeline not found.");
        }

        public async Task AddPipeline(SalesPipeline pipeline)
        {
            if (pipeline.EstimatedValue <= 0)
                throw new ArgumentException("EstimatedValue must be greater than zero.");
            await _pipelineRepository.AddPipeline(pipeline);
        }

        public async Task<bool> UpdatePipeline(SalesPipeline pipeline)
        {
            return await _pipelineRepository.UpdatePipeline(pipeline);
        }

        public async Task<bool> UpdatePipelineStage(int pipelineId, string newStage)
        {
            return await _pipelineRepository.UpdatePipelineStage(pipelineId, newStage);
        }

        public async Task<bool> UpdatePipelineStatus(int pipelineId, string newStatus)
        {
            return await _pipelineRepository.UpdatePipelineStatus(pipelineId, newStatus);
        }

        public async Task<bool> DeletePipeline(int pipelineId)
        {
            return await _pipelineRepository.DeletePipeline(pipelineId);
        }

        public async Task<Oppurtunity> CreateOpportunityFromPipeline(int pipelineId)
        {
            var pipeline = await _pipelineRepository.GetPipelineById(pipelineId);

            if (pipeline == null)
                throw new KeyNotFoundException("Pipeline not found.");

            if (pipeline.Status != "Open" || pipeline.Stage != "Qualified")
                throw new InvalidOperationException("Pipeline must be open and in a qualified stage.");

            var opportunity = new Oppurtunity
            {
                OpportunityValue = pipeline.EstimatedValue,
                Notes = pipeline.Notes,
                CreatedAt = DateTime.UtcNow,
                CustomerId= pipeline.LeadId
            };

            await _opportunityRepository.AddOpportunity(opportunity);

            await _pipelineRepository.UpdatePipelineStatus(pipelineId, "Converted");

            return opportunity;
        }
    }
}
