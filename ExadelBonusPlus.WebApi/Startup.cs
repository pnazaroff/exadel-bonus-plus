using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models;
using AutoMapper;
using ExadelBonusPlus.DataAccess;
using ExadelBonusPlus.Services;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace ExadelBonusPlus.WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MongoDbSettings>(_configuration.GetSection(
                nameof(MongoDbSettings)));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true; //This string is needed for fluent validation in action filter
                })
                .AddFluentValidation();

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "exadel-bonus-plus API V1",
                });
            });

            services.AddBonusTransient(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions
                {
                    SourceCodeLineCount = 5
                };
                app.UseDeveloperExceptionPage(developerExceptionPageOptions);
            }
            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Exadel Bonus Plus API v1")
            );

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", context => {
                    context.Response.Redirect("/swagger/");
                    return Task.CompletedTask;
                });

                endpoints.MapControllers();

            });
        }
    }
}
