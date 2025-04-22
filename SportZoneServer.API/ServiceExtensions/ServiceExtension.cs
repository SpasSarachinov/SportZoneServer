using Microsoft.AspNetCore.Identity;
using SportZoneServer.API.Helpers;
using SportZoneServer.Data.Entities;
using SportZoneServer.Data.Interfaces;
using SportZoneServer.Data.Repositories;

namespace SportZoneServer.API.ServiceExtensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            // SERVICES
            services.AddTransient<IEmailSender<User>, DummyEmailSender>();

            // REPOS
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}