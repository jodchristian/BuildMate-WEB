namespace Buildmate.Models
{
    public class CompanyDocument
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public DateTime UploadedDate { get; set; }
        public string ColorTag { get; set; } = "#f97316"; // for the colored square indicator
    }
}