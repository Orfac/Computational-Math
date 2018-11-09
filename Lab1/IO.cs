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
            PrintMessage("Треугольный вид");
            PrintMessage(matrix.ToString());
            PrintMessage("Решения и невязки");
            for (var i = 0; i < solutions.Length; i++)
                PrintMessage($"X{i+1}: {solutions[i]:E2} | error: {errors[i]:E2} ");
            Console.Write("Определитель матрицы: ");
            PrintMessage(matrix.GetDeterminant().ToString("E2"));
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
                    PrintMessage(ex.Message);
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
                PrintMessage("-------------------------------------------");
                PrintMessage("Введите размер матрицы k");
                var isNumber = int.TryParse(Console.ReadLine(), out var k);
                if (!isNumber)
                {
                    PrintMessage("Ошибка: Введено не целочисленное значение");
                }
                else
                {
                    if (k > 0 && k <= maxMatrixHeight) return k;
                    PrintMessage($"Ошибка: Должно выполняться условие - 0 <= K <= {maxMatrixHeight}");
                }
            } while (true);
        }

        private void ShowMenu()
        {
            PrintMessage("Нажмите соответствующую клавишу для выбора:");
            PrintMessage("1) Задать из консоли");
            PrintMessage("2) Задать из файла");
            PrintMessage("3) Задать случайно");
        }

        private Matrix RandomInput(int k)
        {
            do
            {
                PrintMessage("Введите минимальное значение");
                double max;
                double min;
                try
                {
                    min = double.Parse(Console.ReadLine().Replace(',','.'));
                    PrintMessage("Введите максимальное значение");
                    max = double.Parse(Console.ReadLine().Replace(',','.'));
                }
                catch (Exception ex)
                {
                    if (ex is FormatException || ex is ArgumentNullException)
                    {
                        PrintMessage("Ошибка: Необходимо ввести число");
                        continue;
                    }

                    if (ex is OverflowException)
                        PrintMessage("Ошибка: Число слишком не входит в размер double");
                    else
                    {
                        throw;
                    }
                    
                    continue;
                }

                if (!(min > max) && !NumericComparer.Compare(min, max))
                {
                    var randomGenerator = new RandomMatrixGenerator();
                    return new Matrix(randomGenerator.Generate(k, min, max));    
                }
                PrintMessage("Ошибка: Максимальное значение должно быть строго больше минимального");
            } while (true);
        }

        private Matrix FileInput(int k)
        {
            PrintMessage("Введите путь к файлу");
            var fileName = Console.ReadLine();
            if (fileName == null || !File.Exists(fileName))
            {
                PrintMessage("Файл не существует");
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
                PrintMessage($"Введите коэффициенты уравнения №{i+1} ");
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
                PrintMessage(ex.Message);
                return null;
            }

            return result;
        }

    }
}