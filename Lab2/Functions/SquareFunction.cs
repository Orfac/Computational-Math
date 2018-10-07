namespace Lab2.Functions
{
    public class SquareFunction : Function
    {
        public override double GetY(double x)
        {
            return x * x;
        }

        public override string ToString()
        {
            return "x^2";
        }
    }
}