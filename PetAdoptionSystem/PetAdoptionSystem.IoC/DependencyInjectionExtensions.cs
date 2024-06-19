using Microsoft.Extensions.DependencyInjection;
using PetAdoptionSystem.Application.Interfaces;
using PetAdoptionSystem.Application.Services;
using PetAdoptionSystem.Domain.Interfaces;
using PetAdoptionSystem.Infra.Repositories;

namespace PetAdoptionSystem.Infra.IoC
{
    public static class DependencyInjectionExtensions
    {
        public static void AddProjectDependencies(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IPetRepository>(provider => new PetRepository(connectionString));
        }
    }
}