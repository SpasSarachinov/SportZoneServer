using Microsoft.AspNetCore.Identity;
using SportZoneServer.API.Helpers;
using SportZoneServer.Data.Entities;
using SportZoneServer.Data.Interfaces;
using SportZoneServer.Data.Repositories;
using SportZoneServer.Domain.Interfaces;
using SportZoneServer.Domain.Services;

namespace SportZoneServer.API.ServiceExtensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            // SERVICES
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthService, AuthService>();

            // REPOS
            services.AddScoped<IUserRepository, UserRepository>();
            
            return services;
        }
    }
}
