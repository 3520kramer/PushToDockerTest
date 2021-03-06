using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SkoleProtokolAPI.Services;
using SkoleProtokolLibrary.Interfaces;
using SkoleProtokolLibrary.DBModels;

namespace SkoleProtokolAPI
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
            
            // Requires using Microsoft.Extensions.Options

            // Populates the RollCallDatabaseSettings object with data
            // from the RollCallDatabaseSettings section in the appsettings.json file.
            services.Configure<RollCallDatabaseSettings>(
                Configuration.GetSection(nameof(RollCallDatabaseSettings)));

            // The IRollCallDatabaseSettings interface is setup for dependency injection as a singleton
            // and when the IRollCallDatabaseSettings type is injected it will resolve to a RollCallDatabaseSettings object
            services.AddSingleton<IRollCallDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<RollCallDatabaseSettings>>().Value);

            // RollCallUsersService is setup for dependency injection as a singleton.
            services.AddSingleton<RollCallUsersService>();
            // RollCallModulesService is setup for dependency injection as a singleton.
            services.AddSingleton<RollCallModulesService>();

            services.AddControllers();

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) // Logging:LogLevel:Default
        {
            Console.WriteLine("ENV:: " + env.EnvironmentName);
            //Console.WriteLine("ENV:: " + env.GetValue("FrontendURL", defaultValue: "not found"));

            Console.WriteLine("ENV: " + Configuration.GetValue("FrontendURL", defaultValue: "not found"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            // Gets the frontendURL from appsettings if we are in development environment. If not it will get it from heroku
            // var frontendURL = env.IsDevelopment() ? Configuration.GetValue("FrontendURL", defaultValue: "not found") : Environment.GetEnvironmentVariable("FrontendURL");

            app.UseCors(options => options
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins("http://localhost:3000")
            );

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
