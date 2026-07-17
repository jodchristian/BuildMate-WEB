using Microsoft.AspNetCore.Mvc;
using Buildmate.Data;
using Buildmate.Models;

namespace Buildmate.Controllers
{
    public class MessageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MessageController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string filter = "All", string search = "")
        {
            var query = _context.MessageThreads.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(m => m.SenderName.Contains(search) || m.LastMessage.Contains(search));

            query = filter switch
            {
                "Unread" => query.Where(m => m.IsUnread),
                "Projects" => query.Where(m => m.Category == "Projects"),
                "Suppliers" => query.Where(m => m.Category == "Suppliers"),
                _ => query
            };

            var vm = new MessageListViewModel
            {
                Threads = query.OrderByDescending(m => m.LastMessageTime).ToList(),
                AllCount = _context.MessageThreads.Count(),
                UnreadCount = _context.MessageThreads.Count(m => m.IsUnread),
                ProjectsCount = _context.MessageThreads.Count(m => m.Category == "Projects"),
                SuppliersCount = _context.MessageThreads.Count(m => m.Category == "Suppliers"),
                CurrentFilter = filter,
                SearchTerm = search
            };

            return View(vm);
        }

        public IActionResult Chat(int id)
        {
            var thread = _context.MessageThreads.FirstOrDefault(t => t.Id == id);
            if (thread == null) return NotFound();

            thread.IsUnread = false;
            _context.SaveChanges();

            ViewBag.Messages = _context.ChatMessages.Where(m => m.ThreadId == id).OrderBy(m => m.Timestamp).ToList();
            return View(thread);
        }

        [HttpPost]
        public IActionResult SendMessage(int threadId, string content, IFormFile? attachment)
        {
            string? savedFileName = null;
            string? savedPath = null;
            bool isImage = false;

            if (attachment != null && attachment.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "chat");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                savedFileName = $"{Guid.NewGuid()}_{attachment.FileName}";
                var filePath = Path.Combine(uploadsFolder, savedFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    attachment.CopyTo(stream);
                }

                savedPath = $"/uploads/chat/{savedFileName}";

                var ext = Path.GetExtension(attachment.FileName).ToLower();
                isImage = ext is ".jpg" or ".jpeg" or ".png" or ".gif" or ".webp";
            }

            if (!string.IsNullOrWhiteSpace(content) || savedPath != null)
            {
                _context.ChatMessages.Add(new ChatMessage
                {
                    ThreadId = threadId,
                    Content = content ?? "",
                    IsFromMe = true,
                    Timestamp = DateTime.Now,
                    AttachmentFileName = attachment?.FileName,
                    AttachmentPath = savedPath,
                    IsImage = isImage
                });

                var thread = _context.MessageThreads.FirstOrDefault(t => t.Id == threadId);
                if (thread != null)
                {
                    thread.LastMessage = savedPath != null ? $"📎 {attachment!.FileName}" : content!;
                    thread.LastMessageTime = DateTime.Now;
                }

                _context.SaveChanges();
            }

            return RedirectToAction("Chat", new { id = threadId });
        }
    }
}