using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FrageService.Model;

namespace FrageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FragenController : ControllerBase
    {
        private readonly FrageContext _context;

        public FragenController(FrageContext context)
        {
            _context = context;
        }

        // GET: api/Fragen
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Frage>>> GetFragen()
        {
            if (_context.Fragen == null)
            {
                return NotFound();
            }
            return await _context.Fragen.ToListAsync();
        }

        // GET: api/Fragen/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Frage>> GetFrage(long id)
        {
            if (_context.Fragen == null)
            {
                return NotFound();
            }
            var frage = await _context.Fragen.FindAsync(id);

            if (frage == null)
            {
                return NotFound();
            }

            return frage;
        }

        // PUT: api/Fragen/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFrage(long id, Frage frage)
        {
            if (id != frage.Id)
            {
                return BadRequest();
            }

            _context.Entry(frage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FrageExists(id))
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

        // POST: api/Fragen
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Frage>> PostFrage(Frage frage)
        {
            if (_context.Fragen == null)
            {
                return Problem("Entity set 'FrageContext.Fragen'  is null.");
            }
            _context.Fragen.Add(frage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFrage", new { id = frage.Id }, frage);
        }

        // DELETE: api/Fragen/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFrage(long id)
        {
            if (_context.Fragen == null)
            {
                return NotFound();
            }
            var frage = await _context.Fragen.FindAsync(id);
            if (frage == null)
            {
                return NotFound();
            }

            _context.Fragen.Remove(frage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FrageExists(long id)
        {
            return (_context.Fragen?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
