using System;
using Lab2.Functions;

namespace Lab2
{
    public class Solver
    {
        /// <summary>
        /// Считает интеграл между выбранными значениями с необходимой точностью
        /// </summary>
        /// <param name="a">Нижний предел</param>
        /// <param name="b">Верхний предел</param>
        /// <param name="func">Функция для интегрирования </param>
        /// <param name="accuracy">Точность</param>
        /// <returns></returns>
        public double GetSolution(double bottomLimit, double topLimit, 
            Func<double,double> func, double accuracy)
        {
            var a = bottomLimit;
            var b = topLimit;
            var isUnfolded = b < a;
            if (isUnfolded)
            {
                (a, b) = (b, a);
            }
            
            double integral = 0;
            var length = b - a;
            double offset;
            for (offset = 0; offset + accuracy < length; offset += accuracy )
            {
                var aI = a + offset;
                integral += GetSingleIntegral(aI, aI + accuracy, func);
            }

            if (!NumericComparer.Compare(offset + accuracy, b)) 
            {
                integral += GetSingleIntegral(a + offset + accuracy, b, func);
            }

            if (isUnfolded)
            {
                integral *= (-1);
            }

            return integral;
        }

        public double GetSingleIntegral(double a, double b, Func<double, double> func)
        {
            double middleValue = (a + b) / 2;
            double firstMultiplier = (b - a) / 6;
            
            double secondMultiplier = func(a);      
            secondMultiplier += 4 * func(middleValue);
            secondMultiplier += func(b);
            
            return firstMultiplier * secondMultiplier;
        }
    }
}