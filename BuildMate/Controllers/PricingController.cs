using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Buildmate.Data;
using Buildmate.Models;

namespace Buildmate.Controllers
{
    public class PricingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PricingController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string search = "", string status = "", string category = "", int page = 1)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(p => p.Name.Contains(search) || p.Sku.Contains(search));

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(p => p.Status == status);

            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(p => p.Category == category);

            int pageSize = 5;
            int totalFiltered = query.Count();

            var pagedProducts = query
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var vm = new ProductListViewModel
            {
                Products = pagedProducts,
                TotalProducts = _context.Products.Count(),
                ActiveCount = _context.Products.Count(p => p.Status == "Active"),
                InactiveCount = _context.Products.Count(p => p.Status == "Inactive"),
                CategoryCount = _context.Products.Select(p => p.Category).Distinct().Count(),
                SearchTerm = search,
                StatusFilter = status,
                CategoryFilter = category,
                CurrentPage = page,
                PageSize = pageSize,
                TotalFilteredCount = totalFiltered
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(int Id, decimal Price, DateTime? EffectiveFrom, string PriceList, string Status,
            int[] tierMin, string[] tierMax, decimal[] tierDiscount)
        {
            var existing = _context.Products.FirstOrDefault(p => p.Id == Id);
            if (existing == null) return NotFound();

            existing.Price = Price;
            existing.EffectiveFrom = EffectiveFrom;
            existing.PriceList = PriceList;
            existing.Status = Status;

            var tiers = new List<BulkDiscountTier>();
            if (tierMin != null)
            {
                for (int i = 0; i < tierMin.Length; i++)
                {
                    tiers.Add(new BulkDiscountTier
                    {
                        MinQuantity = tierMin[i],
                        MaxQuantity = tierMax != null && i < tierMax.Length ? tierMax[i] : "",
                        Discount = tierDiscount != null && i < tierDiscount.Length ? tierDiscount[i] : 0
                    });
                }
            }
            existing.BulkDiscountsJson = JsonSerializer.Serialize(tiers);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}