using Invoice.Domain;
using InvoiceAPIMethods;
using LinqToDB.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Invoice.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //DataConnection.DefaultSettings = 
            //    new DbSettings(
            //        new Domain.Models.ConnectionStringSettings()
            //        {
            //            Name = Configuration["customConnectionString:name"],
            //            ProviderName = Configuration["customConnectionString:ProviderName"],
            //            ConnectionString = Configuration["customConnectionString:ConnectionString"],
            //        });
            DataConnection.DefaultSettings = new DbSettings(Configuration.GetConnectionString("local"));

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
               .AddJsonOptions(x => x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            services.AddSingleton(typeof(MainCachingPipes));
            services.AddSingleton(typeof(MainCachingControl));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
