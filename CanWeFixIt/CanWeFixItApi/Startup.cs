using CanWeFixItService.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using CanWeFixItApi.MappingProfiles;
using System.Data;
using Microsoft.Data.Sqlite;

namespace CanWeFixItApi
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CanWeFixItApi", Version = "v1" });
            });
            
            services.AddLogging(config =>
            {
                config.AddDebug();
                config.AddConsole();
            });
            
            services.AddSingleton(typeof(ILogger), typeof(Logger<Startup>));
            services.AddSingleton<IDatabaseService, DatabaseService>();
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddSingleton<IDbConnection>(db => new SqliteConnection(Configuration.GetConnectionString("SqliteConnectionString")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CanWeFixItApi v1"));
            }

            // Populate in-memory database with data
            var database = app.ApplicationServices.GetService(typeof(IDatabaseService)) as IDatabaseService;
            database?.SetupDatabase();
            
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}