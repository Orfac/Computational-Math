using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using Lab3.Services.Methods;
using Lab3.Services.Parsers;
using System.IO;
using Lab3.Services.Functions;
using System.Text;

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
                Parser parser = new Parser();
                string text = context.Request.Query["xData"];
                double[] xData = parser.parseArray(text);
                FunctionRepository repo = new FunctionRepository();
                repo.addFunction(new SinFunction());
                repo.addFunction(new SquareFunction());
                double[] yData = new double[xData.Length];
                for (int i = 0; i < xData.Length; i++)
                {
                    yData[i] = repo.GetFunction(1).getY(xData[i]);       
                }


                double[] newXData = new double[xData.Length];
                double[] newYData = new double[xData.Length];
                for (int i = 0; i < newXData.Length; i++)
                {
                    newXData[i] = xData[i];
                }

                IInterpolationMethod method = new LagrangeMethod();
                for (int i = 0; i < newXData.Length; i++)
                {
                    newYData[i] = method.getY(xData,yData,newXData[i]);
                }

                var sb = new StringBuilder();
                for (int i = 0; i < newXData.Length; i++)
                {
                    sb.Append(newYData[i]);
                    sb.Append(' ');
                }
                sb.Remove(sb.Length - 1,1);
                await context.Response.WriteAsync(sb.ToString());
            });
        }     
    }
}