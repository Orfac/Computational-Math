using Lab4.Models.Functions;
using Lab4.Models.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4.Models
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public static class DiffirentialEquationSolver
    {
        public static List<Point> lastValues;
        public static double X0 { get; set; }
        public static double Y0 { get; set; }
        public static double XN { get; set; }
        public static double Accuracy { get; set; }
        public static int FuncType { get; set; }

        private const int CountMultiplier = 100;
        private static FunctionRepository _repo;
        private static LagrangeMethod _lagrange;
        private static NewtonMethod _newton;

        
        private static void FuncInit()
        {
            _repo.addFunction(new SinFunction());
            _repo.addFunction(new CubeFunction());
            _repo.addFunction(new SquareFunction());
        }

        public static void Update(DiffirentialEquationInput input)
        {
            _lagrange = new LagrangeMethod();
            _newton = new NewtonMethod();
            _repo = new FunctionRepository();
            FuncInit();

            X0 = input.X0;
            XN = input.XN;
            Y0 = input.Y0;
            Accuracy = input.Accuracy;
            FuncType = input.FuncType;

        }

        public static DiffirentialResult Solve()
        {
            Func<double, double, double> func = _repo.GetFunction(FuncType).getY;
            var isCalculated = false;
            List<double> differentialList;
            List<Point> differentialValues = new List<Point>();
            var pointsCount = 5;
            var step = (XN - X0) / pointsCount;

            while (!isCalculated)
            {
                var isReduceStep = false;
                isCalculated = true;

                differentialValues = RungeKutta(X0, Y0, step, func, out differentialList);

                for (int i = 4; i < pointsCount + 1; i++)
                {
                    double newX = differentialValues[i - 1].X + step;
                    double yPred = (differentialValues[i - 4].Y + (4 * step / 3) *
                                    (2 * differentialList[i - 3] - differentialList[i - 2] + 2 * differentialList[i - 1]));
                    differentialList.Add(func(newX, yPred));
                    double yCor = (differentialValues[i - 2].Y +
                                   step / 3 * (differentialList[i - 2] + 4 * differentialList[i - 1] +
                                               differentialList[i]));

                    if (Math.Abs(yCor - yPred) / 29 > Accuracy)
                    {
                        step /= 2;
                        pointsCount *= 2;
                        isReduceStep = true;
                        break;
                    }
                    else
                    {
                        differentialValues.Add(new Point { X = newX, Y = yCor });
                        differentialList[i] = func(newX, yCor);
                    }
                }

                if (isReduceStep) isCalculated = false;
            }

            lastValues = differentialValues;
            double[] resx = new double[differentialValues.Count];
            double[] resy = new double[differentialValues.Count];
            for (int i = 0; i < differentialValues.Count; i++)
            {
                resx[i] = differentialValues[i].X;
                resy[i] = differentialValues[i].Y;
            }
            return new DiffirentialResult
            {
                xData = resx,
                yData = resy
            };
        }

        public static List<Point> RungeKutta(double x0, double y0, double step,
            Func<double, double, double> function, out List<double> differentialList)
        {
            differentialList = new List<double>();

            var kuttaСoef = new double[4];
            var startDataPoints = new List<Point>() { new Point { X = x0, Y = y0 } };

            for (int i = 0; i < 3; i++)
            {
                kuttaСoef[0] = function(startDataPoints[i].X, startDataPoints[i].Y);
                kuttaСoef[1] = function(startDataPoints[i].X + step / 2, startDataPoints[i].Y + kuttaСoef[0] / 2);
                kuttaСoef[2] = function(startDataPoints[i].X + step / 2, startDataPoints[i].Y + kuttaСoef[1] / 2);
                kuttaСoef[3] = function(startDataPoints[i].X + step, startDataPoints[i].Y + kuttaСoef[2]);
                var newDataPoint = new Point
                {
                    X = startDataPoints[i].X + step,
                    Y = (startDataPoints[i].Y +
                     step * (kuttaСoef[0] + 2 * kuttaСoef[1] + 2 * kuttaСoef[2] + kuttaСoef[3]) / 6.0)
                };

                startDataPoints.Add(newDataPoint);

                differentialList.Add(function(startDataPoints[i].X, startDataPoints[i].Y));
            }

            differentialList.Add(function(startDataPoints[3].X, startDataPoints[3].Y));

            return startDataPoints;
        }

    }
}

