namespace Buildmate.Models
{
    public class ActivityLog
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime Timestamp { get; set; }
        public string IconType { get; set; } = "info"; // rfq, quotation, order
    }
}