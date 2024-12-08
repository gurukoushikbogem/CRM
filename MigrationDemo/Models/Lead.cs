namespace MigrationDemo.Models
{
    public class Lead
    {
        public int LeadId { get; set; }
        public string LeadSource { get; set; }
        public string Name { get; set; }
        public string ContactDetails { get; set; }
        public string LeadStatus { get; set; }
        public int? AssignedTo { get; set; }
        public decimal? PotentialValue { get; set; }
        public string SalesStage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
