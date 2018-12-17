using Lab4.Models;

namespace Lab4.Services.Parsers
{
    public interface IParser
    {
        DiffirentialEquationInput ParseFromStrings(string sType, string stringx0, string stringy0, string stringxN, string stringAccuracy);
    } 
}
