namespace MigrationDemo.Models
{
    public class CommunicationHistory
    {
        public int InteractionId { get; set; }
        public int CustomerId { get; set; }
        public string InteractionType { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Notes { get; set; }
        public bool FollowUpRequired { get; set; } = false;
        public DateTime? FollowUpDate { get; set; }
        public string FollowUpStatus { get; set; } = "Pending";
        public string CreatedBy { get; set; }
    }
}
