using System;

namespace Lab4.Models.Functions
{
    public class EPow : IFunction
    {
        public string Name => "e^x+2";

        public double getY(double x,double y)
        {
            return Math.Pow(Math.E, x) + 2;
        }
    }
}