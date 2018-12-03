using Lab3.Models.Functions;
using Lab3.Models.Methods;

namespace Lab3.Models
{
    public class Interpolater
    {
        private const int CountMult = 100;
        private FunctionRepository _repo;
        private LagrangeMethod _lagrange;
        
        private double offset;
        public Interpolater(double offset)
        {
            _repo = new FunctionRepository();
            _lagrange = new LagrangeMethod();
            FuncInit();
            this.offset = offset;
        }

        public void FuncInit()
        {
            _repo.addFunction(new SquareFunction());
            _repo.addFunction(new SinFunction());
            _repo.addFunction(new EPow());
        }

        public InterpolateResult Interpolate(double[] xData, int funcNumber = 0, double[] yData = null)
        {

            int size1 = xData.Length;
            double[] yData0 = new double[size1];
            if (yData == null) 
            {
                yData = new double[size1];
                for (int i = 0; i < size1; i++)
                {
                    yData[i] = _repo.GetFunction(funcNumber).getY(xData[i]);   
                   
                    if (i%3 == 0){
                        yData[i] += offset;
                    }
                     yData0[i] = yData[i];
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

            double[] realYData = new double[size2];
            for (int i = 0; i < size2; i++)
            {
                realYData[i] = _repo.GetFunction(funcNumber).getY(newXData[i]);
            }
            return new InterpolateResult(newXData,newYData,realYData,_repo.GetFunction(funcNumber).Name,yData0);

        }
    }
}