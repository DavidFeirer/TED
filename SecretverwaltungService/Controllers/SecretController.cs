using Microsoft.AspNetCore.Mvc;
using SecretverwaltungService.Services;
namespace Secretverwaltungsservice.Controller;

[ApiController]
[Route("api/secrets")]
public class SecretsController : ControllerBase
{
    private readonly SecretsService _secretsService;

    public SecretsController(SecretsService secretsService)
    {
        _secretsService = secretsService;
    }

    [HttpGet("Secret-Key")]
    public IActionResult GetDatabaseConnectionString()
    {
        string secretKey = _secretsService.GetSecretKey();
        return Ok(secretKey);
    }
}
