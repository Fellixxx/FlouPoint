using FlouPoint.CLI.TestGeneration.Interfaces;
using FlouPoint.CLI.TestGeneration.Strategies.Generation;

namespace FlouPoint.CLI.TestGeneration.Factories
{
    public static class TestGenerationStrategyFactory
    {
        public static ITestGenerationStrategy GetStrategy(string propertyType)
        {
            return propertyType switch
            {
                "string" => new StringTestGenerationStrategy(),
                "int" => new IntTestGenerationStrategy(),
                "double" => new DoubleTestGenerationStrategy(),
                "bool" => new BoolTestGenerationStrategy(),
                "DateTime" => new DateTimeTestGenerationStrategy(),
                "decimal" => new DecimalTestGenerationStrategy(),
                // Add more cases for other types as needed
                _ => new DefaultTestGenerationStrategy()
            };
        }
    }
}
