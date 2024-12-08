namespace MigrationDemo.Models
{
    public class Campaign
    {
        public int CampaignId { get; set; } 
        public string Name { get; set; } 
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }
        public string TargetSegment { get; set; } 
        public decimal Budget { get; set; } 
        public string Status { get; set; } 
        public int? CreatedBy { get; set; } 
        public decimal? ActualSpend { get; set; }
        public int? TotalLeadsGenerated { get; set; } 
        public string Notes { get; set; } 
    }
}
