﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.OData.Query;
using FragebogenService.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace FragebogenService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FragebogenController : ControllerBase
    {
        private readonly FragebogenContext _context;

        public FragebogenController(FragebogenContext context)
        {
            _context = context;
            InitializeInitialValues();
        }

        // GET: api/Fragebogens
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Fragebogen>>> GetFrageboegen()
        {
            if (_context.Frageboegen == null)
            {
                return NotFound();
            }
            return await _context.Frageboegen.ToListAsync();
        }

        // GET: api/Fragebogens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fragebogen>> GetFragebogen(long id)
        {
            if (_context.Frageboegen == null)
            {
                return NotFound();
            }
            var fragebogen = await _context.Frageboegen.FindAsync(id);

            if (fragebogen == null)
            {
                return NotFound();
            }

            return fragebogen;
        }

        // PUT: api/Fragebogens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFragebogen(long id, Fragebogen fragebogen)
        {
            if (id != fragebogen.Id)
            {
                return BadRequest();
            }

            _context.Entry(fragebogen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FragebogenExists(id))
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

        // POST: api/Fragebogens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Fragebogen>> PostFragebogen(Fragebogen fragebogen)
        {
            if (_context.Frageboegen == null)
            {
                return Problem("Entity set 'FragebogenContext.Frageboegen'  is null.");
            }
            _context.Frageboegen.Add(fragebogen);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFragebogen", new { id = fragebogen.Id }, fragebogen);
        }

        // DELETE: api/Fragebogens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFragebogen(long id)
        {
            if (_context.Frageboegen == null)
            {
                return NotFound();
            }
            var fragebogen = await _context.Frageboegen.FindAsync(id);
            if (fragebogen == null)
            {
                return NotFound();
            }

            _context.Frageboegen.Remove(fragebogen);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FragebogenExists(long id)
        {
            return (_context.Frageboegen?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private void InitializeInitialValues()
        {
            var initialFragebogen = new[]
            {
            new Fragebogen { Id = 1, Name = "Fragebogen_1", Typ = "Nachhaltigkeit" },
            new Fragebogen { Id = 2, Name = "Fragebogen_2", Typ = "Arbeitszufriedenheit" },
            new Fragebogen { Id = 3, Name = "Fragebogen_3", Typ = "Kundenzufriedenheit" }
            };

            foreach (var fragebogen in initialFragebogen)
            {
                if (_context.Frageboegen.Find(fragebogen.Id) == null)
                {
                    _context.Frageboegen.Add(fragebogen);
                }
            }

            _context.SaveChanges();
        }
    }
}
