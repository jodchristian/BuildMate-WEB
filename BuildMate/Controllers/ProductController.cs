using Microsoft.AspNetCore.Mvc;
using Buildmate.Data;
using Buildmate.Models;

namespace Buildmate.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
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
        public IActionResult Create()
        {
            return View(new Product());
        }

        [HttpPost]
        public IActionResult Create(Product product, string[] featureTags)
        {
            if (featureTags != null && featureTags.Length > 0)
                product.KeyFeatures = string.Join(",", featureTags);

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product, string[] featureTags)
        {
            if (featureTags != null && featureTags.Length > 0)
                product.KeyFeatures = string.Join(",", featureTags);

            _context.Products.Update(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}