using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FrageService.Model;
using Microsoft.AspNetCore.OData.Query;

namespace FrageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FragenController : ControllerBase
    {
        private readonly FrageContext _context;
        private readonly HttpClient _httpClient;

        public FragenController(FrageContext context)
        {
            _context = context;
            InitializeInitialValues();
            _httpClient = new HttpClient();
        }

        // GET: api/Fragen
        [HttpGet]
        [EnableQuery]
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

        private void InitializeInitialValues()
        {
            var initialFragen = new[]
            {
            new Frage { Id = 1, Text = "Wie könnten wir als Unternehmen unseren ökologischen Fußabdruck reduzieren?", FragebogenId = 1 },
            new Frage { Id = 2, Text = "Welche Maßnahmen könnten ergriffen werden, um nachhaltige Produkte oder Dienstleistungen anzubieten?", FragebogenId = 1 },
            new Frage { Id = 3, Text = "Wie könnten wir unsere Mitarbeiter motivieren, nachhaltige Praktiken am Arbeitsplatz umzusetzen?", FragebogenId = 1 },
            new Frage { Id = 4, Text = "Was schätzen Sie am meisten an Ihrer derzeitigen Arbeitsumgebung?", FragebogenId = 2 },
            new Frage { Id = 5, Text = "Welche Faktoren tragen Ihrer Meinung nach am meisten zur Arbeitszufriedenheit bei?", FragebogenId = 2 },
            new Frage { Id = 6, Text = "Was könnte das Unternehmen tun, um das Arbeitsumfeld und das Wohlbefinden der Mitarbeiter zu verbessern?", FragebogenId = 2 },
            new Frage { Id = 7, Text = "Was könnten wir verbessern, um die Kundenerfahrung bei der Nutzung unserer Produkte zu optimieren?", FragebogenId = 3 },
            new Frage { Id = 8, Text = "Was ist aus Ihrer Sicht das herausragendste Merkmal unserer Markeleistungen?", FragebogenId = 3 },
            new Frage { Id = 9, Text = "Wie könnten wir unsere Kundenkommunikation oder den Support weiter verbessern?", FragebogenId = 3 }
            };

            foreach (var frage in initialFragen)
            {
                if (_context.Fragen.Find(frage.Id) == null)
                {
                    _context.Fragen.Add(frage);
                }
            }

            _context.SaveChanges();
        }
    }
}
