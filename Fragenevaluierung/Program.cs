using Fragenevaluierung.Service;

using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();


var servicePort = Environment.GetEnvironmentVariable("SERVICE_PORT");

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICheckFrage, CheckFrage>();

builder.Services.AddLogging(builder =>
{
    builder.AddConsole();
    builder.AddDebug();
});


builder.Services.AddSingleton<IConsulClient>(sp => new ConsulClient(config =>
{
    config.Address = new Uri("http://localhost:8500");
}));

// Registrierung des Microservices bei Consul
builder.Services.AddSingleton<IHostedService>(sp =>
{
    var consulClient = sp.GetRequiredService<IConsulClient>();
    var serviceName = "fragenevaluierung";
    var serviceId = System.Guid.NewGuid().ToString();

    var registration = new AgentServiceRegistration
    {
        ID = serviceId,
        Name = serviceName,
        Port = int.Parse(servicePort ?? "5001"),
        Tags = new[] { "boolean", "post" }
    };

    return new ConsulHostedService(registration, consulClient);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
