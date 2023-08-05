using FlightSimulator.Data;
using FlightSimulator.Logic;
using FlightSimulator.Models;
using FlightSimulator.Repositories;
using FlightSimualtor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FlightSimulator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //This method gets called by the runtime.Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //Add other services here
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:3000");
                    });
            });
            //Register the AirportContext class and configure it to use a local SQLite database
            services.AddDbContext<AirportContext>(options =>
                options.UseSqlite("Data Source=Airport.db"));
            services.AddScoped<IRepository, Repository>();
            services.AddSingleton<IFlightManager, FlightManager>();
            services.AddSingleton<ISimulator, Simulator>();
            services.AddMvc();
            services.AddRouting();
            services.AddHttpClient();

        }

        //This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "api/{controller=Home}/{action=Get}");
            });
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope();
            var flightManager = app.ApplicationServices.GetRequiredService<IFlightManager>();
            var context = serviceScope.ServiceProvider.GetRequiredService<AirportContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            //context.Database.Migrate();
            flightManager.Initialize();
        }
    }
}
