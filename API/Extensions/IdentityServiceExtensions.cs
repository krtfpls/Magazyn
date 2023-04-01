using System.Text;
using API.Services;
using Data;
using Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
        IConfiguration config)
        {
//ASP Identity User Manager
            services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireNonAlphanumeric= false;
                opt.Password.RequiredLength=8;
                opt.User.RequireUniqueEmail=true;
                opt.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<DataContext>()
            .AddSignInManager<SignInManager<User>>()
            .AddDefaultTokenProviders();

// JWT Token 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                                .GetBytes(config["JWT:Secret"])),
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime= true,
                            ValidAudience=config["JWT:ValidAudience"],
                            ValidIssuer= config["JWT:ValidIssuer"],
                            ClockSkew = TimeSpan.Zero,

                            RequireSignedTokens= true,
                            RequireExpirationTime= true
                        }
                        );
            
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan= TimeSpan.FromHours(3));

            return services;
        }
    }
}