using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Lab3.Services.Parsers;
using System.IO;
using System.Text;
using Lab3.Models.Methods;
using Lab3.Models.Functions;
using Lab3.Models;
using System;

namespace Lab3
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.Map("/Interpolate", Interpolate);
        }

        public static void Interpolate(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                try
                {
                    var input = InterpolaterInput.getFromRequest(context);
                    var interpolater = new Interpolater(input.Offset);
                    var result = interpolater.Interpolate(input.XData, funcNumber:input.FuncType);
                    
                    await context.Response.WriteAsync(result.ToString());
                }
                catch (Exception ex)
                {
                    await context.Response.WriteAsync(ex.Message);
                }
                
                
            });
        }     
    }
}