namespace Buildmate.Models
{
    public class DashboardViewModel
    {
        public int NewRfqRequests { get; set; }
        public int QuotationsSentThisMonth { get; set; }
        public int OrdersReceivedThisMonth { get; set; }
        public decimal TotalSalesThisMonth { get; set; }

        public List<RfqRequest> RecentRfqRequests { get; set; } = new();
        public int NewCount { get; set; }
        public int RespondedCount { get; set; }
        public int ClosedCount { get; set; }
        public string CurrentRfqTab { get; set; } = "New";
        public int TotalOrders { get; set; }
        public int PendingOrders { get; set; }
        public int ProcessingOrders { get; set; }
        public int ShippedOrders { get; set; }
        public int DeliveredOrders { get; set; }

        public int AcceptedQuotations { get; set; }
        public int PendingQuotations { get; set; }
        public int DeclinedQuotations { get; set; }
        public int TotalQuotations => AcceptedQuotations + PendingQuotations + DeclinedQuotations;

        public List<ActivityLog> RecentActivities { get; set; } = new();
    }
}