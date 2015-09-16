using Lisa.Excelsis.WebApi.Models;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Lisa.Excelsis.WebApi
{

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvcWithDefaultRoute();
            DummieData.LoadDummieData();
        }
    }
}
