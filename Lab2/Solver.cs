using System;
using Lab2.Functions;

namespace Lab2
{
    public class Solver
    {
        private Function _function;

        public Solver(Function function)
        {
            _function = function;
        }
        /// <summary>
        /// Calculates integral from a to b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public double GetSolution(double a, double b)
        {
            double middleValue = (a + b) / 2;
            double firstMultiplier = (b - a) / 6;
            
            double secondMultiplier = _function.GetY(a);      
            secondMultiplier += 4 * _function.GetY(middleValue);
            secondMultiplier += _function.GetY(b);
            
            return firstMultiplier * secondMultiplier;
        }
    }
}