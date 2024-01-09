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
    public class ReportProblemApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportProblemApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ReportProblemApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportProblemModel>>> GetReportedProblems()
        {
          if (_context.ReportedProblems == null)
          {
              return NotFound();
          }
            return await _context.ReportedProblems.ToListAsync();
        }

        // GET: api/ReportProblemApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportProblemModel>> GetReportProblemModel(int id)
        {
          if (_context.ReportedProblems == null)
          {
              return NotFound();
          }
            var reportProblemModel = await _context.ReportedProblems.FindAsync(id);

            if (reportProblemModel == null)
            {
                return NotFound();
            }

            return reportProblemModel;
        }

        // PUT: api/ReportProblemApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReportProblemModel(int id, ReportProblemModel reportProblemModel)
        {
            if (id != reportProblemModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(reportProblemModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportProblemModelExists(id))
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

        // POST: api/ReportProblemApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReportProblemModel>> PostReportProblemModel(ReportProblemModel reportProblemModel)
        {
          if (_context.ReportedProblems == null)
          {
              return Problem("Entity set 'ApplicationDbContext.ReportedProblems'  is null.");
          }
            _context.ReportedProblems.Add(reportProblemModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReportProblemModel", new { id = reportProblemModel.Id }, reportProblemModel);
        }

        // DELETE: api/ReportProblemApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReportProblemModel(int id)
        {
            if (_context.ReportedProblems == null)
            {
                return NotFound();
            }
            var reportProblemModel = await _context.ReportedProblems.FindAsync(id);
            if (reportProblemModel == null)
            {
                return NotFound();
            }

            _context.ReportedProblems.Remove(reportProblemModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReportProblemModelExists(int id)
        {
            return (_context.ReportedProblems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
