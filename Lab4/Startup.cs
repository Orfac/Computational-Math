using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab4.Models;
using Lab4.Services.Parsers;
using Lab4.Models.Methods;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Lab4
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
            app.Map("/diffirentiate", diffirentiate);
            app.Map("/getSingle", getSingle);
        }
        
        public static void getSingle(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                try
                {
                    var input = SingleXInput.getFromRequest(context);
                    var lagrange = new LagrangeMethod();
                    var differentialValues = DiffirentialEquationSolver.lastValues;
                    double[] resx = new double[differentialValues.Count];
                    double[] resy = new double[differentialValues.Count];
                    for (int i = 0; i < differentialValues.Count; i++)
                    {
                        resx[i] = differentialValues[i].X;
                        resy[i] = differentialValues[i].Y;
                    }
                    var result = lagrange.getY(resx,resy,input.X);
                    
                    await context.Response.WriteAsync(result.ToString());
                }
                catch (Exception ex)
                {
                    await context.Response.WriteAsync(ex.Message);
                }
                
                
            });
        }
        public static void diffirentiate(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                try
                {
                    var input = DiffirentialEquationInput.getFromRequest(context);
                    

                    DiffirentialEquationSolver.Update(input);
                    var resultDiff = DiffirentialEquationSolver.Solve();

                    var interpolater = new Interpolater(0);
                    var resultInterpolate = interpolater.Interpolate(
                        resultDiff.xData,0,resultDiff.yData
                    );
                    resultDiff.xData = resultInterpolate.xData;
                    resultDiff.yData = resultInterpolate.yData;
                    await context.Response.WriteAsync(resultDiff.ToString());
                }
                catch (Exception ex)
                {
                    await context.Response.WriteAsync(ex.Message);
                }
                
                
            });
        }     
    }
}