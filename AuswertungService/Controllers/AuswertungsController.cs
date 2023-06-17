using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuswertungService.Model;

namespace AuswertungService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuswertungsController : ControllerBase
    {
        private readonly AuswertungContext _context;

        public AuswertungsController(AuswertungContext context)
        {
            _context = context;
        }

        // GET: api/Auswertungs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Auswertung>>> GetAuswertungen()
        {
          if (_context.Auswertungen == null)
          {
              return NotFound();
          }
            return await _context.Auswertungen.ToListAsync();
        }

        // GET: api/Auswertungs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Auswertung>> GetAuswertung(int id)
        {
          if (_context.Auswertungen == null)
          {
              return NotFound();
          }
            var auswertung = await _context.Auswertungen.FindAsync(id);

            if (auswertung == null)
            {
                return NotFound();
            }

            return auswertung;
        }

        // PUT: api/Auswertungs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuswertung(int id, Auswertung auswertung)
        {
            if (id != auswertung.Id)
            {
                return BadRequest();
            }

            _context.Entry(auswertung).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuswertungExists(id))
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

        // POST: api/Auswertungs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Auswertung>> PostAuswertung(Auswertung auswertung)
        {
          if (_context.Auswertungen == null)
          {
              return Problem("Entity set 'AuswertungContext.Auswertungen'  is null.");
          }
            _context.Auswertungen.Add(auswertung);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuswertung", new { id = auswertung.Id }, auswertung);
        }

        // DELETE: api/Auswertungs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuswertung(int id)
        {
            if (_context.Auswertungen == null)
            {
                return NotFound();
            }
            var auswertung = await _context.Auswertungen.FindAsync(id);
            if (auswertung == null)
            {
                return NotFound();
            }

            _context.Auswertungen.Remove(auswertung);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuswertungExists(int id)
        {
            return (_context.Auswertungen?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
