namespace Buildmate.Models
{
    public class BulkDiscountTier
    {
        public int MinQuantity { get; set; }
        public string MaxQuantity { get; set; } = string.Empty; // "Above" or a number as text
        public decimal Discount { get; set; }
    }
}