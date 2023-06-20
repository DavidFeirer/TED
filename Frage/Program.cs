using FrageService.Model;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Consul;

namespace FrageService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddControllers().AddOData(opt => opt.Select().Filter().OrderBy());
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<FrageContext>(opt =>
                           opt.UseInMemoryDatabase("FrageList"));

            builder.Services.AddSingleton<IConsulClient>(sp => new ConsulClient(config =>
            {
                config.Address = new Uri("http://localhost:8500"); // Adresse des lokalen Consul-Agents
            }));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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