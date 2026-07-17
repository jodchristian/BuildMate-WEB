namespace Buildmate.Models
{
    public class RfqRequest
    {
        public int Id { get; set; }
        public string Reference { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Buyer { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public DateTime PostedDate { get; set; }
        public DateTime ValidTill { get; set; }
        public int ItemsCount { get; set; }
        public string Status { get; set; } = "New"; // New, Responded, Closed

        public int DaysLeft => (ValidTill.Date - DateTime.Now.Date).Days;
    }
}