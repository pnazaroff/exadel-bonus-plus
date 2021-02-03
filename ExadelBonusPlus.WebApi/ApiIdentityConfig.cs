using System;
using ExadelBonusPlus.Services.Models;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDbGenericRepository;

namespace ExadelBonusPlus.WebApi
{
    public static class ApiIdentityConfig

    {
        public static void AddApiIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoDbContext = new MongoDbContext(configuration["MongoDbSettings:ConnectionString"], configuration["MongoDbSettings:DatabaseName"]);
            
            services.AddDefaultIdentity<ApplicationUser>()
                .AddMongoDbStores<IMongoDbContext>(mongoDbContext);




            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.Configure<JwtBearerOptions>(
                IdentityServerJwtConstants.IdentityServerJwtBearerScheme,
                options =>
                {
                });

        }
    }
}
