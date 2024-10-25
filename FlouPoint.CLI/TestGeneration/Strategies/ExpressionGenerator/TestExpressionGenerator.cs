
using FlouPoint.CLI.TestGeneration.Strategies.ExpressionGenerator.AST;

namespace FlouPoint.CLI.TestGeneration.Strategies.ExpressionGenerator
{
    public class TestExpressionGenerator
    {
        public static string GenerateAssignment(string left, string right)
        {
            var leftExpr = new IdentifierExpression(left);
            var rightExpr = new IdentifierExpression(right);
            var binaryExpr = new BinaryExpression(
                leftExpr,
                new IdentifierExpression("="),
                null,
                rightExpr
            );

            return binaryExpr.ToString();
        }

        public static string GenerateAssertion(string actualValue, string expectedValue)
        {
            var actualExpr = new IdentifierExpression(actualValue);
            var shouldExpr = new IdentifierExpression("Should().Be");
            var expectedExpr = new IdentifierExpression(expectedValue);
            var assertionExpr = new BinaryExpression(
                actualExpr,
                shouldExpr,
                null,
                expectedExpr
            );

            return $"{actualExpr}.Should().Be({expectedExpr});";
        }
    }
}
