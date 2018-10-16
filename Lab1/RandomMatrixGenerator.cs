using System;

namespace Lab1
{
    public class RandomMatrixGenerator
    {
        public double[,] Generate(int size, double min, double max)
        {
            var gen = new Random();           
            var height = size;
            var length = size + 1;
            var randomMatrix = new double[height, length];
            
            for (var i = 0; i < height; i++)
                for (var j = 0; j < length; j++)
                    randomMatrix[i, j] = gen.NextDouble() * (max - min) + min;
            return randomMatrix;
        }
    }
}