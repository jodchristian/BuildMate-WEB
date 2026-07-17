namespace Buildmate.Models
{
    public class MessageThread
    {
        public int Id { get; set; }
        public string SenderName { get; set; } = string.Empty;
        public string SenderInitials { get; set; } = string.Empty;
        public string AvatarColor { get; set; } = "#fed7aa";
        public bool IsOnline { get; set; } = false;
        public string RelatedOrder { get; set; } = string.Empty;
        public string ProjectName { get; set; } = string.Empty;
        public string OrderStatus { get; set; } = string.Empty;
        public string LastMessage { get; set; } = string.Empty;
        public DateTime LastMessageTime { get; set; }
        public bool IsUnread { get; set; } = false;
        public string Category { get; set; } = "Projects";
    }
}