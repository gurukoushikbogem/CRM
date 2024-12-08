using MigrationDemo.Data;
using MigrationDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace MigrationDemo.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public ReportRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Report>> GetAllReports()
        {
            return await _dbContext.Reports.ToListAsync();
        }

        public async Task<Report> GetReportById(int reportId)
        {
            return await _dbContext.Reports.FindAsync(reportId);
        }

        public async Task<List<Report>> GetReportsByType(string type)
        {
            return await _dbContext.Reports.Where(r => r.Type == type).ToListAsync();
        }

        public async Task AddReport(Report report)
        {
            _dbContext.Reports.Add(report);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteReport(int reportId)
        {
            var report = await _dbContext.Reports.FindAsync(reportId);
            if (report == null) return false;

            _dbContext.Reports.Remove(report);
            return await _dbContext.SaveChangesAsync() > 0;
        }

    }
}
