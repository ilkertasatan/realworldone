using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RealWorldOne.UserManagement.Api.Extensions;

namespace RealWorldOne.UserManagement.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddApiControllers()
                .AddHealthCheck()
                .AddSwagger()
                .AddMediatR()
                .AddInMemoryDatabase()
                .AddFluentValidation()
                .AddUseCases(Configuration);
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/healthz/ready", new HealthCheckOptions
                {
                    Predicate = check => !check.Name.Contains("Liveness"),
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecks("/healthz/live", new HealthCheckOptions
                {
                    Predicate = check => check.Name.Contains("Liveness"),
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                
                endpoints.MapControllers();
            });
        }
    }
}