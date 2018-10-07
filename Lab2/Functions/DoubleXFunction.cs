namespace Lab2.Functions
{
    public class DoubleXFunction : Function
    {
        public override double GetY(double x)
        {
            return x + x;
        }

        public override string ToString()
        {
            return "2x";
        }
    }
}