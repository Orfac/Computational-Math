namespace Lab3.Models.Functions
{
    public interface IFunction
    {
        string Name { get; }
        double getY(double x);
    } 
}