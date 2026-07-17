namespace Buildmate.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public string ProjectName { get; set; } = string.Empty;
        public string BuyerName { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public decimal Amount { get; set; }
        public int ItemsCount { get; set; }
        public string Status { get; set; } = "To Confirm"; // To Confirm, To Ship, In Transit, Delivered, Cancelled
        public DateTime ExpectedDelivery { get; set; }

        public int DaysLeft => (ExpectedDelivery.Date - DateTime.Now.Date).Days;

        public string DeliveryStatusText
        {
            get
            {
                if (Status == "Delivered") return "Delivered";
                if (Status == "Cancelled") return "Cancelled";
                return DaysLeft < 0 ? "Overdue" : $"{DaysLeft} Days Left";
            }
        }
    }
}