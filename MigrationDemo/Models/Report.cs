namespace MigrationDemo.Models
{
    public class Report
    {
        public int ReportId { get; set; } // Primary Key
        public string Type { get; set; } // e.g., "Sales Performance", "Customer Activity"
        public DateTime GeneratedDate { get; set; } // When the report was generated
        public string Data { get; set; } // Serialized report data, could be JSON or XML
        public string CreatedBy { get; set; } // User or system that created the report
        public string Description { get; set; } // Summary or purpose of the report
        public string Format { get; set; } // e.g., "PDF", "Excel", "JSON"
        public int? RelatedEntityId { get; set; } // Optional: ID of related entity (e.g., CampaignId, LeadId)
    }
}
