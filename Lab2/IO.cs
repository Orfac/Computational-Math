using System;
using System.IO;
using System.Runtime.InteropServices;
using Lab2.Functions;

namespace Lab2
{
    public class IO
    {
        public Function SelectFunction(Function[] functions)
        {
            Function function = null;
            var isSelected = false;
            do
            {
                try
                {
                    function = SelectFunctionDialog(functions);
                    isSelected = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Необходимо ввести число");
                }
                catch (InvalidDataException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            } while (!isSelected);

            return function;
        }

        internal void PrintException(string message)
        {
            Console.WriteLine(message);
        }

        public void PrintSolution(Solution solution)
        {
            Console.WriteLine($"Значение интеграла: {solution.Integral}");
            Console.WriteLine($"Кол-во разбиений: {solution.Count}");
            Console.WriteLine($"Погрешность: {solution.Error}");
        }

        public double SelectAccuracy() => 
            SelectParameterDialog("Введите точность измерения");

       public double SelectLeftLimit() => 
            SelectParameterDialog("Введите левый предел интегрирования");
        
        public double SelectRightLimit() => 
            SelectParameterDialog("Введите правый предел интегрирования");
        
        private double SelectParameterDialog(string message)
        {
            do
            {
                Console.WriteLine(message);
                try
                {
                    var input = Console.ReadLine();
                    return double.Parse(input);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка: Необходимо ввести вещественное число");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Ошибка: число не помещается в размер типа double");
                }
                
            } while (true);
        }
        
        private static Function SelectFunctionDialog(Function[] functions)
        {
            Console.WriteLine("Выберите функцию:");
            for (var i = 0; i < functions.Length; i++)
            {
                Console.WriteLine($"{i + 1}) F(x) = {functions[i]}");
            }

            var input = Console.ReadLine();
            var funcNumber = int.Parse(input);
            if (0 >= funcNumber || funcNumber > functions.Length)
            {
                throw new InvalidDataException
                    ($"Ошибка: Номер функции находится в интервале [1, {functions.Length}]");
            }
               
            return functions[funcNumber - 1];

        }
    }
}