using System;
using Lab2.Functions;

namespace Lab2
{
    public class Solver
    {
        private const int MinimalSplitNumber = 100;
        private const double RungeCoefficient = 1/15;
        /// <summary>
        /// Считает интеграл между выбранными значениями с необходимой точностью
        /// </summary>
        /// <param name="a">Нижний предел</param>
        /// <param name="b">Верхний предел</param>
        /// <param name="func">Функция для интегрирования </param>
        /// <param name="accuracy">Точность</param>
        /// <returns></returns>
        public Solution GetSolution(
            Function func, double a, double b, double accuracy)
        {
            if (func == null) throw new NullReferenceException("Функция не задана");
            if (NumericComparer.Compare(a,b)) return new Solution
            {
                Integral = 0,
                Count = 0,
                Error = 0
            };

            var isUnfolded = b < a;
            if (isUnfolded)
            {
                (a, b) = (b, a);
            }

            int n = MinimalSplitNumber;
            double integral = GetDistributedIntegral(a,b,n,func);
            double doubleNIntegral = GetDistributedIntegral(a,b,n*2, func);
            double error = RungeCoefficient * Math.Abs(integral - doubleNIntegral);
            
            while (error > accuracy)
            {
                n *= 2;
                if (n * 2 == int.MaxValue)
                {
                    throw new OverflowException 
                        ("Невозможно получить решение с заданной точностью");
                }           
                integral = GetDistributedIntegral(a,b,n,func);
                error = RungeCoefficient * Math.Abs(integral - GetDistributedIntegral(a,b,n*2,func));
            }

            if (isUnfolded)
            {
                integral *= -1;
            }

            return new Solution
            {
                Integral = integral,
                Count = n,
                Error = error
            };
        }

        private double GetDistributedIntegral(double a, double b, int n, Function func)
        {
            double length = b - a;
            double step = length / n;

            double integral = 0;
            for (double offset = 0; offset + step < length; offset += step)
            {
                double leftLimit = a + offset;
                double rightLimit = leftLimit + step;
                integral += GetSingleIntegral(leftLimit, rightLimit, func);
            }
            return integral;
        }

        private double GetSingleIntegral(double a, double b, Function func)
        {
            
            double firstMultiplier = (b - a) / 6;        
            double secondMultiplier = func.GetY(a);

            double middleValue = (a + b) / 2;
            secondMultiplier += 4 * func.GetY(middleValue);
            secondMultiplier += func.GetY(b);
            
            return firstMultiplier * secondMultiplier;
        }
    }
}