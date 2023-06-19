using AntwortService.Model;
using Newtonsoft.Json;
using System.Text;

namespace AntwortService.Services
{
    public class UpdateQueue
    {
        private readonly HttpClient _httpClient;
        private readonly string _queueServiceUrl; // Die URL des Queue-Service

        public UpdateQueue(string queueServiceUrl)
        {
            _httpClient = new HttpClient();
            this._queueServiceUrl = queueServiceUrl;
        }

        public async void PostToQueue(Antwort antwort)
        {
            var content = new StringContent(JsonConvert.SerializeObject(antwort.Text), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(_queueServiceUrl, content);
        }
    }
}
