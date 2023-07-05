using AntwortService.Model;
using Newtonsoft.Json;
using System.Text;
using RabbitMQ.Client;
using System.Threading.Channels;
using System.Net.NetworkInformation;

namespace AntwortService.Services
{
    public class UpdateQueue
    {
        readonly ConnectionFactory _factory = new ConnectionFactory { HostName = "localhost" };

        public void PostToQueue(Antwort antwort)
        {
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "antworten", type: ExchangeType.Fanout);

            var antwortText = antwort.Text;
            var body = Encoding.UTF8.GetBytes(antwortText);
            channel.BasicPublish(exchange: "antworten",
                                 routingKey: string.Empty,
                                 basicProperties: null,
                                 body: PrepareQueueBody(antwort));


            Console.WriteLine($" [x] Sent {antwortText}");
        }

        public byte[] PrepareQueueBody(Antwort antwort)
        {
            var antwortText = JsonConvert.SerializeObject(antwort);
            var body = Encoding.UTF8.GetBytes(antwortText);
            return body;
        }
    }
}
