namespace Buildmate.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public int ThreadId { get; set; }
        public string Content { get; set; } = string.Empty;
        public bool IsFromMe { get; set; }
        public DateTime Timestamp { get; set; }

        public string? AttachmentFileName { get; set; }
        public string? AttachmentPath { get; set; }
        public bool IsImage { get; set; }
    }
}