using System;

namespace Lab3.Models.Functions
{
    public class EPow : IFunction
    {
        public string Name => "e^x+2";

        public double getY(double x)
        {
            return Math.Pow(Math.E, x) + 2;
        }
    }
}