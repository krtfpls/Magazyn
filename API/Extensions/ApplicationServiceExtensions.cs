using API.Services;
using Entities.interfaces;
using static API.Services.Security;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserAccessor, UserAccessor>();

             return services;
        }
    }
}