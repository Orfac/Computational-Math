namespace Lab2.Functions
{
    public class Hyperbola : Function
    {
        public override double GetY(double x)
        {
            return 1 / x;
        }

        public override string ToString()
        {
            return "1/x";
        }
    }
}