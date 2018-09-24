﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GaussMethod
{
    public class Solver
    {
        public static double[] Solve(Matrix matrix)
        {
            if (!Check(matrix, out string message))
            {
                Console.WriteLine(message);
                return null;
            }

            int height = matrix.Height; 
            int width = matrix.Width;
            var result = new double[height];

            for (int i = height - 1; i >= 0; i--)
            {
                // Запись числа из столбца значений
                double unknownValue = matrix[i, width - 1];

                // Подставляем известные переменные
                for (int j = i + 1; j < width - 1; j++)
                    unknownValue -= matrix[i, j] * result[j];

                // Находим новую переменную поделив на коэффициент перед ней
                unknownValue /= matrix[i, i];

                // Записываем переменную в ответ
                result[i] = unknownValue;
            }

            return result;
        }

        public static bool ToTriangular(ref Matrix matrix)
        {
            for (int i = 0; i < matrix.Height; i++)
            {
                for (int j = i + 1; j < matrix.Height; j++)
                {
                    // Если равно 0, то при решении Гауссом найдём бесконечное число решений
                    if (!NumericComparer.Compare(matrix[i, i], 0))
                    {
                        // Для каждого элемента "обнуляем" его
                        if (!NumericComparer.Compare(matrix[j, i], 0))
                            matrix.AddRow(i, j, -matrix[j, i] / matrix[i, i]);
                    }
                    else
                    {
                        return false;
                    }

                }
            }

            return true;
        }

        private static bool Check(Matrix matrix, out string message)
        {
            if (matrix.Width != matrix.Height + 1)
            {
                message = "Система уравнений не подходит для решения методом Гаусса," +
                    " кол-во строк должно быть на 1 меньше, чем кол-во столбцов";
                return false;
            }

            if (!matrix.IsTriangular)
            {
                bool result = ToTriangular(ref matrix);
                if (!result)
                {
                    message = "Матрицу невозможно привести к ступенчатому виду";
                    return result;
                }
            }
            
            message = "";
            return true;
        }
    }
}
