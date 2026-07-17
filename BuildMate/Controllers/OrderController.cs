using Microsoft.AspNetCore.Mvc;
using Buildmate.Data;
using Buildmate.Models;

namespace Buildmate.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string tab = "All", string search = "", string status = "", string project = "", int page = 1)
        {
            var query = _context.Orders.AsQueryable();

            if (tab != "All")
                query = query.Where(o => o.Status == tab);

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(o => o.OrderNumber.Contains(search) || o.BuyerName.Contains(search) || o.ProjectName.Contains(search));

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(o => o.Status == status);

            if (!string.IsNullOrWhiteSpace(project))
                query = query.Where(o => o.ProjectName == project);

            int pageSize = 5;
            int totalFiltered = query.Count();

            var pagedOrders = query
                .OrderByDescending(o => o.OrderDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var vm = new OrderListViewModel
            {
                Orders = pagedOrders,
                TotalOrders = _context.Orders.Count(),
                ToConfirmCount = _context.Orders.Count(o => o.Status == "To Confirm"),
                ToShipCount = _context.Orders.Count(o => o.Status == "To Ship"),
                InTransitCount = _context.Orders.Count(o => o.Status == "In Transit"),
                DeliveredCount = _context.Orders.Count(o => o.Status == "Delivered"),
                CancelledCount = _context.Orders.Count(o => o.Status == "Cancelled"),
                CurrentTab = tab,
                SearchTerm = search,
                StatusFilter = status,
                ProjectFilter = project,
                CurrentPage = page,
                PageSize = pageSize,
                TotalFilteredCount = totalFiltered
            };

            return View(vm);
        }

        public IActionResult Details(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();
            return View(order);
        }
    }
}