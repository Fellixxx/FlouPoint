using FlouPoint.CLI.TestGeneration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlouPoint.CLI.TestGeneration.Strategies.When
{
    public class IntWhenExpression : IWhenExpression
    {
        public string GenerateWhen(string className, string propertyName)
        {
            return "    // When\n" +
                   $"    instance.{propertyName} = expectedValue;\n" +
                   $"    var actualValue = instance.{propertyName};\n";
        }
    }
}
