namespace Lab4.Models.Functions
{
    public interface IFunction
    {
        string Name { get; }
        double getY(double x, double y);
    } 
}