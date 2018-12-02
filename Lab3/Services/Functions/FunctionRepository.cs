using System.Collections.Generic;

namespace Lab3.Services.Functions
{
    public class FunctionRepository : IFunctionRepository
    {
        private List<IFunction> _functions;

        public FunctionRepository()
        {
            _functions = new List<IFunction>();
        }

        public IFunction GetFunction(int number)
        {
            
        }
    }
}