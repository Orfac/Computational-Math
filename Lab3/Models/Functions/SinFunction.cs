using System;

namespace Lab3.Models.Functions
{
    public class SinFunction : IFunction
    {
        public string Name => "sin(x)";

        public double getY(double x)
        {
            return Math.Sin(x);
        }
    }
}