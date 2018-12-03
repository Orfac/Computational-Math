using Lab3.Models.Functions;
using Lab3.Models.Methods;

namespace Lab3.Models
{
    public class Interpolater
    {
        private const int CountMult = 100;
        private FunctionRepository _repo;
        private LagrangeMethod _lagrange;
        public Interpolater()
        {
            _repo = new FunctionRepository();
            _lagrange = new LagrangeMethod();
        }

        public void FuncInit()
        {
            _repo.addFunction(new SquareFunction());
            _repo.addFunction(new SinFunction());
            _repo.addFunction(new EPow());
        }

        public double[] Interpolate(int funcNumber, double[] xData, double[] yData = null)
        {
            int size1 = xData.Length;
            if (yData == null) 
            {
                yData = new double[size1];
                for (int i = 0; i < size1; i++)
                {
                    yData[i] = _repo.GetFunction(funcNumber).getY(xData[i]);       
                }
            }
            int size2 = size1 * CountMult;
            double[] newXData = new double[size2];

            var diffValues = xData[size1 - 1] - xData[0];
            var diff = diffValues / (size2);
            for (int i = 0; i < size2; i++)
            {
                newXData[i] = diff*i + xData[0];
            }
            
            double[] newYData = new double[size2];
            for (int i = 0; i < size2; i++)
            {
                newYData[i] = _lagrange.getY(xData,yData,newXData[i]);
            }

        }
    }
}