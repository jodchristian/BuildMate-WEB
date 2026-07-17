using Microsoft.AspNetCore.Mvc;
using Buildmate.Data;
using Buildmate.Models;

namespace Buildmate.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string rfqTab = "New")
        {
            var vm = new DashboardViewModel
            {
                NewRfqRequests = _context.RfqRequests.Count(r => r.Status == "New"),
                QuotationsSentThisMonth = _context.Quotations.Count(),
                OrdersReceivedThisMonth = _context.Orders.Count(),
                TotalSalesThisMonth = _context.Orders.Sum(o => o.Amount),

                RecentRfqRequests = _context.RfqRequests.Where(r => r.Status == rfqTab).Take(4).ToList(),
                CurrentRfqTab = rfqTab,
                NewCount = _context.RfqRequests.Count(r => r.Status == "New"),
                RespondedCount = _context.RfqRequests.Count(r => r.Status == "Responded"),
                ClosedCount = _context.RfqRequests.Count(r => r.Status == "Closed"),

                TotalOrders = _context.Orders.Count(),
                PendingOrders = _context.Orders.Count(o => o.Status == "Pending"),
                ProcessingOrders = _context.Orders.Count(o => o.Status == "Processing"),
                ShippedOrders = _context.Orders.Count(o => o.Status == "Shipped"),
                DeliveredOrders = _context.Orders.Count(o => o.Status == "Delivered"),

                AcceptedQuotations = _context.Quotations.Count(q => q.Status == "Accepted"),
                PendingQuotations = _context.Quotations.Count(q => q.Status == "Pending"),
                DeclinedQuotations = _context.Quotations.Count(q => q.Status == "Declined"),

                RecentActivities = _context.ActivityLogs.OrderByDescending(a => a.Timestamp).Take(4).ToList()
            };

            return View(vm);
        }
    }
}