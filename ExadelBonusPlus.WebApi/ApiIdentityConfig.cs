using System;
using System.Text;
using ExadelBonusPlus.Services;
using ExadelBonusPlus.Services.Models;
using ExadelBonusPlus.WebApi.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ExadelBonusPlus.WebApi
{
    public static class ApiIdentityConfig

    {
        public static void AddApiIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserService, UserService>();

            services.Configure<AppJwtSettings>(configuration.GetSection(
                nameof(AppJwtSettings)));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(
                    configuration["MongoDbSettings:ConnectionString"],
                    configuration["MongoDbSettings:DatabaseName"])
                .AddSignInManager()
                .AddDefaultTokenProviders();


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddCookie(cfg => cfg.SlidingExpiration = true)
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidIssuer = configuration["AppJwtSettings:Issuer"],
                        ValidateAudience = false,
                        ValidAudience = configuration["AppJwtSettings:Audience"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppJwtSettings:SecretKey"]))
                    };
                });


        }
    }
}
