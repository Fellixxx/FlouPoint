using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlouPoint.CLI.TestGeneration.Strategies.ExpressionGenerator.AST
{
    public class TypeArguments
    {
        public List<string> ArgumentTypes { get; }

        public TypeArguments(List<string> argumentTypes)
        {
            ArgumentTypes = argumentTypes;
        }
    }
}
