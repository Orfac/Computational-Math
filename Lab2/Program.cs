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
            var solver = new Solver();
            
            var function = io.SelectFunction(functions);
            var leftLimit = io.SelectLeftLimit();
            var rightLimit = io.SelectRightLimit();

            var accuracy = io.SelectAccuracy();

           
            Solution solution;
            try
            {
                solution = solver.GetSolution(function, leftLimit, rightLimit,accuracy);
            } 
            catch(OverflowException ex) 
            {
                io.PrintException(ex.Message);
                return;
            }
            catch(NullReferenceException ex)
            {
                io.PrintException(ex.Message);
                return;
            }
            
            io.PrintSolution(solution);
            
        }

        private static Function[] InitializeFunctions()
        {
            return new Function[]
            {
                new SquareFunction(), 
                new DoubleXFunction(),
                new SqrtFunction(), 
                new Hyperbola(),
                new FullSquareFunction()
            };
        }
        
    }
}