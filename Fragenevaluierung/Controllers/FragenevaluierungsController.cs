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

        public FragenevaluierungsController(ICheckFrage checkFrage)
        {
            _checkFrage = checkFrage;
        }

        [HttpPost]
        public ActionResult<bool> EvaluiereFrage([FromBody] string frage)
        {
            return _checkFrage.isValid(frage);
        }
    }
}
