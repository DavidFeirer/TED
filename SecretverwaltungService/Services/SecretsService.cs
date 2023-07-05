using Newtonsoft.Json;
using SecretverwaltungService.Model;
namespace SecretverwaltungService.Services;
public class SecretsService
{
    private readonly SecretsConfig _secretsConfig;

    public SecretsService()
    {
        string json = File.ReadAllText("Configuration/secrets.json"); // Update with the correct file path if needed
        _secretsConfig = JsonConvert.DeserializeObject<SecretsConfig>(json);
    }

    public string GetSecretKey()
    {
        return _secretsConfig.SecretKey;
    }
}
