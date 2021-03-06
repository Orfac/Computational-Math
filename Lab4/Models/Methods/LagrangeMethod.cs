using System;

namespace Lab4.Models.Methods
{
    public class LagrangeMethod : IInterpolationMethod
    {
        public double getY(double[] xData, double[] yData,  double x)
        {
            if (xData == null || yData == null)
                throw new ArgumentNullException();
            if (xData.Length != yData.Length) 
                throw new ArgumentException("У каждой точки должны быть x и y координаты");
            
            
            int size = xData.Length;
            double sum = 0; 
            for(int i = 0; i < size; i++)
            {
                double multiplier = 1; 
                for(int j = 0; j < size; j++)
                {
                    if(i!=j) 
                    {
                        multiplier *= (x - xData[j])/(xData[i]-xData[j]);
                    }
                }
                sum += yData[i]*multiplier;
            }
            return sum;
        }

    }
}