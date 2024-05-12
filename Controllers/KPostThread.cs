using eStavba.Data;
using eStavba.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace eStavba.Controllers
{
    public class KPostThread : Controller
    {
        private readonly ApplicationDbContext _context;

        public KPostThread(ApplicationDbContext context)
        
        {
            _context = context;
        }

        [Authorize]

        public IActionResult Index()
        {
            var threads = _context.ForumThreads
                .Include(t => t.Replies)
                .ToList();

            var viewModelList = threads.Select(thread => new ForumThreadViewModel
            {
                Thread = thread,
                Replies = thread.Replies.ToList()
            }).ToList();

            return View(viewModelList);
        }

        public IActionResult Thread(int id)
        {
            var thread = _context.ForumThreads
                .Include(t => t.Replies)
                .FirstOrDefault(t => t.Id == id);

            if (thread == null)
            {
                return NotFound();
            }

            var viewModel = new ForumThreadViewModel
            {
                Thread = thread,
                Replies = thread.Replies.ToList()
            };

            return View(viewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [HttpPost]
        public IActionResult Create(ForumThreadModel thread)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            thread.CreatedAt = DateTime.Now;

            thread.UserId = userId;

            _context.ForumThreads.Add(thread);
            _context.SaveChanges();

            return RedirectToAction("Index");
            
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var thread = _context.ForumThreads.Find(id);

            if (thread == null)
            {
                return NotFound();
            }

            return View(thread);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult DeleteConfirmed(int id)
        {
            var thread = _context.ForumThreads
                .Include(t => t.Replies)  
                .FirstOrDefault(t => t.Id == id);

            if (thread == null)
            {
                return NotFound();
            }

            _context.ForumReplies.RemoveRange(thread.Replies);
            _context.ForumThreads.Remove(thread);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var isAdminUser = string.Equals(User?.Identity?.Name, "estavba@gmail.com", StringComparison.OrdinalIgnoreCase);
            var thread = _context.ForumThreads
                        .Include(t => t.Replies)
                        .FirstOrDefault(t => t.Id == id);

            if (thread == null)
            {
                return NotFound();
            }

            if (thread.Replies != null && thread.Replies.Any())
            {
                TempData["ErrorMessage"] = "Cannot edit a thread with replies.";
                return RedirectToAction("Index");
            }

            if (User.FindFirstValue(ClaimTypes.NameIdentifier) != thread.UserId)
            {
                return RedirectToAction("Index");
            } 

            return View(thread);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(ForumThreadModel thread)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            thread.UserId = userId;

            _context.Update(thread);
            _context.SaveChanges();

            return RedirectToAction("Index");
            
        }

        [Authorize]

        [HttpPost]
        public IActionResult Reply(int threadId, string replyContent)
        {
            var thread = _context.ForumThreads
                .Include(t => t.Replies)
                .FirstOrDefault(t => t.Id == threadId);

            if (thread == null)
            {
                return NotFound();
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var reply = new ForumReplyModel
            {
                Content = replyContent,
                CreatedAt = DateTime.Now,
                ThreadId = threadId,
                UserId = userId,
            };

            thread.Replies.Add(reply);

            _context.SaveChanges();

            return RedirectToAction("Index", new { id = threadId });
        }


    }
}
