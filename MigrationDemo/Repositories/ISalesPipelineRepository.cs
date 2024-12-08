using MigrationDemo.Models;

namespace MigrationDemo.Repositories
{
    public interface ISalesPipelineRepository
    {
        Task<List<SalesPipeline>> GetAllPipelines();
        Task<SalesPipeline> GetPipelineById(int pipelineId);
        Task<List<SalesPipeline>> GetPipelinesByLeadId(int leadId);
        Task<List<SalesPipeline>> GetPipelinesByStatus(string status);
        Task<List<SalesPipeline>> GetPipelinesByStage(string stage);
        Task AddPipeline(SalesPipeline pipeline);
        Task<bool> UpdatePipeline(SalesPipeline pipeline);
        Task<bool> UpdatePipelineStage(int pipelineId, string newStage);
        Task<bool> UpdatePipelineStatus(int pipelineId, string newStatus);
        Task<bool> DeletePipeline(int pipelineId);
    }
}
