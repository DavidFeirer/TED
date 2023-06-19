using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AntwortService.Model;
using AntwortService.Services;

namespace AntwortService.Controllers
{
    [Route("api/AntwortController")]
    [ApiController]
    public class AntwortController : ControllerBase
    {
        private readonly AntwortManagementService _antwortManagementService;

        public AntwortController(AntwortManagementService antwortManagementService)
        {
            _antwortManagementService = antwortManagementService;
        }

        // GET: api/Antworts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Antwort>>> GetAntworten()
        {
            if (_antwortManagementService.HoleAlleAntworten() == null)
            {
                return NotFound();
            }
            return _antwortManagementService.HoleAlleAntworten();
        }

        // GET: api/Antworts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Antwort>> GetAntwort(int id)
        {
            if (_antwortManagementService.HoleAlleAntworten() == null)
            {
                return NotFound();
            }
            var antwort = _antwortManagementService.HoleAntwort(id);

            if (antwort == null)
            {
                return NotFound();
            }

            return antwort;
        }

        // POST: api/Antworts
        [HttpPost]
        public async Task<ActionResult<Antwort>> PostAntwort(Antwort antwort)
        {
            if (_antwortManagementService.HoleAlleAntworten() == null)
            {
                return Problem("Entity set 'AntwortContext.Antworten'  is null.");
            }
            _antwortManagementService.SpeichereAntwort(antwort);

            return CreatedAtAction("GetAntwort", new { id = antwort.Id }, antwort);
        }

        // DELETE: api/Antworts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAntwort(int id)
        {
            if (_antwortManagementService.HoleAlleAntworten() == null)
            {
                return NotFound();
            }
            _antwortManagementService.LoescheAntwort(id);

            return NoContent();
        }

        private bool AntwortExists(int id)
        {
            return (_antwortManagementService.HoleAlleAntworten()?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
