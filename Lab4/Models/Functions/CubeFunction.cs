using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4.Models.Functions
{
    public class CubeFunction : IFunction
    {
        public string Name => "x^3 + 3x + y";

        public double getY(double x,double y)
        {
            return x*x*x + 3*x + y;
        }
    }
}
