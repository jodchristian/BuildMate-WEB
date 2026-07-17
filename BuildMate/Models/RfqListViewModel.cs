namespace Buildmate.Models
{
    public class RfqListViewModel
    {
        public List<RfqRequest> AllRfqs { get; set; } = new();
        public int NewCount { get; set; }
        public int RespondedCount { get; set; }
        public int ClosedCount { get; set; }
        public int TotalCount { get; set; }

        public string CurrentTab { get; set; } = "All";
        public string SearchTerm { get; set; } = "";
        public string StatusFilter { get; set; } = "";
        public string CategoryFilter { get; set; } = "";
    }
}