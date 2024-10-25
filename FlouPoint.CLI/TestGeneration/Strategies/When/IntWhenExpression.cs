using FlouPoint.CLI.TestGeneration.Interfaces;

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
