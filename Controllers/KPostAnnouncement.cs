using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eStavba.Data;
using eStavba.Models;
using Microsoft.AspNetCore.Authorization;

namespace eStavba.Controllers
{
    [Authorize]
    public class KPostAnnouncement : Controller
    {
        private readonly ApplicationDbContext _context;

        public KPostAnnouncement(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: AnnouncementModels
        public async Task<IActionResult> Index()
        {
            var announcements = await _context.Announcement.ToListAsync();
            return View(announcements);
        }

        /*[Authorize]
        private List<Announcement> GetAnnouncements()
        {

            return new List<Announcement>
        {
            new Announcement { Id = 1, Title = "Annual Community Picnic", Content = "Join us for our annual community picnic on 29.11.2023 at FRI. Fun activities, food, and entertainment for everyone!", DatePosted = DateTime.Now.AddDays(-4) },
            new Announcement { Id = 2, Title = "Elevator Maintenance Scheduled", Content = "Please be advised that elevator maintenance is scheduled for 30.11.2023. Expect brief disruptions during the maintenance period. We apologize for any inconvenience.", DatePosted = DateTime.Now.AddDays(-1) },
            new Announcement { Id = 3, Title = "Security Update: Access Cards", Content = "For enhanced security, residents are reminded to use their access cards at all entry points. Report lost cards immediately to the management office.", DatePosted = DateTime.Now.AddDays(-5) },
            new Announcement { Id = 4, Title = "Home Improvement Webinar", Content = "Join our upcoming webinar on practical home improvement tips. Learn from experts and get ideas to enhance your living space.", DatePosted = DateTime.Now.AddDays(-2) },
            new Announcement { Id = 5, Title = "Mobile App is Here!", Content = "Download the new eStavba mobile app for convenient access to forums, announcements, and community updates on the go. Available on Google Play.", DatePosted = DateTime.Now.AddDays(-1) },
        };
        }*/


        [Authorize]
        // GET: AnnouncementModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Announcement == null)
            {
                return NotFound();
            }

            var announcementModel = await _context.Announcement
                .FirstOrDefaultAsync(m => m.Id == id);
            if (announcementModel == null)
            {
                return NotFound();
            }

            return View(announcementModel);
        }

        // GET: AnnouncementModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AnnouncementModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content,DatePosted")] AnnouncementModel announcementModel)
        {
            if (ModelState.IsValid)
            {
                _context.Announcement.Add(announcementModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(announcementModel);
        }

        // GET: AnnouncementModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Announcement == null)
            {
                return NotFound();
            }

            var announcementModel = await _context.Announcement.FindAsync(id);
            if (announcementModel == null)
            {
                return NotFound();
            }
            return View(announcementModel);
        }

        // POST: AnnouncementModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,DatePosted")] AnnouncementModel announcementModel)
        {
            if (id != announcementModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(announcementModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnnouncementModelExists(announcementModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(announcementModel);
        }

        // GET: AnnouncementModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Announcement == null)
            {
                return NotFound();
            }

            var announcementModel = await _context.Announcement
                .FirstOrDefaultAsync(m => m.Id == id);
            if (announcementModel == null)
            {
                return NotFound();
            }

            return View(announcementModel);
        }

        // POST: AnnouncementModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Announcement == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Announcement'  is null.");
            }
            var announcementModel = await _context.Announcement.FindAsync(id);
            if (announcementModel != null)
            {
                _context.Announcement.Remove(announcementModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnnouncementModelExists(int id)
        {
            return (_context.Announcement?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}