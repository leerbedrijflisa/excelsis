﻿using Lisa.Excelsis.WebApi.Models;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Data.Entity;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Lisa.Excelsis.WebApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
                options.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Error;
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
            app.UseCors("CorsExcelsis");
            app.UseMvcWithDefaultRoute();
            app.UseStaticFiles();            
        }
    }
}