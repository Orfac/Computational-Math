using System;
using System.IO;
using System.Text;

namespace Lab1
{
    class Program
    {
        private const int MaxMatrixHeight = 20;

        public static void Main(string[] args)
        {
            var io = new IO();

            Matrix matrix = null;
            do
            {
                try
                {
                    matrix = io.InputMatrixDialog(MaxMatrixHeight);
                }
                catch (InvalidDataException ex)
                {
                    io.PrintMessage(ex.Message);
                }
               
                
            } while (matrix == null);

            io.PrintMessage("Исходная матрица:");
            io.PrintMessage(matrix.ToString());
            
            var solver = new Solver();
            double[] solutions;    
            try
            {
                solutions = solver.Solve(matrix);
            }
            catch (ArgumentException ex)
            {
                io.PrintMessage(ex.Message);
                return;
            }
            
            var errors = matrix.GetErrors(solutions);
            io.PrintSolvedMatrixInfo(matrix, solutions, errors);
            

            
        }
        
        
    }
}