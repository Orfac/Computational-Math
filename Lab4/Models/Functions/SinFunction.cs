using System;

namespace Lab4.Models.Functions
{
    public class SinFunction : IFunction
    {
        public string Name => "sin(x)";

        public double getY(double x, double y)
        {
            return Math.Sin(x);
        }
    }
}