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
    public class AnnouncementModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnnouncementModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: AnnouncementModels
        public async Task<IActionResult> Index()
        {

            var announcements = GetAnnouncements();
            return View(announcements);
            /*return _context.AnnouncementModel != null ? 
                          View(await _context.AnnouncementModel.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.AnnouncementModel'  is null.");*/
        }

        [Authorize]
        private List<AnnouncementModel> GetAnnouncements()
        {
            // Implement your logic to fetch announcements from a database or any other source
            // For simplicity, let's return a hardcoded list in this example
            return new List<AnnouncementModel>
        {
            new AnnouncementModel { Id = 1, Title = "Annual Community Picnic", Content = "Join us for our annual community picnic on 29.11.2023 at FRI. Fun activities, food, and entertainment for everyone!", DatePosted = DateTime.Now.AddDays(-2) },
            new AnnouncementModel { Id = 2, Title = "Elevator Maintenance Scheduled", Content = "Please be advised that elevator maintenance is scheduled for 30.11.2023. Expect brief disruptions during the maintenance period. We apologize for any inconvenience.", DatePosted = DateTime.Now.AddDays(-1) },
            new AnnouncementModel { Id = 3, Title = "Security Update: Access Cards", Content = "For enhanced security, residents are reminded to use their access cards at all entry points. Report lost cards immediately to the management office.", DatePosted = DateTime.Now.AddDays(-2) },

            // Add more announcements as needed
        };
        }


        [Authorize]
        // GET: AnnouncementModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AnnouncementModel == null)
            {
                return NotFound();
            }

            var announcementModel = await _context.AnnouncementModel
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
        public async Task<IActionResult> Create([Bind("Id,Title,Content,DatePosted")] AnnouncementModel announcementModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(announcementModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(announcementModel);
        }

        // GET: AnnouncementModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AnnouncementModel == null)
            {
                return NotFound();
            }

            var announcementModel = await _context.AnnouncementModel.FindAsync(id);
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
            if (id == null || _context.AnnouncementModel == null)
            {
                return NotFound();
            }

            var announcementModel = await _context.AnnouncementModel
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
            if (_context.AnnouncementModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AnnouncementModel'  is null.");
            }
            var announcementModel = await _context.AnnouncementModel.FindAsync(id);
            if (announcementModel != null)
            {
                _context.AnnouncementModel.Remove(announcementModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnnouncementModelExists(int id)
        {
          return (_context.AnnouncementModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
