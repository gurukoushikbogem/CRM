namespace MigrationDemo.Models
{
    public class SalesPipeline
    {
        public int PipelineId { get; set; }
        public int LeadId { get; set; }
        public string Stage { get; set; } = "Qualification";
        public decimal EstimatedValue { get; set; }
        public DateTime? ClosingDate { get; set; }
        public string Status { get; set; } = "Open";
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
