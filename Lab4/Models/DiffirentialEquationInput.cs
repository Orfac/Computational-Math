using Lab4.Services.Parsers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4.Models
{
    public class DiffirentialEquationInput
    {
        public double X0 { get; set; }
        public double Y0 { get; set; }
        public double XN { get; set; }
        public double Accuracy { get; set; }
        public int FuncType { get; set; }

        public static DiffirentialEquationInput getFromRequest(HttpContext context)
        {
            string stringx0 = context.Request.Query["x0"];
            string stringy0 = context.Request.Query["y0"];
            string stringxN = context.Request.Query["xn"];
            string sType = context.Request.Query["funcNumber"];
            string stringAccuracy = context.Request.Query["accuracy"];

            var parser = new Parser();
            return parser.ParseFromStrings(sType,stringx0,stringy0,stringxN,stringAccuracy);
        }

        
    }
}
