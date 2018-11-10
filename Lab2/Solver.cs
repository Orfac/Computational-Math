using System;
using Lab2.Functions;

namespace Lab2
{
    public class Solver
    {
        private const int MinimalSplitNumber = 5;
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
            int n2 = n * 2;
            double integral = GetSlice(a,b,n2,func);
            double doubleNIntegral = GetSlice(a,b,n2*2, func);
            double error = RungeCoefficient * Math.Abs(integral - doubleNIntegral);
            
            while (error > accuracy)
            {
                n2 *= 2;
                if (n2 * 2 > 10000)
                    throw new OverflowException 
                        ("Ошибка: Не удалось посчитать интеграл с заданной точностью");  

                integral = GetSlice(a,b,n2,func);
                error = RungeCoefficient * Math.Abs(integral - GetSlice(a,b,n2*2,func));
            }
            
            if (!double.IsFinite(integral))
                throw new OverflowException
                    ("Ошибка: Интеграл имеет разрыв на этом промежутке");

            if (isUnfolded)
            {
                integral *= -1;
            }

            return new Solution
            {
                Integral = integral,
                Count = n2,
                Error = error
            };
        }

        private double GetSlice(double a, double b, int n2, Function func)
        {
            double h = (b - a) / n2;
            int n = n2 / 2;

            double integral = func.GetY(a) + func.GetY(b);

            double oddSum = 0;
            for (int i = 1; i <= n; i++)
            {
                double ai = a + (2 * i - 1) * h;
                oddSum += func.GetY(ai);
            }

            double evenSum = 0;
            for (int i = 1; i < n; i++)
            {
                double ai = a + 2 * i * h;
                evenSum += func.GetY(ai);
            }

            integral = (integral + 4 * oddSum + 2 * evenSum) * h / 3;
            return integral;
        }

    }
}