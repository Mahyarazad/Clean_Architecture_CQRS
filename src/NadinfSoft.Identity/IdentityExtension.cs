using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NadinfSoft.Identity.Data;
using NadinfSoft.Identity.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NadinSoft.Application.Abstractions.Identity;
using NadinfSoft.Identity.Services;

namespace NadinfSoft.Identity
{
    public static class IdentityExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityServer"));
            });

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            IdentityBuilder builder = services.AddIdentityCore<ApplicationUser>()
                .AddEntityFrameworkStores<UserDbContext>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddDefaultTokenProviders();


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = configuration["JwtSettings:Audience"],
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"]))
                    };
                }); ;



            services.AddScoped<IAuthService, AuthService>();

            return services;
        }

        public static IApplicationBuilder UseIdentity(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }
    }
}
