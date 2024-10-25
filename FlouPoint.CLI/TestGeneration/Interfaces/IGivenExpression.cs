using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlouPoint.CLI.TestGeneration.Interfaces
{
    public interface IGivenExpression
    {
        string GenerateGiven(string className, string propertyName, string propertyType, string expectedValue);
    }
}
