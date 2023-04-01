using API.Infrastructure;
using API.Services;
using Application.Documents;
using Entities.interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using static API.Services.Security;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserAccessor, UserAccessor>();

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<Create>();
            services.AddScoped<LocalEmailSender>();
             return services;
        }
    }
}