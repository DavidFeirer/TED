using AntwortService.Model;
using AuswertungService.Model;
using AuswertungService.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuswertungService.Controllers
{
    [Route("api/AuswertungsController")]
    [ApiController]
    public class AuswertungsController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;
        public AuswertungsController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        // GET: api/AuswertungsController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Auswertung>>> GetAuswertungen()
        {
          using(var scope = _serviceProvider.CreateScope())
            {
              var auswertungManagementService = scope.ServiceProvider.GetRequiredService<IAuswertungManagementService>();
              if (auswertungManagementService.HoleAlleAuswertungen() == null)
              {
                  return NotFound();
              }
              return auswertungManagementService.HoleAlleAuswertungen();
          }
        }
        
        [HttpGet("antworten")]
        public async Task<ActionResult<IEnumerable<String>>> GetAntworten()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var auswertungManagementService = scope.ServiceProvider.GetRequiredService<IAuswertungManagementService>();
                if (auswertungManagementService.HoleAlleAntworten() == null)
                {
                    return NotFound();
                }
                return auswertungManagementService.HoleAlleAntworten();
            }
        }

        // GET: api/AuswertungsController/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Auswertung>> GetAuswertung(int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var auswertungManagementService = scope.ServiceProvider.GetRequiredService<IAuswertungManagementService>();
                if (auswertungManagementService.HoleAlleAuswertungen() == null)
                {
                    return NotFound();
                }
                var auswertung = auswertungManagementService.HoleAuswertung(id);

                if (auswertung == null)
                {
                    return NotFound();
                }

                return auswertung;
            }
        }
        
        // POST: api/AuswertungsController
        [HttpPost]
        public async Task<ActionResult<Auswertung>> PostAuswertung(Auswertung auswertung)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var auswertungManagementService = scope.ServiceProvider.GetRequiredService<IAuswertungManagementService>();
                auswertungManagementService.SpeichereAuswertung(auswertung);

                return CreatedAtAction("GetAuswertung", new { id = auswertung.Id }, auswertung);
            }
        }

        // DELETE: api/AuswertungsController/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuswertung(int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var auswertungManagementService = scope.ServiceProvider.GetRequiredService<IAuswertungManagementService>();
                if (auswertungManagementService.HoleAlleAuswertungen() == null)
                {
                    return NotFound();
                }

                auswertungManagementService.LoescheAuswertung(id);

                return NoContent();
            }
            
        }
    }
}
