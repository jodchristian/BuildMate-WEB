using Microsoft.AspNetCore.Mvc;
using Buildmate.Data;
using Buildmate.Models;

namespace Buildmate.Controllers
{
    public class NotificationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotificationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string filter = "All")
        {
            var query = _context.Notifications.AsQueryable();

            query = filter switch
            {
                "Unread" => query.Where(n => !n.IsRead),
                "Orders & Shipments" => query.Where(n => n.Category == "Orders & Shipments"),
                "RFQ & Quotations" => query.Where(n => n.Category == "RFQ & Quotations"),
                "Payments & Invoices" => query.Where(n => n.Category == "Payments & Invoices"),
                "System Updates" => query.Where(n => n.Category == "System Updates"),
                _ => query
            };

            var items = query.OrderByDescending(n => n.Timestamp).Take(6).ToList();

            var groups = items
                .GroupBy(n => GetDateLabel(n.Timestamp))
                .Select(g => new NotificationGroup { DateLabel = g.Key, Items = g.ToList() })
                .ToList();

            var vm = new NotificationListViewModel
            {
                Groups = groups,
                AllCount = _context.Notifications.Count(),
                UnreadCount = _context.Notifications.Count(n => !n.IsRead),
                OrdersCount = _context.Notifications.Count(n => n.Category == "Orders & Shipments"),
                RfqCount = _context.Notifications.Count(n => n.Category == "RFQ & Quotations"),
                PaymentsCount = _context.Notifications.Count(n => n.Category == "Payments & Invoices"),
                SystemCount = _context.Notifications.Count(n => n.Category == "System Updates"),
                CurrentFilter = filter,
                ShowingCount = items.Count,
                TotalCount = query.Count()
            };

            return View(vm);
        }

        private string GetDateLabel(DateTime date)
        {
            if (date.Date == DateTime.Now.Date) return "Today";
            if (date.Date == DateTime.Now.Date.AddDays(-1)) return "Yesterday";
            return date.ToString("MMMM d, yyyy");
        }

        [HttpPost]
        public IActionResult MarkAllRead()
        {
            var unread = _context.Notifications.Where(n => !n.IsRead).ToList();
            foreach (var n in unread) n.IsRead = true;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult MarkRead(int id)
        {
            var n = _context.Notifications.FirstOrDefault(x => x.Id == id);
            if (n != null)
            {
                n.IsRead = true;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}