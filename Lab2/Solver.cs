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
        /// <returns>Численное значение интеграла</returns>
        public double GetSolution(double bottomLimit, double topLimit, 
            Function func, double accuracy)
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
            var iterationCount = length / accuracy;

            if (NumericComparer.Compare(iterationCount,0)) 
                return GetSingleIntegral(a,b,func);

            for (var i = 0.0; !(NumericComparer.Compare(i,length) || i > length); i += accuracy){
                integral += GetSingleIntegral(a + i, b, func);
            }             

            if (isUnfolded)
            {
                integral *= (-1);
            }

            return integral;
        }

        private double GetSingleIntegral(double a, double b, Function func)
        {
            double middleValue = (a + b) / 2;
            double firstMultiplier = (b - a) / 6;
            
            double secondMultiplier = func.GetY(a);      
            secondMultiplier += 4 * func.GetY(middleValue);
            secondMultiplier += func.GetY(b);
            
            return firstMultiplier * secondMultiplier;
        }
    }
}