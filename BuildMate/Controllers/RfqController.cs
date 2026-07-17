using Microsoft.AspNetCore.Mvc;
using Buildmate.Data;
using Buildmate.Models;

namespace Buildmate.Controllers
{
    public class RfqController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RfqController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string tab = "All", string search = "", string status = "", string category = "")
        {
            var query = _context.RfqRequests.AsQueryable();

            // Tab filter (All / New / Responded / Closed)
            if (tab != "All")
                query = query.Where(r => r.Status == tab);

            // Search box (title or buyer)
            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(r => r.Title.Contains(search) || r.Buyer.Contains(search));

            // Status dropdown filter
            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(r => r.Status == status);

            // Category dropdown filter
            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(r => r.Category.Contains(category));

            var vm = new RfqListViewModel
            {
                AllRfqs = query.OrderByDescending(r => r.PostedDate).ToList(),
                NewCount = _context.RfqRequests.Count(r => r.Status == "New"),
                RespondedCount = _context.RfqRequests.Count(r => r.Status == "Responded"),
                ClosedCount = _context.RfqRequests.Count(r => r.Status == "Closed"),
                TotalCount = _context.RfqRequests.Count(),
                CurrentTab = tab,
                SearchTerm = search,
                StatusFilter = status,
                CategoryFilter = category
            };

            return View(vm);
        }

        public IActionResult Details(int id)
        {
            var rfq = _context.RfqRequests.FirstOrDefault(r => r.Id == id);
            if (rfq == null) return NotFound();
            return View(rfq);
        }
    }
}