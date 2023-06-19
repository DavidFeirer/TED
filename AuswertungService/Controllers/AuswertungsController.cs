using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuswertungService.Model;
using AuswertungService.Services;

namespace AuswertungService.Controllers
{
    [Route("api/AuswertungsController")]
    [ApiController]
    public class AuswertungsController : ControllerBase
    {
        private readonly AuswertungManagementService _auswertungManagementService;
        public AuswertungsController(AuswertungManagementService auswertungManagementService)
        {
            _auswertungManagementService = auswertungManagementService; 
        }

        // GET: api/AuswertungsController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Auswertung>>> GetAuswertungen()
        {
          if (_auswertungManagementService.HoleAlleAuswertungen() == null)
          {
              return NotFound();
          }
            return _auswertungManagementService.HoleAlleAuswertungen();
        }

        // GET: api/AuswertungsController/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Auswertung>> GetAuswertung(int id)
        {
          if (_auswertungManagementService.HoleAlleAuswertungen() == null)
          {
              return NotFound();
          }
            var auswertung = _auswertungManagementService.HoleAuswertung(id);

            if (auswertung == null)
            {
                return NotFound();
            }

            return auswertung;
        }
        [HttpGet("queue")]
        public async Task<ActionResult<List<String>>> GetAuswertung()
        {
            return _auswertungManagementService.HoleAlleAntworten();            
        }

        // POST: api/AuswertungsController
        [HttpPost]
        public async Task<ActionResult<Auswertung>> PostAuswertung(Auswertung auswertung)
        {
            _auswertungManagementService.SpeichereAuswertung(auswertung);

            return CreatedAtAction("GetAuswertung", new { id = auswertung.Id }, auswertung);
        }
        [HttpPost("queue")]
        public async Task<ActionResult<Auswertung>> PostMessage(Object message)
        {
            _auswertungManagementService.SpeichereAuswertung(message.ToString());
            return CreatedAtAction("GetAuswertung", message.ToString());
        }

        // DELETE: api/AuswertungsController/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuswertung(int id)
        {
            if (_auswertungManagementService.HoleAlleAuswertungen() == null)
            {
                return NotFound();
            }

            _auswertungManagementService.LoescheAuswertung(id);

            return NoContent();
        }
    }
}
