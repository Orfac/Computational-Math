namespace Lab2.Functions
{
    public class FullSquareFunction : Function
    {
        public override double GetY(double x)
        {
            return x * x + 2 * x + 1;
        }

        public override string ToString()
        {
            return "x^x + 2x + 1";
        }
    }
}