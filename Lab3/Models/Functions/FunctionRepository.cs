using System.Collections.Generic;

namespace Lab3.Models.Functions
{
    public class FunctionRepository : IFunctionRepository
    {
        private List<IFunction> _functions;

        public FunctionRepository()
        {
            _functions = new List<IFunction>();
        }

        public void addFunction(IFunction function)
        {
            _functions.Add(function);
        }

        public IFunction GetFunction(int number)
        {
            return _functions[number];
        }
    }
}