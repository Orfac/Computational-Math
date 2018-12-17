using System.Text;

namespace Lab3.Models
{
    public class InterpolateResult
    {


        public double[] XData { get; }
        public double[] YData { get; }

        public double[] RealYData { get; }

        public string FuncName { get; }

        public double[] YData0 { get; }
        public InterpolateResult(double[] xData, double[] yData, double[] realYData, string name, double[] yData0)
        {
            this.XData = xData;
            this.YData = yData;
            this.RealYData = realYData;
            this.FuncName = name;
            this.YData0 = yData0;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(this.FuncName);
            sb.Append(' ');
            for (int i = 0; i < this.YData0.Length; i++)
            {
                sb.Append(this.YData0[i]);
                sb.Append(' ');
            }
            for (int i = 0; i < this.XData.Length; i++)
            {
                sb.Append(this.XData[i]);
                sb.Append(' ');
                sb.Append(this.YData[i]);
                sb.Append(' ');
                sb.Append(this.RealYData[i]);
                sb.Append(' ');
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}