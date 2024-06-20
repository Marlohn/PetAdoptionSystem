using PetAdoptionSystem.Infra.Middlewares;
using PetAdoptionSystem.IoC.Extensions;
using PetAdoptionSystem.ServiceDefaults;

namespace PetAdoptionSystem.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        string dbConnectionString = builder.Configuration.GetConnectionString("marlohnDb") ?? string.Empty; // Fix it
        builder.Services.AddProjectDependencies();
        builder.Services.AddJwtDependencies();
        builder.Services.AddDatabaseExecutorDependencies(dbConnectionString);
        builder.Services.AddCacheDependencies();


        builder.Services.AddCustomAuthentication(builder.Configuration);
        builder.Services.AddCustomAuthorization();
        builder.Services.AddCustomSwagger();


        var app = builder.Build();

        app.MapDefaultEndpoints();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
