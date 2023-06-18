using Fragenevaluierung.Service;

using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICheckFrage, CheckFrage>();

// Registrierung des Consul-Clients als Singleton
builder.Services.AddSingleton<IConsulClient>(sp => new ConsulClient(config =>
{
    config.Address = new Uri("http://localhost:8500"); // Adresse des lokalen Consul-Agents
}));

// Registrierung des Microservices bei Consul
builder.Services.AddSingleton<IHostedService>(sp =>
{
    var consulClient = sp.GetRequiredService<IConsulClient>();
    var serviceName = "fragenevaluierung";
    var serviceId = System.Guid.NewGuid().ToString();
    var servicePort = 8080;

    // Metadaten für den Microservice
    var registration = new AgentServiceRegistration
    {
        ID = serviceId,
        Name = serviceName,
        Port = servicePort,
        Tags = new[] { "tag1", "tag2" }
    };

    return new ConsulHostedService(registration, consulClient);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
