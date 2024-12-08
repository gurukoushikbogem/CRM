using MigrationDemo.Data;
using MigrationDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace MigrationDemo.Repositories
{
    public class SalesPipelineRepository : ISalesPipelineRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public SalesPipelineRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<SalesPipeline>> GetAllPipelines()
        {
            return await _dbContext.SalesPipelines.ToListAsync();
        }

        public async Task<SalesPipeline> GetPipelineById(int pipelineId)
        {
            return await _dbContext.SalesPipelines.FindAsync(pipelineId);
        }

        public async Task<List<SalesPipeline>> GetPipelinesByLeadId(int leadId)
        {
            return await _dbContext.SalesPipelines.Where(p => p.LeadId == leadId).ToListAsync();
        }

        public async Task<List<SalesPipeline>> GetPipelinesByStatus(string status)
        {
            return await _dbContext.SalesPipelines.Where(p => p.Status == status).ToListAsync();
        }

        public async Task<List<SalesPipeline>> GetPipelinesByStage(string stage)
        {
            return await _dbContext.SalesPipelines.Where(p => p.Stage == stage).ToListAsync();
        }

        public async Task AddPipeline(SalesPipeline pipeline)
        {
            _dbContext.SalesPipelines.Add(pipeline);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdatePipeline(SalesPipeline pipeline)
        {
            pipeline.UpdatedAt = DateTime.UtcNow;
            _dbContext.SalesPipelines.Update(pipeline);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdatePipelineStage(int pipelineId, string newStage)
        {
            var pipeline = await _dbContext.SalesPipelines.FindAsync(pipelineId);
            if (pipeline == null) return false;

            pipeline.Stage = newStage;
            pipeline.UpdatedAt = DateTime.UtcNow;
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdatePipelineStatus(int pipelineId, string newStatus)
        {
            var pipeline = await _dbContext.SalesPipelines.FindAsync(pipelineId);
            if (pipeline == null) return false;

            pipeline.Status = newStatus;
            pipeline.UpdatedAt = DateTime.UtcNow;
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletePipeline(int pipelineId)
        {
            var pipeline = await _dbContext.SalesPipelines.FindAsync(pipelineId);
            if (pipeline == null) return false;

            _dbContext.SalesPipelines.Remove(pipeline);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
