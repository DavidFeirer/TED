using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AntwortService.Model;

namespace AntwortService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AntwortController : ControllerBase
    {
        private readonly AntwortContext _context;

        public AntwortController(AntwortContext context)
        {
            _context = context;
        }

        // GET: api/Antworts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Antwort>>> GetAntworten()
        {
          if (_context.Antworten == null)
          {
              return NotFound();
          }
            return await _context.Antworten.ToListAsync();
        }

        // GET: api/Antworts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Antwort>> GetAntwort(int id)
        {
          if (_context.Antworten == null)
          {
              return NotFound();
          }
            var antwort = await _context.Antworten.FindAsync(id);

            if (antwort == null)
            {
                return NotFound();
            }

            return antwort;
        }

        // PUT: api/Antworts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAntwort(int id, Antwort antwort)
        {
            if (id != antwort.Id)
            {
                return BadRequest();
            }

            _context.Entry(antwort).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AntwortExists(id))
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

        // POST: api/Antworts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Antwort>> PostAntwort(Antwort antwort)
        {
          if (_context.Antworten == null)
          {
              return Problem("Entity set 'AntwortContext.Antworten'  is null.");
          }
            _context.Antworten.Add(antwort);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAntwort", new { id = antwort.Id }, antwort);
        }

        // DELETE: api/Antworts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAntwort(int id)
        {
            if (_context.Antworten == null)
            {
                return NotFound();
            }
            var antwort = await _context.Antworten.FindAsync(id);
            if (antwort == null)
            {
                return NotFound();
            }

            _context.Antworten.Remove(antwort);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AntwortExists(int id)
        {
            return (_context.Antworten?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
