using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eStavba.Data;
using eStavba.Models;
using eStavba.Filters;

namespace eStavba.Controllers_Api
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuth]
    public class ThreadApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ThreadApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ThreadApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ForumThreadModel>>> GetForumThreads()
        {
          if (_context.ForumThreads == null)
          {
              return NotFound();
          }
            return await _context.ForumThreads.ToListAsync();
        }

        // GET: api/ThreadApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ForumThreadModel>> GetForumThreadModel(int id)
        {
          if (_context.ForumThreads == null)
          {
              return NotFound();
          }
            var forumThreadModel = await _context.ForumThreads.FindAsync(id);

            if (forumThreadModel == null)
            {
                return NotFound();
            }

            return forumThreadModel;
        }

        // PUT: api/ThreadApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutForumThreadModel(int id, ForumThreadModel forumThreadModel)
        {
            if (id != forumThreadModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(forumThreadModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ForumThreadModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ThreadApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ForumThreadModel>> PostForumThreadModel(ForumThreadModel forumThreadModel)
        {
          if (_context.ForumThreads == null)
          {
              return Problem("Entity set 'ApplicationDbContext.ForumThreads'  is null.");
          }
            _context.ForumThreads.Add(forumThreadModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetForumThreadModel", new { id = forumThreadModel.Id }, forumThreadModel);
        }

        // DELETE: api/ThreadApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteForumThreadModel(int id)
        {
            if (_context.ForumThreads == null)
            {
                return NotFound();
            }
            var forumThreadModel = await _context.ForumThreads.FindAsync(id);
            if (forumThreadModel == null)
            {
                return NotFound();
            }

            _context.ForumThreads.Remove(forumThreadModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ForumThreadModelExists(int id)
        {
            return (_context.ForumThreads?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
