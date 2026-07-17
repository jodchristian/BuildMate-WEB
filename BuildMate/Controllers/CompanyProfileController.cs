using Microsoft.AspNetCore.Mvc;
using Buildmate.Data;
using Buildmate.Models;

namespace Buildmate.Controllers
{
    public class CompanyProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompanyProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var profile = _context.CompanyProfiles.FirstOrDefault();
            var documents = _context.CompanyDocuments.OrderByDescending(d => d.UploadedDate).ToList();
            ViewBag.Documents = documents;
            return View(profile);
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var profile = _context.CompanyProfiles.FirstOrDefault();
            return View(profile);
        }

        [HttpPost]
        public IActionResult Edit(CompanyProfile model, string[] categoryTags)
        {
            var existing = _context.CompanyProfiles.FirstOrDefault(c => c.Id == model.Id);
            if (existing == null) return NotFound();

            existing.CompanyName = model.CompanyName;
            existing.ShortDescription = model.ShortDescription;
            existing.Address = model.Address;
            existing.Phone = model.Phone;
            existing.Email = model.Email;
            existing.Website = model.Website;
            existing.AboutCompany = model.AboutCompany;
            existing.BusinessType = model.BusinessType;
            existing.YearEstablished = model.YearEstablished;
            existing.CompanySize = model.CompanySize;
            existing.ServiceAreas = model.ServiceAreas;

            if (categoryTags != null && categoryTags.Length > 0)
                existing.MainCategories = string.Join(",", categoryTags);

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}