using System;

namespace Lab2
{
    public class Solver
    {
        private Function _function;

        public Solver(Function function)
        {
            _function = function;
        }

        public decimal GetSolution(decimal a, decimal b, decimal accuracy)
        {
            decimal middleValue = (a + b) / 2;
            decimal firstMultiplier = (b - a) / 6;
            
            decimal secondMultiplier = _function.GetY(a);      
            secondMultiplier += 4 * _function.GetY(middleValue);
            secondMultiplier += _function.GetY(b);
            
            return firstMultiplier * secondMultiplier;
        }
    }
}