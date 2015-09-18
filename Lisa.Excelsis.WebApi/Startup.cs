using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;

namespace Lisa.Excelsis.WebApi
{

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

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
            DummieData.LoadDummieData();
            app.UseCors("CorsExcelsis");
        }
    }
}
