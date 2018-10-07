using System;

namespace Lab2.Functions
{
    public class SqrtFunction : Function
    {
        public override double GetY(double x)
        {
            return Math.Sqrt(x);
        }

        public override string ToString()
        {
            return "sqrt(x)";
        }
    }
}