using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

using ProxetTournamentFinale.Data;

namespace ProxetTournamentFinale
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
            services.AddControllers();

            // Add database context
            services.AddDbContext<PlayersContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("PlayersContext")));

            // Map PlayersContext to IPlayersContext
            services.AddScoped<IPlayersContext, PlayersContext>();

            // Add database health check
            services.AddHealthChecks().AddDbContextCheck<PlayersContext>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                // Map health checks to /api/v1/healthcheck endpoint
                endpoints.MapHealthChecks("/api/v1/healthcheck");
            });
        }
    }
}
