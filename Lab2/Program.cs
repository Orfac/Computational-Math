using System;
using System.Reflection.Metadata.Ecma335;
using Lab2.Functions;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            var io = new IO();
            var functions = InitializeFunctions();
            
            var function = io.SelectFunction(functions);
            var leftLimit = io.SelectLeftLimit();
            var rightLimit = io.SelectRightLimit();

            var solver = new Solver();
            Console.WriteLine(solver.GetSolution(leftLimit, rightLimit).ToString("0.#######"));
        }

        private static Function[] InitializeFunctions()
        {
            return new Function[]
            {
                new SquareFunction(), 
                new DoubleXFunction(),
                new SqrtFunction(), 
                new TripleXFunction(),
                new DoubleXFunction()
            };
        }
    }
}