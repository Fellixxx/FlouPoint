using FlouPoint.CLI.TestGeneration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlouPoint.CLI.TestGeneration.Strategies.Then
{
    public class IntThenExpression : IThenExpression
    {
        public string GenerateThen(string propertyName, string expectedValue)
        {
            return "    // Then\n" +
                   $"    actualValue.Should().Be({expectedValue});\n";
        }
    }
}
