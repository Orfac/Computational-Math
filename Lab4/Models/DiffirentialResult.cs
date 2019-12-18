using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Models
{
    public class DiffirentialResult
    {
        public double[] xData { get; set; }
        public double[] yData { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < this.xData.Length; i++)
            {
                sb.Append(this.xData[i]);
                sb.Append(' ');
                sb.Append(this.yData[i]);
                sb.Append(' ');
            }
            return sb.ToString();
        }
    }
}
