using System;
using System.IO;
using System.Text;

namespace Lab1
{
    class Program
    {
        private const int MaxMatrixHeight = 20;

        public static void Main(string[] args)
        {
            Matrix matrix;
            do
            {
                matrix = InputMatrix();
            } while (matrix == null);


            Console.WriteLine(matrix);
            var solver = new Solver();
            var solutions = solver.Solve(matrix);
            if (solutions == null)
            {
                Console.WriteLine("Матрицу не удалось решить методом Гаусса");
            }
            else
            {
                Console.WriteLine("Треугольный вид");
                Console.WriteLine(matrix);
                var errors = matrix.GetErrors(solutions);
                Console.WriteLine("Решения и невязки");
                for (var i = 0; i < solutions.Length; i++)
                    Console.WriteLine($"X{i+1}: {solutions[i]} | error: {errors[i]} ");
            }

            Console.Write("Определитель матрицы: ");
            Console.WriteLine(matrix.GetDeterminant());
        }

        private static Matrix InputMatrix()
        {
            var k = AskCount();
            ShowMenu();

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    return ConsoleInput(k);
                case "2":
                    return FileInput(k);
                case "3":
                    return RandomInput(k);
                default:
                    throw new InvalidDataException();
            }
        }

        private static int AskCount()
        {
            do
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("Введите размер матрицы k");
                var isInputCorrect = int.TryParse(Console.ReadLine(), out var k);
                if (isInputCorrect && k > 0)
                {
                    if (k > MaxMatrixHeight)
                        Console.WriteLine($"K должно быть <= {MaxMatrixHeight}");
                    else
                        return k;
                }
                else
                {
                    Console.WriteLine("K неотрицательно");
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

            if (!(min > max) && !NumericComparer.Compare(min, max)) return new Matrix(RandomGenerator(k, min, max));
            Console.WriteLine("Максимальное значение должно быть строго больше минимального");
            return null;

        }

        private static Matrix FileInput(int k)
        {
            Console.WriteLine("Введите путь к файлу");
            var fileName = Console.ReadLine();
            if (fileName == null || !File.Exists(fileName))
            {
                Console.WriteLine("Файл не существует");
                return null;
            }

            using (var sr = new StreamReader(fileName))
            {
                var source = sr.ReadToEnd();
                return HandleMatrixParse(source, k);
            }
        }

        private static Matrix ConsoleInput(int k)
        {
            var matrixString = new StringBuilder();
            for (var i = 0; i < k; i++)
            {
                Console.WriteLine($"Введите уравнение №{i} ");
                matrixString.Append(Console.ReadLine());
                matrixString.Append(' ');
            }
            return HandleMatrixParse(matrixString.ToString(), k);
            
        }

        private static double[,] RandomGenerator(int size, double min, double max)
        {
            var gen = new Random();
            var randomMatrix = new double[size, size + 1];
            for (var i = 0; i < size; i++)
            for (var j = 0; j < size + 1; j++)
                randomMatrix[i, j] = gen.NextDouble() * (max - min) + min;
            return randomMatrix;
        }

        private static Matrix HandleMatrixParse(string source, int k)
        {
            Matrix result;
            try
            {
                result = Matrix.Parse(source, k);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
                return null;
            }

            return result;
        }
    }
}