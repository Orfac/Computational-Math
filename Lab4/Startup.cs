using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab4.Models;
using Lab4.Services.Parsers;
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
        }
        
        public static void diffirentiate(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                try
                {
                    var input = DiffirentialEquationInput.getFromRequest(context);
                    var solver = new DiffirentialEquationSolver();

                    solver.Update(input);
                    var result = solver.Solve();

                    
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