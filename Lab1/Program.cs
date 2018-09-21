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
            Matrix matrix;
            do
            {
                Console.WriteLine("Введите размер матрицы k");
                int k = int.Parse(Console.ReadLine());
                Console.WriteLine("Выберите вариант задания матрицы");
                Console.WriteLine("1) Задать из консоли");
                Console.WriteLine("2) Задать из файла");
                Console.WriteLine("3) Задать случайно");
                char choice = Console.ReadKey(true).KeyChar;
                switch (choice)
                {
                    case '1':
                        matrix = ConsoleInput(3);
                        break;
                    case '2':
                        matrix = FileInput();
                        break;
                    case '3':
                        matrix = RandomInput();
                        break;
                    default:
                        matrix = null;
                        break;
                }

            } while (matrix == null);
            

        }

        private static Matrix RandomInput()
        {
            try
            {
                Console.WriteLine("Введите размер матрицы k");
                int k = int.Parse(Console.ReadLine());
                Console.WriteLine("Введите минимальное и максимальное значение(double)");
                double min, max;
                min = double.Parse(Console.ReadLine());
                max = double.Parse(Console.ReadLine());
                return new Matrix(RandomGenerator(k, min, max));
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static Matrix FileInput()
        {
            Console.WriteLine("Введите путь к файлу");
            string fileName = Console.ReadLine();
            if (!File.Exists(fileName))
            {
                Console.WriteLine("Файл не существует");
            }
            using (var sr = new StreamReader(fileName))
            {
                int k = int.Parse(sr.ReadLine());

                string source = sr.ReadToEnd();
                Matrix result = Matrix.Parse(source, k, out string message);
                if (!String.IsNullOrEmpty(message))
                {
                    Console.WriteLine(message);
                }
                return result;
            }
        }

        private static Matrix ConsoleInput(int k)
        {
            return null;
        }

        public static double[,] RandomGenerator(int size, double min, double max)
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

        private static void Print(Matrix matrix)
        {
            for (int i = 0; i < matrix.Height; i++)
            {
                for (int j = 0; j < matrix.Width; j++)
                {
                    Console.Write("{0} ",matrix[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
