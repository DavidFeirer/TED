using Consul;
using FrageService.Controllers;
using FrageService.Model;
using System.Net;

namespace FrageService.Services
{
    public class EvFrageService : IEvFrageService
    {
        private readonly HttpClient _httpClient;
        private readonly IConsulClient _consulClient;
        private readonly ILogger<EvFrageService> _logger;

        public EvFrageService(HttpClient httpClient, IConsulClient consulClient, ILogger<EvFrageService> logger)
        {
            _httpClient = new HttpClient();
            _consulClient = consulClient;
            _logger = logger;
        }



        private readonly string serviceName = "fragenevaluierung";
        public async Task<bool> isValid(String text)
        {
            try
            {
                var services = await _consulClient.Catalog.Service(serviceName);
                var instances = services.Response;
                var instance = instances.FirstOrDefault();

                if (instance != null)
                {
                    string fragenevaluierungUrl = $"http://{instance.Address}:{instance.ServicePort}/api/evaluierung";
                    _logger.LogInformation("Fragenevaluierung url: {}", fragenevaluierungUrl);
                    var response = await _httpClient.PostAsJsonAsync(fragenevaluierungUrl, text);
                    response.EnsureSuccessStatusCode();

                    var isValid = bool.Parse(await response.Content.ReadAsStringAsync());
                    _logger.LogInformation("Validation: {}", isValid);
                 
                    return isValid;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception: {}", ex);
                return false;
            }
        }
    }
}
