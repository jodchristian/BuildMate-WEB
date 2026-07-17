using Microsoft.AspNetCore.Mvc;
using Buildmate.Data;
using Buildmate.Models;

namespace Buildmate.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var settings = _context.UserSettings.FirstOrDefault();
            ViewBag.BankAccounts = _context.BankAccounts.OrderByDescending(b => b.IsPrimary).ToList();
            return View(settings);
        }

        [HttpPost]
        public IActionResult UpdateAccount(UserSettings model)
        {
            var existing = _context.UserSettings.FirstOrDefault(s => s.Id == model.Id);
            if (existing == null) return NotFound();

            existing.FirstName = model.FirstName;
            existing.LastName = model.LastName;
            existing.Email = model.Email;
            existing.PhoneNumber = model.PhoneNumber;
            existing.LanguagePreference = model.LanguagePreference;
            existing.TimeZone = model.TimeZone;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdatePassword(int id, string currentPassword, string newPassword, string confirmPassword)
        {
            // Password change logic would go here (hashing, validation, etc.)
            // For now, just redirect back with a success indicator
            TempData["PasswordUpdated"] = "true";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddBankAccount(BankAccount account)
        {
            account.Status = "Pending";
            _context.BankAccounts.Add(account);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}