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
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICategoryService, CategoryService>();

            // REPOS
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            return services;
        }
    }
}
