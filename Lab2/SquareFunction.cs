namespace Lab2
{
    public class SquareFunction : Function
    {
        public override decimal GetY(decimal x)
        {
            return x * x;
        }

        public override string ToString()
        {
            return "x^2";
        }
    }
}