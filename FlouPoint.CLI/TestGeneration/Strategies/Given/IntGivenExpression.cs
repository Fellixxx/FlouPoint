using FlouPoint.CLI.TestGeneration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlouPoint.CLI.TestGeneration.Strategies.Given
{
    public class IntGivenExpression : IGivenExpression
    {
        public string GenerateGiven(string className, string propertyName, string propertyType, string expectedValue)
        {
            return "    // Given\n" +
                   $"    var instance = new {className}();\n" +
                   $"    {propertyType} expectedValue = {expectedValue};\n";
        }
    }
}
