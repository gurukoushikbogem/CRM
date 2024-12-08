namespace MigrationDemo.Models
{
    public class Oppurtunity
    {
        public int OpportunityId { get; set; }
        public int? CustomerId { get; set; }
        public int? AccountManagerId { get; set; }
        public decimal OpportunityValue { get; set; }
        public DateTime? CloseDate { get; set; }
        public string Stage { get; set; } = "Open"; 
        public string AccountHealth { get; set; } = "Healthy"; 
        public DateTime? RenewalDate { get; set; } 
        public string Notes { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
