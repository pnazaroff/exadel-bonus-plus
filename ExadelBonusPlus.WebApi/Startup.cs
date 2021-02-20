using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using ExadelBonusPlus.WebApi.Options;
using ExadelBonusPlus.WebApi.Installers;

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
            services.InstallServicesInAssembly(_configuration);
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

            var swaggerOptions = new SwaggerOptions();
            _configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger(options =>
            {
                options.RouteTemplate = swaggerOptions.JsonRoute;
            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint
                (swaggerOptions.UiEndpoint, swaggerOptions.Description);
            });

            app.UseRouting();

            
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(builder => builder.AllowAnyOrigin()
                                          .AllowAnyMethod()
                                          .AllowAnyHeader());


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", context =>
                {
                    context.Response.Redirect("/swagger/");
                    return Task.CompletedTask;
                });
                endpoints.MapControllers();
            });
        }
    }
}
