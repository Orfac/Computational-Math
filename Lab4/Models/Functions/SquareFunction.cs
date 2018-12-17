using System;

namespace Lab4.Models.Functions
{
    public class SquareFunction : IFunction
    {
        public string Name => "x^2 - 2x + 1";

        public double getY(double x, double y)
        {
            return x * x - 2*x + 1;
        }
    }
}