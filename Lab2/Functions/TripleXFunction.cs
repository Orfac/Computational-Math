namespace Lab2.Functions
{
    public class TripleXFunction : Function
    {
        public override double GetY(double x)
        {
            return 3 * x;
        }

        public override string ToString()
        {
            return "3x";
        }
    }
}