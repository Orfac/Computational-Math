using System;

namespace Lab1
{
    public class RandomMatrixGenerator
    {
        public double[,] Generate(int size, double min, double max)
        {
            var gen = new Random();           
            var height = size;
            var width = size + 1;
            var randomMatrix = new double[height, width];
            var interval = max - min;
            
            for (var i = 0; i < height; i++)
                for (var j = 0; j < width; j++)
                    randomMatrix[i, j] = gen.NextDouble() * interval + min;
            
            return randomMatrix;
        }
    }
}