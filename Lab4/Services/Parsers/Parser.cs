using System;
using Lab4.Models;

namespace Lab4.Services.Parsers
{
    public class Parser : IParser
    {

        public DiffirentialEquationInput ParseFromStrings(string sType, string stringx0, string stringy0, string stringxN, string stringAccuracy)
        {
            // for linux change replacing to , to .
            stringAccuracy = stringAccuracy.Replace('.', ',');
            stringx0 = stringx0.Replace('.', ',');
            stringxN = stringxN.Replace('.', ',');
            stringy0 = stringy0.Replace('.', ',');
   
            double accuracy = double.Parse(stringAccuracy);
            int type = int.Parse(sType);
            double x0 = double.Parse(stringx0);
            double xN = double.Parse(stringxN);
            double y0 = double.Parse(stringy0);

            return new DiffirentialEquationInput
            {
                FuncType = type,
                Accuracy = accuracy,
                X0 = x0,
                XN = xN,
                Y0 = y0
            };

        }
    }
}
