namespace Buildmate.Models
{
    public class MessageListViewModel
    {
        public List<MessageThread> Threads { get; set; } = new();
        public int AllCount { get; set; }
        public int UnreadCount { get; set; }
        public int ProjectsCount { get; set; }
        public int SuppliersCount { get; set; }
        public string CurrentFilter { get; set; } = "All";
        public string SearchTerm { get; set; } = "";
    }
}