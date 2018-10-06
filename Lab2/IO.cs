using System;
using System.IO;

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
                    function = TrySelectFunction(functions);
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

        private static Function TrySelectFunction(Function[] functions)
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
                throw new InvalidDataException("Номер функции находится в интервале");
            }
        }
    }
}