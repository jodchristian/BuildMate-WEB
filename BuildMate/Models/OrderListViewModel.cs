namespace Buildmate.Models
{
    public class OrderListViewModel
    {
        public List<Order> Orders { get; set; } = new();

        public int TotalOrders { get; set; }
        public int ToConfirmCount { get; set; }
        public int ToShipCount { get; set; }
        public int InTransitCount { get; set; }
        public int DeliveredCount { get; set; }
        public int CancelledCount { get; set; }

        public string CurrentTab { get; set; } = "All";
        public string SearchTerm { get; set; } = "";
        public string StatusFilter { get; set; } = "";
        public string ProjectFilter { get; set; } = "";

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public int TotalFilteredCount { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalFilteredCount / (double)PageSize);
    }
}