using MigrationDemo.Models;
using MigrationDemo.Repositories;
using System.Text.Json;

namespace MigrationDemo.Services
{
    public class ReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<List<Report>> GetAllReports()
        {
            return await _reportRepository.GetAllReports();
        }

        public async Task<Report> GetReportById(int reportId)
        {
            return await _reportRepository.GetReportById(reportId)
                ?? throw new KeyNotFoundException("Report not found.");
        }

        public async Task<List<Report>> GetReportsByType(string type)
        {
            return await _reportRepository.GetReportsByType(type);
        }

        public async Task AddReport(Report report)
        {
            report.GeneratedDate = DateTime.UtcNow;
            await _reportRepository.AddReport(report);
        }

        public async Task<bool> DeleteReport(int reportId)
        {
            return await _reportRepository.DeleteReport(reportId);
        }

        public Report GenerateSalesPerformanceReport(List<SalesPipeline> pipelines)
        {
            var totalRevenue = pipelines.Sum(p => p.EstimatedValue);
            var reportData = new
            {
                TotalRevenue = totalRevenue,
                TotalDeals = pipelines.Count,
                ClosedDeals = pipelines.Count(p => p.Stage == "Closed"),
            };

            return new Report
            {
                Type = "Sales Performance",
                Data = JsonSerializer.Serialize(reportData),
                CreatedBy = "System",
                Description = "Sales performance summary",
                Format = "JSON"
            };
        }

    }
}
