using System;
using System.Reflection.Metadata.Ecma335;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            var io = new IO();
            var functions = InitializeFunctions();
            var function = io.SelectFunction(functions);
            var solver = new Solver(function);
            Console.WriteLine(solver.GetSolution(5, 10, (decimal) 0.1).ToString("0.#######"));
        }

        private static Function[] InitializeFunctions()
        {
            return new Function[]
            {
                new SquareFunction(), 
                new SquareFunction(),
                new SquareFunction(),
                new SquareFunction()
            };
        }
    }
}