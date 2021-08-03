using AuhtSystem.Business.Interfaces;
using AuhtSystem.Business.Services;
using AuthSystem.Repository.Interfaces;
using AuthSystem.Repository.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace AuthSystem.WebApi.Configurations
{
    public static class ConfigureDI
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}
