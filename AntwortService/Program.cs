using AntwortService.Model;
using AntwortService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AntwortService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            string queueServiceUrl = "https://localhost:5001/api/MessageController";
            builder.Services.AddDbContext<AntwortContext>(opt =>
               opt.UseInMemoryDatabase("AntwortList"));
            builder.Services.AddSingleton<UpdateQueue>(new UpdateQueue(queueServiceUrl));
            builder.Services.AddScoped<AntwortManagementService>();

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