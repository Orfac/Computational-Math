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
                string text = context.Request.Query["xData"];
                var parser = new Parser();
                double[] xData = parser.parseArray(text);

                var interpolater = new Interpolater();
                double[] newYData = interpolater.Interpolate(2,xData);
                var sb = new StringBuilder();
                for (int i = 0; i < size2; i++)
                {
                    sb.Append(newXData[i]);
                    sb.Append(' ');
                    sb.Append(newYData[i]);
                    sb.Append(' ');
                }
                sb.Remove(sb.Length - 1,1);
                await context.Response.WriteAsync(sb.ToString());
            });
        }     
    }
}