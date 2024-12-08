namespace MigrationDemo.Models
{
    public class NotificationPreference
    {
        public int PreferenceId { get; set; } // Primary Key
        public int UserId { get; set; } // Foreign Key to the User table
        public string NotificationType { get; set; } // e.g., "Task Deadline", "Lead Conversion"
        public bool IsEnabled { get; set; } // Whether this type of notification is enabled

    }
}
