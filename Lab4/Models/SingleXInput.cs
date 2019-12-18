using Lab4.Services.Parsers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4.Models
{
    public class SingleXInput
    {
        public double X { get; set; }
        
        public static SingleXInput getFromRequest(HttpContext context)
        {
            string stringx0 = context.Request.Query["xi"];
            return new SingleXInput{
                X = double.Parse(stringx0)
            };
        }

        
    }
}
