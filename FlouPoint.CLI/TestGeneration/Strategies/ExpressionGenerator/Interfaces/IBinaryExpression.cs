using FlouPoint.CLI.TestGeneration.Strategies.ExpressionGenerator.AST;

namespace FlouPoint.CLI.TestGeneration.Strategies.ExpressionGenerator.Interfaces
{
    public interface IBinaryExpression
    {
        InfixExpression Left { get; }
        IdentifierExpression Operator { get; }
        TypeArguments? TypeArguments { get; }
        InfixExpression Right { get; }
    }
}
