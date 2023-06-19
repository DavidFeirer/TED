using Newtonsoft.Json;
using QueueService.Model;
using System.Text;

namespace QueueService.Services
{
    public class MessageQueueService <T>
    {
        private readonly Queue<Message<T>> messageQueue;
        private readonly HttpClient httpClient;
        private readonly string auswertungServiceUrl; // Die URL des Auswertungs-Service

        public MessageQueueService(string auswertungServiceUrl)
        {
            messageQueue = new Queue<Message<T>>();
            httpClient = new HttpClient();
            this.auswertungServiceUrl = auswertungServiceUrl;
        }

        public void PushMessage(Message<T> message)
        {
            messageQueue.Enqueue(message);
            SendMessageToAuswertungsServices(message);

        }
        private async void SendMessageToAuswertungsServices<String>(Message<String> message)
        {
            var content = new StringContent(JsonConvert.SerializeObject(message.Inhalt), Encoding.UTF8, "application/json");
            await httpClient.PostAsync(auswertungServiceUrl, content);
        }
        public Message<T> PopMessage()
        {
            if (messageQueue.Count > 0)
            {
                return messageQueue.Dequeue();
            }
            else
            {
                return null;
            }
        }
        public List<Message<T>> GetAllMessages()
        {
            return messageQueue.ToList();
        }

        public long GetNewLongId()
        {
            return DateTime.Now.Ticks;
        }
    }
}
