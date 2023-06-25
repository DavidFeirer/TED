using Fragenevaluierung.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fragenevaluierung.Controllers
{
    [Route("api/evaluierung")]
    [ApiController]
    public class FragenevaluierungsController : ControllerBase
    {

        private ICheckFrage _checkFrage;
        private readonly ILogger<FragenevaluierungsController> _logger;

        public FragenevaluierungsController(ICheckFrage checkFrage, ILogger<FragenevaluierungsController> logger)
        {
            _checkFrage = checkFrage;
            _logger = logger;
        }

        [HttpPost]
        public ActionResult<bool> EvaluiereFrage([FromBody] string frage)
        {
            _logger.LogInformation("New Request to validate frage: {}", frage);
            return _checkFrage.isValid(frage);
        }
    }
}
