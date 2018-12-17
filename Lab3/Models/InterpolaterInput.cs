using Lab3.Services.Parsers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3.Models
{
    public class InterpolaterInput
    {
        public double[] XData { get; set; }
        public int FuncType { get; set; }
        public double Offset { get; set; }

        public static InterpolaterInput getFromRequest(HttpContext context)
        {
            string text = context.Request.Query["xData"];
            string sType = context.Request.Query["funcNumber"];
            string stringOffset = context.Request.Query["offset"];

            // for linux use replacing from , to .
            stringOffset = stringOffset.Replace('.', ',');
            double offset = double.Parse(stringOffset);
            int type = int.Parse(sType);
            var parser = new Parser();
            double[] xData = parser.parseArray(text);

            Array.Sort(xData);

            return new InterpolaterInput
            {
                FuncType = type,
                XData = xData,
                Offset = offset
            };
        }
    }
}
