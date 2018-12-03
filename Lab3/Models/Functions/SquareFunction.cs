using System;

namespace Lab3.Models.Functions
{
    public class SquareFunction : IFunction
    {
        public string Name => "x^2";

        public double getY(double x)
        {
            return x * x;
        }
    }
}