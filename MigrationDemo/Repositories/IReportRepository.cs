using MigrationDemo.Models;

namespace MigrationDemo.Repositories
{
    public interface IReportRepository
    {
        Task<List<Report>> GetAllReports();
        Task<Report> GetReportById(int reportId);
        Task<List<Report>> GetReportsByType(string type);
        Task AddReport(Report report);
        Task<bool> DeleteReport(int reportId);

    }
}
