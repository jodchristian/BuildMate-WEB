namespace Buildmate.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Sku { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string SubCategory { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }
        public int Moq { get; set; } = 1;
        public string Tax { get; set; } = "VAT (12%)";
        public bool IsTaxInclusive { get; set; } = true;

        public int CurrentStock { get; set; }
        public int LowStockAlert { get; set; }
        public string LeadTime { get; set; } = string.Empty;
        public string StorageLocation { get; set; } = string.Empty;

        public string KeyFeatures { get; set; } = string.Empty;
        public string Status { get; set; } = "Active";
        public string ImageFile { get; set; } = "gradecement.png";
        public DateTime? EffectiveFrom { get; set; }
        public string PriceList { get; set; } = "Standard Price List";
        public string BulkDiscountsJson { get; set; } = "[]";
        public List<string> KeyFeaturesList =>
            string.IsNullOrWhiteSpace(KeyFeatures)
                ? new List<string>()
                : KeyFeatures.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}