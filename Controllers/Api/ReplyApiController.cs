using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eStavba.Data;
using eStavba.Models;

namespace eStavba.Controllers_Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReplyApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReplyApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ReplyApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ForumReplyModel>>> GetForumReplies()
        {
          if (_context.ForumReplies == null)
          {
              return NotFound();
          }
            return await _context.ForumReplies.ToListAsync();
        }

        // GET: api/ReplyApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ForumReplyModel>> GetForumReplyModel(int id)
        {
          if (_context.ForumReplies == null)
          {
              return NotFound();
          }
            var forumReplyModel = await _context.ForumReplies.FindAsync(id);

            if (forumReplyModel == null)
            {
                return NotFound();
            }

            return forumReplyModel;
        }

        // PUT: api/ReplyApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutForumReplyModel(int id, ForumReplyModel forumReplyModel)
        {
            if (id != forumReplyModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(forumReplyModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ForumReplyModelExists(id))
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

        // POST: api/ReplyApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ForumReplyModel>> PostForumReplyModel(ForumReplyModel forumReplyModel)
        {
          if (_context.ForumReplies == null)
          {
              return Problem("Entity set 'ApplicationDbContext.ForumReplies'  is null.");
          }
            _context.ForumReplies.Add(forumReplyModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetForumReplyModel", new { id = forumReplyModel.Id }, forumReplyModel);
        }

        // DELETE: api/ReplyApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteForumReplyModel(int id)
        {
            if (_context.ForumReplies == null)
            {
                return NotFound();
            }
            var forumReplyModel = await _context.ForumReplies.FindAsync(id);
            if (forumReplyModel == null)
            {
                return NotFound();
            }

            _context.ForumReplies.Remove(forumReplyModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ForumReplyModelExists(int id)
        {
            return (_context.ForumReplies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
