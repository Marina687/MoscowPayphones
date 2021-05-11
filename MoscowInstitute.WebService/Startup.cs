using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoscowTrafficRestriction.DomainObjects;
using MoscowTrafficRestriction.DomainObjects.Ports;
using MoscowTrafficRestriction.ApplicationServices.GetTrafficRestrictionListUseCase;
using MoscowTrafficRestriction.ApplicationServices.Ports.Gateways.Database;
using MoscowTrafficRestriction.InfrastructureServices.Gateways.Database;
using Microsoft.EntityFrameworkCore;
using MoscowTrafficRestriction.ApplicationServices.Repositories;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using MoscowTrafficRestriction.WebService.InfrastructureServices.Gateways;
using MoscowTrafficRestriction.WebService.Scheduler;

namespace MoscowTrafficRestriction.WebService 
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TrafficRestrictionContext>(opts =>
                opts.UseSqlite($"Filename={System.IO.Path.Combine(System.Environment.CurrentDirectory, "MoscowTrafficRestriction.db")}")
            );
            services.AddHostedService<ScheduleTask>();
            services.AddScoped<ITrafficRestrictionDatabaseGateway, TrafficRestrictionEFSqliteGateway>();

            services.AddScoped<DbTrafficRestrictionRepository>();
            services.AddScoped<IReadOnlyTrafficRestrictionRepository>(x => x.GetRequiredService<DbTrafficRestrictionRepository>());
            services.AddScoped<ITrafficRestrictionRepository>(x => x.GetRequiredService<DbTrafficRestrictionRepository>());

            services.AddScoped<IGetTrafficRestrictionListUseCase, GetTrafficRestrictionListUseCase>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
