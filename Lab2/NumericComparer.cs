using System;

namespace Lab2
{
    public class NumericComparer
    {
        public static bool Compare(double x, double y)
        {
            return Math.Abs(x - y) < double.Epsilon;
        }
    }
}