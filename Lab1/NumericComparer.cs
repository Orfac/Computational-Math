using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GaussMethod
{
    public class NumericComparer
    {
        public static double _error = 1E-7;

        public static bool Compare(double x, double y)
        {
            return x - y < _error;
        }
    }
}