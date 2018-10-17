using System;
using System.IO;
using System.Text;

namespace Lab1
{
    public class IO
    {
        public Matrix InputMatrixDialog(int maxMatrixHeight)
        {
            var k = AskCount(maxMatrixHeight);
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
                    throw new InvalidDataException("Ошибка: выберите число в диапазоне от 1 до 3");
            }
        }     

        private int AskCount(int maxMatrixHeight)
        {
            do
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("Введите размер матрицы k");
                var isNumber = int.TryParse(Console.ReadLine(), out var k);
                if (isNumber && k > 0)
                {
                    if (k > maxMatrixHeight)
                        Console.WriteLine($"Ошибка: K должно быть <= {maxMatrixHeight}");
                    else
                        return k;
                }
                else
                {
                    Console.WriteLine("Ошибка: K неотрицательно");
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
            double min, max;
            var isInputParamsCorrect = false;
            do
            {
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

                if (!(min > max) && !NumericComparer.Compare(min, max))
                {
                    isInputParamsCorrect = true;
                }
                else
                {
                    Console.WriteLine("Максимальное значение должно быть строго больше минимального");
                }
            } while (!isInputParamsCorrect);

            var randomGenerator = new RandomMatrixGenerator();
            return new Matrix(randomGenerator.Generate(k, min, max));    

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
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            return result;
        }
    }
}