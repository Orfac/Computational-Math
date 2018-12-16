using System;

namespace Lab3.Services.Parsers
{
    public class Parser : IParser
    {
        public double[] parseArray(string input)
        {
            // for linux use replacing from , to .
            input = input.Replace('.',',');
            var elements = input.Split(new[] {' ', '\t', '\n', '\r'},
                StringSplitOptions.RemoveEmptyEntries);

            double[] parsedArray = new double[elements.Length];
            for (int i = 0; i < elements.Length; i++)
            {
                parsedArray[i] = double.Parse(elements[i]);
            }

            return parsedArray;
        }
    }
}
