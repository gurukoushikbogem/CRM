namespace MigrationDemo.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Industry { get; set; }
        public string ContactDetails { get; set; }
        public string AccountStatus { get; set; }
        public DateTime? LastContactDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
