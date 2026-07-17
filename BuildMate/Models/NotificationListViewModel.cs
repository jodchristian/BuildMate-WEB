namespace Buildmate.Models
{
    public class NotificationGroup
    {
        public string DateLabel { get; set; } = string.Empty;
        public List<NotificationItem> Items { get; set; } = new();
    }

    public class NotificationListViewModel
    {
        public List<NotificationGroup> Groups { get; set; } = new();

        public int AllCount { get; set; }
        public int UnreadCount { get; set; }
        public int OrdersCount { get; set; }
        public int RfqCount { get; set; }
        public int PaymentsCount { get; set; }
        public int SystemCount { get; set; }

        public string CurrentFilter { get; set; } = "All";

        public int ShowingCount { get; set; }
        public int TotalCount { get; set; }
    }
}