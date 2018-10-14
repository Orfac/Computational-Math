using System;

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
            var accurasy = io.SelectAccuracy();

            var solver = new Solver();
            var solution = solver.GetSolution(leftLimit, rightLimit,function,accurasy);
            Console.WriteLine(solution);
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