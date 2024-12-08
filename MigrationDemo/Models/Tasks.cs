namespace MigrationDemo.Models
{
    public class Tasks
    {
        public int TaskId { get; set; }
        public int? CustomerId { get; set; }
        public int AssignedTo { get; set; }
        public string TaskDescription { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = "Pending";
        public string Priority { get; set; } = "Medium";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }
    }
}
