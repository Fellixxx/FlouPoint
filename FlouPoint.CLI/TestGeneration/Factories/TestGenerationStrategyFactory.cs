using FlouPoint.CLI.TestGeneration.Interfaces;
using FlouPoint.CLI.TestGeneration.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
