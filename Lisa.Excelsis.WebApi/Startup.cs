using Lisa.Excelsis.WebApi.Models;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Data.Entity;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;

namespace Lisa.Excelsis.WebApi
{

    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            // Setup configuration sources.
            var builder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        { 
            services.AddMvc();
            services.AddEntityFramework()
               .AddSqlServer()
               .AddDbContext<ExcelsisDb>(options =>
               {
                   options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]);
               });

            services.ConfigureCors(options =>
            {
                options.AddPolicy(
                   "CorsExcelsis",
                    builder =>
                    {
                        builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
                    });
            });            
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvcWithDefaultRoute();
            app.UseStaticFiles();
            app.UseCors("CorsExcelsis");
            DummieData.LoadDummieData();
        }
    }
}
