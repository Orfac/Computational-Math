namespace Lab3.Services.Functions
{
    public interface IFunction
    {
        string Name { get; }
        double getY(double x);
    } 
}