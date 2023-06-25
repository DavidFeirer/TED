using Consul;

namespace Fragenevaluierung.Service
{
    public class ConsulHostedService : IHostedService
    {
        private readonly AgentServiceRegistration _registration;
        private readonly IConsulClient _consulClient;

        public ConsulHostedService(AgentServiceRegistration registration, IConsulClient consulClient)
        {
            _registration = registration;
            _consulClient = consulClient;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _consulClient.Agent.ServiceRegister(_registration, cancellationToken);
            }
            catch (Exception)
            {
                Console.WriteLine("could not reach Consul-agent");
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _consulClient.Agent.ServiceDeregister(_registration.ID, cancellationToken);
            }
            catch (Exception)
            {
                Console.WriteLine("could not reach Consul-agent");
            }
        }
    }
}
