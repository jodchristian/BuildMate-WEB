namespace Buildmate.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string BankName { get; set; } = string.Empty;
        public string AccountName { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public string AccountType { get; set; } = "Savings Account";
        public string Status { get; set; } = "Verified"; // Verified, Pending
        public bool IsPrimary { get; set; } = false;
    }
}