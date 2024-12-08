namespace MigrationDemo.Models
{
    public class Notification
    {
        public int NotificationId { get; set; } // Primary Key
        public string Type { get; set; } // e.g., "Lead Conversion", "Task Deadline"
        public string Message { get; set; } // Notification message
        public DateTime Timestamp { get; set; } // When the notification was generated
        public int UserId { get; set; } // Foreign Key to the User table
        public bool IsRead { get; set; } // Tracks if the user has read the notification
        public string Priority { get; set; } // e.g., "High", "Medium", "Low"
    }
}
