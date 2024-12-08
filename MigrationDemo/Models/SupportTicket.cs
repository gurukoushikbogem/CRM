namespace MigrationDemo.Models
{
    public class SupportTicket
    {
        public int TicketId { get; set; }
        public int CustomerId { get; set; }
        public string IssueDescription { get; set; }
        public int? AssignedTo { get; set; }
        public string TicketStatus { get; set; } = "Open";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ResolutionDate { get; set; }
        public DateTime? SLADeadline { get; set; }
        public string Priority { get; set; } = "Normal";
        public string Notes { get; set; }
    }
}
