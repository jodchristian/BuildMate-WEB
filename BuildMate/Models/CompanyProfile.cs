namespace Buildmate.Models
{
    public class CompanyProfile
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string LogoFile { get; set; } = "compannyprof.png";
        public bool IsVerified { get; set; } = true;
        public DateTime SinceDate { get; set; }
        public string ShortDescription { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;

        public int TotalOrders { get; set; }
        public decimal AverageRating { get; set; }
        public int ReviewsCount { get; set; }
        public int ResponseRate { get; set; }

        public string AboutCompany { get; set; } = string.Empty;
        public string BusinessType { get; set; } = string.Empty;
        public int YearEstablished { get; set; }
        public string CompanySize { get; set; } = string.Empty;
        public string MainCategories { get; set; } = string.Empty; // comma-separated
        public string ServiceAreas { get; set; } = string.Empty;

        public string DtiRegNo { get; set; } = string.Empty;
        public string BusinessPermitNo { get; set; } = string.Empty;
        public string Tin { get; set; } = string.Empty;
        public bool VatRegistered { get; set; }

        public List<string> MainCategoriesList =>
            string.IsNullOrWhiteSpace(MainCategories)
                ? new List<string>()
                : MainCategories.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}