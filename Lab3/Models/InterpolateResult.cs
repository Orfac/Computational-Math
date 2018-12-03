namespace Lab3.Models
{
    public class InterpolateResult
    {
       

        public double[] xData {get;}
        public double[] yData {get;}

        public double[] realYData {get;}

        public string  funcName {get;}

        public double[] yData0 {get;}
         public InterpolateResult(double[] xData, double[] yData, double[] realYData, string name, double[] yData0)
        {
            this.xData = xData;
            this.yData = yData;
            this.realYData = realYData;
            this.funcName = name;
            this.yData0 = yData0;
        }
    }
}