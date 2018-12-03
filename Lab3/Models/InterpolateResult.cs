namespace Lab3.Models
{
    public class InterpolateResult
    {
       

        public double[] xData {get;}
        public double[] yData {get;}

         public InterpolateResult(double[] xData, double[] yData)
        {
            this.xData = xData;
            this.yData = yData;
        }
    }
}