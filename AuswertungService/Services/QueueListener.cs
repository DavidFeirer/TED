using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using AntwortService.Model;

namespace AuswertungService.Services
{
    public class QueueListener : BackgroundService
    {
        readonly ConnectionFactory _factory = new ConnectionFactory { HostName = "localhost" };
        private readonly IServiceProvider _serviceProvider;

        public QueueListener(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "antworten", type: ExchangeType.Fanout);

            // declare a server-named queue
            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName,
                              exchange: "antworten",
                              routingKey: string.Empty);

            Console.WriteLine(" [*] Waiting for antworten.");

            while (!stoppingToken.IsCancellationRequested) {
                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    byte[] body = ea.Body.ToArray();
                    var antwortJSON = Encoding.UTF8.GetString(body);
                    var antwort = JsonConvert.DeserializeObject<Antwort>(antwortJSON);
                    Console.WriteLine($" [x] {antwort.Text}");
                    using(var scope = _serviceProvider.CreateScope())
                    {
                        var _auswertungManagementService = scope.ServiceProvider.GetRequiredService<IAuswertungManagementService>();
                        _auswertungManagementService.SpeichereAntwort(antwort);
                    }
                };

                channel.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
