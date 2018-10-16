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
                    Console.WriteLine(ex.Message);
                }
               
                
            } while (matrix == null);


            Console.WriteLine(matrix);
            var solver = new Solver();
            var solutions = solver.Solve(matrix);
            if (solutions == null)
            {
                Console.WriteLine("Матрицу не удалось решить методом Гаусса");
            }
            else
            {
                Console.WriteLine("Треугольный вид");
                Console.WriteLine(matrix);
                var errors = matrix.GetErrors(solutions);
                Console.WriteLine("Решения и невязки");
                for (var i = 0; i < solutions.Length; i++)
                    Console.WriteLine($"X{i+1}: {solutions[i]} | error: {errors[i]} ");
            }

            Console.Write("Определитель матрицы: ");
            Console.WriteLine(matrix.GetDeterminant().ToString("0.#######"));
        }
        
        
    }
}