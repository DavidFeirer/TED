using SecretverwaltungService.Model;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Consul;
using SecretverwaltungService.Services;

namespace SecretverwaltungService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddControllers().AddOData(opt => opt.Select().Filter().OrderBy());

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddSingleton<IConsulClient>(sp => new ConsulClient(config =>
            {
                config.Address = new Uri("http://localhost:8500");
            }));

            builder.Services.AddScoped<HttpClient>();
            builder.Services.AddScoped<SecretsService>();

            builder.Services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });

            var app = builder.Build();

            //if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}