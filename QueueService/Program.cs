using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using QueueService.Model;
using QueueService.Services;

internal class Program
{
    private static void Main<T>(string[] args) 
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.;
        builder.Services.AddSingleton<MessageQueueService<T>>(provider =>
        {
            var evaluationServiceUrl = "https://localhost:7118/api/AuswertungsController/queue"; // Die URL des Auswertungs-Service
            return new MessageQueueService<T>(evaluationServiceUrl);
        });
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddDbContext<MessageContext<T>>(opt =>
            opt.UseInMemoryDatabase("QueueList"));
        builder.Services.AddSwaggerGen();

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