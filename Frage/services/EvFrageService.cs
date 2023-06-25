using Consul;
using FrageService.Model;
using System.Net;

namespace FrageService.Services
{
    public class EvFrageService : IEvFrageService
    {
        private readonly HttpClient _httpClient;
        private readonly IConsulClient _consulClient;

        public EvFrageService(HttpClient httpClient, IConsulClient consulClient)
        {
            _httpClient = new HttpClient();
            _consulClient = consulClient;
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
                    string fragenevaluierungUrl = $"http://{instance.ServiceAddress}:{instance.ServicePort}/api/evaluierung";
                    Console.WriteLine(fragenevaluierungUrl);
                    var response = await _httpClient.PostAsJsonAsync(fragenevaluierungUrl, text);
                    response.EnsureSuccessStatusCode();
                    Console.WriteLine(response);

                    return bool.Parse(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
