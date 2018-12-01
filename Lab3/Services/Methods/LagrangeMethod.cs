using System;

namespace Lab3.Services.Methods
{
    public class LagrangeMethod : IInterpolationMethod
    {
        public double getY(double[] yArray, double[] xArray,  double x)
        {
            if (xArray == null || yArray == null)
                throw new ArgumentNullException();
            if (xArray.Length != yArray.Length) 
                throw new ArgumentException("У каждой точки должны быть x и y координаты");
            
            
            int size = xArray.Length;
            double sum = 0; 
            for(int i = 0; i < size; i++){
                double mul = 1; 
                for(int j = 0; j < size; j++){
                    if(i!=j) mul *= (x - xArray[j])/(xArray[i]-xArray[j]);
                }
                sum += yArray[i]*mul;
            }
            return sum;
        }

        public double getY(Func<double,double> y,  double[] xArray, double x)
        {
            if (xArray == null || y == null)
                throw new ArgumentNullException();

            int size = xArray.Length;
            double sum = 0; 
            for(int i = 0; i < size; i++){
                double mul = 1; 
                for(int j = 0; j < size; j++){
                    if(i!=j) mul *= (x - xArray[j])/(xArray[i]-xArray[j]);
                }
                sum += y(xArray[i]) * mul;
            }
            return sum;
        }

    }
}