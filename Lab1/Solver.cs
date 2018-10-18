using System;

namespace Lab1
{
    public class Solver
    {
        public double[] Solve(Matrix matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException();

            if (matrix.Width != matrix.Height + 1)
                throw new ArgumentException(
                    "Система уравнений не подходит для решения методом Гаусса," +
                    " кол-во строк должно быть на 1 меньше, чем кол-во столбцов");

            if (!matrix.IsTriangular()) ToTriangular(ref matrix);
            CheckZeroOrInfinitySolutions(matrix);

            return FindSolutions(matrix);
        }

        private void CheckZeroOrInfinitySolutions(Matrix matrix)
        {
            bool noSolutions = false;
            bool infinitySolutions = false;
            for (var i = 0; i < matrix.Height; i++)
            {
                bool isEquationCorrect = false;
                for (var j = i; j < matrix.Width - 1; j++)
                {
                    if (!NumericComparer.Compare(matrix[i, j], 0)) isEquationCorrect = true;
                }

                if (isEquationCorrect) continue;
                if (NumericComparer.Compare(matrix[i, matrix.Width - 1], 0))
                {
                    infinitySolutions = true;
                }
                else
                {
                    noSolutions = true;
                }
            }
            if (noSolutions) throw new ArgumentException("Нет решений");
            if (infinitySolutions) throw new ArgumentException("Бесконечное число решений");
        }
        
        private double[] FindSolutions(Matrix matrix)
        {
            var height = matrix.Height;
            var width = matrix.Width;
            var solutions = new double[height];
            
            for (var i = height - 1; i >= 0; i--)
            {
                
                // Запись числа из столбца значений
                var x = matrix[i, width - 1];
                
                // Подставляем известные переменные
                for (var j = i + 1; j < width - 1; j++)
                    x -= matrix[i, j] * solutions[j];

                // Находим новую переменную поделив на коэффициент перед ней
                x /= matrix[i, i];
                if (double.IsNaN(x))
                {
                    throw new ArgumentException("");
                }
                // Записываем переменную в ответ
                solutions[i] = x;
            }

            return solutions;
        }

        private void ToTriangular(ref Matrix matrix)
        {
            for (var i = 0; i < matrix.Height; i++)
            for (var j = i + 1; j < matrix.Height; j++)
                // Если равно 0, то при решении Гауссом найдём бесконечное число решений
                if (!NumericComparer.Compare(matrix[i, i], 0))
                {
                    // Для каждого элемента "обнуляем" его
                    if (!NumericComparer.Compare(matrix[j, i], 0))
                        matrix.AddRow(i, j, -matrix[j, i] / matrix[i, i]);
                }
                else
                {
                    throw new ArgumentException
                        ("Матрицу невозможно привести к ступенчатому виду");
                }
        }
    }
}