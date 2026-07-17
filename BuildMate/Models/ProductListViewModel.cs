namespace Buildmate.Models
{
    public class ProductListViewModel
    {
        public List<Product> Products { get; set; } = new();

        public int TotalProducts { get; set; }
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int CategoryCount { get; set; }

        public string SearchTerm { get; set; } = "";
        public string StatusFilter { get; set; } = "";
        public string CategoryFilter { get; set; } = "";

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public int TotalFilteredCount { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalFilteredCount / (double)PageSize);
    }
}