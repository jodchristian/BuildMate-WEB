namespace Buildmate.Models
{
    public class Quotation
    {
        public int Id { get; set; }
        public string Status { get; set; } = "Pending"; // Accepted, Pending, Declined
        public DateTime SentDate { get; set; }
    }
}