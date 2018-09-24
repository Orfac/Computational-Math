using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GaussMethod
{
    class Program
    {
        public static void Main(string[] args)
        {
            Matrix matrix = null;
            InputMatrix(ref matrix);
            
            Console.WriteLine(matrix);
            Console.Write("Определитель матрицы: ");
            Console.WriteLine(matrix.Determinant);
            double[] completedMatrix = Solver.Solve(matrix);
            if (completedMatrix == null)
            {
                Console.WriteLine("Матрицу не удалось решить методом Гаусса");
                
            }
            else
            {
                Console.WriteLine("Треугольный вид");
                Console.WriteLine(matrix);
                decimal[] errors = matrix.GetErrors(completedMatrix);
                Console.WriteLine("Решения и невязки");
                for (int i = 0; i < completedMatrix.Length; i++)
                {
                    Console.WriteLine($"X{i}: {completedMatrix[i]} | error: {errors[i]} ");
                }
                
            }
           

        }

        private static void InputMatrix(ref Matrix matrix)
        {
            do
            {
                int k = AskCount();
                
                ShowMenu();
                char choice = Console.ReadKey(true).KeyChar;
                switch (choice)
                {
                    case '1':
                        matrix = ConsoleInput(k);
                        break;
                    case '2':
                        matrix = FileInput(k);
                        break;
                    case '3':
                        matrix = RandomInput(k);
                        break;
                    default:
                        matrix = null;
                        break;
                }

            } while (matrix == null);
        }

        private static int AskCount()
        {
            bool isInputCorrect = true;
            do
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("Введите размер матрицы k");
                isInputCorrect = int.TryParse(Console.ReadLine(), out int k);
                if (isInputCorrect)
                {
                    return k;
                }
            } while (true);
            
        }

        private static void ShowMenu()
        {        
            Console.WriteLine("Нажмите соответствующую клавишу для выбора:");
            Console.WriteLine("1) Задать из консоли");
            Console.WriteLine("2) Задать из файла");
            Console.WriteLine("3) Задать случайно");
        }

        private static Matrix RandomInput(int k)
        {
            double min, max;
            Console.WriteLine("Введите минимальное значение");
            try
            {
                min = double.Parse(Console.ReadLine());
                Console.WriteLine("Введите максимальное значение");
                max = double.Parse(Console.ReadLine());
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            if (min > max || NumericComparer.Compare(min,max))
            {
                Console.WriteLine("Максимальное значение должно быть строго больше минимального");
                return null;
            }
            return new Matrix(RandomGenerator(k, min, max));
        }

        private static Matrix FileInput(int k)
        {
            Console.WriteLine("Введите путь к файлу");
            string fileName = "input.txt";
            //string fileName = Console.ReadLine();
            if (!File.Exists(fileName))
            {
                Console.WriteLine("Файл не существует");
                return null;
            }
            using (var sr = new StreamReader(fileName))
            {
                string source = sr.ReadToEnd();
                Matrix result = Matrix.Parse(source, k, out string message);
                if (!string.IsNullOrEmpty(message))
                {
                    Console.WriteLine(message);
                }
                return result;
            }
        }

        private static Matrix ConsoleInput(int k)
        {
            var matrixString = new StringBuilder();
            for (int i = 0; i < k; i++)
            {
                Console.WriteLine($"Введите уравнение №{i} ");
                matrixString.Append(Console.ReadLine());
                matrixString.Append(' ');
            }
            Matrix result = Matrix.Parse(matrixString.ToString(), k, out string message);
            if (!string.IsNullOrEmpty(message))
            {
                Console.WriteLine(message);
            }
            return result;
        }

        private static double[,] RandomGenerator(int size, double min, double max)
        {
            Random rgen = new Random();
            double[,] randomMatrix = new double[size, size + 1];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size + 1; j++)
                {
                    randomMatrix[i,j] = rgen.NextDouble() * (max - min) + min;
                }
            }
            return randomMatrix;
        }

    }
}
