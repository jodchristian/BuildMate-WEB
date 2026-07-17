namespace Buildmate.Models
{
    public class NotificationItem
    {
        public int Id { get; set; }
        public string Category { get; set; } = string.Empty; // Orders & Shipments, RFQ & Quotations, Payments & Invoices, System Updates
        public string IconType { get; set; } = "rfq"; // rfq, order, system, payment
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Tag { get; set; } // e.g. "RFQ-2505-0017"
        public DateTime Timestamp { get; set; }
        public bool IsRead { get; set; } = false;
    }
}