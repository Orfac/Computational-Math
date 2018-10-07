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
                catch (InvalidDataException e)
                {
                    Console.WriteLine(e);
                }
                
            } while (!isSelected);

            return function;
        }

        public double SelectLeftLimit()
        {
            do
            {
                try
                {
                    return SelectLimitDialog("Введите левый предел интегрирования");                 
                }
                catch (FormatException)
                {
                    Console.WriteLine("Необходимо ввести вещественное число");
                }
                
            } while (true);
            
        }
        
        public double SelectRightLimit()
        {
            return SelectLimitDialog("Введите правый предел интегрирования");
        }
        
        private double SelectLimitDialog(string message)
        {
            do
            {
                Console.WriteLine(message);
                try
                {
                    return SelectLimitDialog(message);
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

            var funcNumber = Int32.Parse(Console.ReadLine());
            if (0 < funcNumber && funcNumber <= functions.Length)
            {
                return functions[funcNumber - 1];
            }
            else
            {
                throw new InvalidDataException
                    ($"Ошибка: Номер функции находится в интервале [1, {functions.Length}]");
            }
        }
    }
}