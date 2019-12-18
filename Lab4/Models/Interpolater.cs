using Lab4.Models.Functions;
using Lab4.Models.Methods;

namespace Lab4.Models
{
    public class Interpolater
    {
        private const int CountMultiplier = 100;
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

        private void FuncInit()
        {
            _repo.addFunction(new SinFunction());
            _repo.addFunction(new CubeFunction());
            _repo.addFunction(new SquareFunction());
        }

        public InterpolateResult Interpolate(double[] xData, int funcNumber = 0, double[] yData = null)
        {

            var baseSize = xData.Length;
            var yBase = new double[baseSize];
            if (yData == null)
            {
                yData = CalculateYByFunction(xData, funcNumber, baseSize, yBase);
            }
            
            var interpolatedSize = GetInterpolatedSize(baseSize);
            var newXData = CalculateNewXData(xData, interpolatedSize, baseSize);
            var newYData = CalculateYByInterpolation(xData, yData, interpolatedSize, newXData);
            var realYData = CalculateRealY(funcNumber, interpolatedSize, newXData);
            
            return new InterpolateResult(newXData,newYData,realYData,_repo.GetFunction(funcNumber).Name,yBase);

        }

        private double[] CalculateRealY(int funcNumber, int interpolatedSize, double[] newXData)
        {
            var realYData = new double[interpolatedSize];
            for (var i = 0; i < interpolatedSize; i++)
            {
                realYData[i] = _repo.GetFunction(funcNumber).getY(newXData[i],0);
            }

            return realYData;
        }

        private double[] CalculateYByInterpolation(double[] xData, double[] yData, int interpolatedSize, double[] newXData)
        {
            var newYData = new double[interpolatedSize];
            for (var i = 0; i < interpolatedSize; i++)
            {
                newYData[i] = _lagrange.getY(xData, yData, newXData[i]);
            }

            return newYData;
        }

        private double[] CalculateNewXData(double[] xData, int interpolatedSize, int baseSize)
        {
            var newXData = new double[interpolatedSize];

            var diffValues = xData[baseSize - 1] - xData[0];
            var diff = diffValues / (interpolatedSize);
            for (var i = 0; i < interpolatedSize; i++)
            {
                newXData[i] = diff * i + xData[0];
            }

            return newXData;
        }

        private double[] CalculateYByFunction(double[] xData, int funcNumber, int baseSize, double[] yBase)
        {
            var yData = new double[baseSize];
            for (var i = 0; i < baseSize; i++)
            {
                yData[i] = _repo.GetFunction(funcNumber).getY(xData[i],0);

                if (i % 3 == 0)
                {
                    yData[i] += offset;
                }

                yBase[i] = yData[i];
            }

            return yData;
        }

        private int GetInterpolatedSize(int baseSize)
        {
            return baseSize * CountMultiplier;
        }
    }
}