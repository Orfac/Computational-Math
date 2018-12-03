namespace Lab3.Models
{
    public class InterpolateResult
    {
       

        public double[] xData {get;}
        public double[] yData {get;}

        public double[] realYData {get;}

         public InterpolateResult(double[] xData, double[] yData, double[] realYData)
        {
            this.xData = xData;
            this.yData = yData;
            this.realYData = realYData;
        }
    }
}