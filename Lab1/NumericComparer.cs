using System;

namespace Lab1
{
    public class NumericComparer
    {
        public static double _error = 1E-7;

        public static bool Compare(double x, double y)
        {
            return Math.Abs(x - y) < _error;
        }
    }
}