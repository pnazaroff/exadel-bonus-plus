using ExadelBonusPlus.Services;
using ExadelBonusPlus.Services.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Threading.Tasks;

namespace ExadelBonusPlus.WebApi
{
    public class Startup
    {
        private IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
                // requires using Microsoft.Extensions.Options
                services.Configure<ExadelBonusDbSettings>(
                _configuration.GetSection(nameof(ExadelBonusDbSettings)));

            services.AddSingleton<IExadelBonusDbSettings>(sp =>
                sp.GetRequiredService<IOptions<ExadelBonusDbSettings>>().Value);

            services.AddSingleton<VendorService>();


            services.AddControllers();

            services.AddSwaggerGen(options =>
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Exadel Bonus Plus API", Version = "v1" })
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Exadel Bonus Plus API v1")
            );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", context => {
                    context.Response.Redirect("/swagger/");
                    return Task.CompletedTask;
                });

                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});

                endpoints.MapControllers();

            });
        }
    }
}
