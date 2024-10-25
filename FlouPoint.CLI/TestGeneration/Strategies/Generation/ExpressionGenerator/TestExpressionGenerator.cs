using FlouPoint.CLI.TestGeneration.Strategies.Generation.ExpressionGenerator.AST;

namespace FlouPoint.CLI.TestGeneration.Strategies.Generation.ExpressionGenerator
{
    public class TestExpressionGenerator
    {
        public static string GenerateAssignment(string left, string right) => $"{left} = {right};";
        public static string GenerateAssertion(string actualValue, string expectedValue) => $"{actualValue}.Should().Be({expectedValue});";
    }
}
