using System;
using System.IO;
using System.Text;

namespace Lab1
{
    public class IO
    {
        private enum InputType
        {
            Console,
            File,
            Random
        }
        public Matrix InputMatrixDialog(int maxMatrixHeight)
        {
            var type = AskInputType();
            var k = AskCount(maxMatrixHeight);
            return InputMatrix(type, k);
        }

        public void PrintMessage(string message) => Console.WriteLine(message);
       
        public void PrintSolvedMatrixInfo(Matrix matrix, double[] solutions, decimal[] errors)
        {
            Console.WriteLine("Треугольный вид");
            Console.WriteLine(matrix);
            Console.WriteLine("Решения и невязки");
            for (var i = 0; i < solutions.Length; i++)
                Console.WriteLine($"X{i+1}: {solutions[i]} | error: {errors[i]} ");
            Console.Write("Определитель матрицы: ");
            Console.WriteLine(matrix.GetDeterminant().ToString("0.#######"));
        }
        
        private Matrix InputMatrix(InputType type, int k)
        {
            switch (type)
            {
                case InputType.Console:
                    return ConsoleInput(k);
                case InputType.File:
                    return FileInput(k);
                case InputType.Random:
                    return RandomInput(k);
                default:
                    throw new InvalidDataException("Ошибка: Не удалось определить тип ввода матрицы");
            }
        }

        private InputType AskInputType()
        {
            do
            {
                ShowMenu();
                var choice = Console.ReadLine();
                try
                {
                    return GetInputType(choice);                    
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (true);
        }

        private InputType GetInputType(string choice)
        {
            switch (choice)
            {
                case "1":
                    return InputType.Console;
                case "2":
                    return InputType.File;
                case "3":
                    return InputType.Random;
                default:
                    throw new InvalidCastException("Ошибка: Введите число от 1 до 3");
            }
        }

        private int AskCount(int maxMatrixHeight)
        {
            do
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("Введите размер матрицы k");
                var isNumber = int.TryParse(Console.ReadLine(), out var k);
                if (!isNumber)
                {
                    Console.WriteLine("Ошибка: Введено не целочисленное значение");
                }
                else
                {
                    if (k > 0 && k <= maxMatrixHeight) return k;
                    Console.WriteLine($"Ошибка: Должно выполняться условие - 0 <= K <= {maxMatrixHeight}");
                }
            } while (true);
        }

        private void ShowMenu()
        {
            Console.WriteLine("Нажмите соответствующую клавишу для выбора:");
            Console.WriteLine("1) Задать из консоли");
            Console.WriteLine("2) Задать из файла");
            Console.WriteLine("3) Задать случайно");
        }

        private Matrix RandomInput(int k)
        {
            do
            {
                Console.WriteLine("Введите минимальное значение");
                double max;
                double min;
                try
                {
                    min = double.Parse(Console.ReadLine());
                    Console.WriteLine("Введите максимальное значение");
                    max = double.Parse(Console.ReadLine());
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Ошибка: Необходимо ввести число");
                    continue;
                }

                if (!(min > max) && !NumericComparer.Compare(min, max))
                {
                    var randomGenerator = new RandomMatrixGenerator();
                    return new Matrix(randomGenerator.Generate(k, min, max));    
                }
                Console.WriteLine("Ошибка: Максимальное значение должно быть строго больше минимального");
            } while (true);
        }

        private Matrix FileInput(int k)
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

        private Matrix ConsoleInput(int k)
        {
            var matrixString = new StringBuilder();
            for (var i = 0; i < k; i++)
            {
                Console.WriteLine($"Введите коэффициенты уравнения №{i+1} ");
                matrixString.Append(Console.ReadLine());
                matrixString.Append(' ');
            } 
            return HandleMatrixParse(matrixString.ToString(), k);
            
        }

        private Matrix HandleMatrixParse(string source, int k)
        {
            Matrix result;
            try
            {
                result = Matrix.Parse(source, k);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return result;
        }

    }
}