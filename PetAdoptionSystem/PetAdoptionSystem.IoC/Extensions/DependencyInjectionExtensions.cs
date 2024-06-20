using Microsoft.Extensions.DependencyInjection;
using PetAdoptionSystem.Application.Interfaces;
using PetAdoptionSystem.Application.Services;
using PetAdoptionSystem.Domain.Interfaces;
using PetAdoptionSystem.Infra.Repositories;
using PetAdoptionSystem.Infra.Services;

namespace PetAdoptionSystem.IoC.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddProjectDependencies(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IDatabaseExecutor>(provider => new DatabaseExecutor(connectionString));
        }

        public static void AddJwtDependencies(this IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService>();
        }
    }
}