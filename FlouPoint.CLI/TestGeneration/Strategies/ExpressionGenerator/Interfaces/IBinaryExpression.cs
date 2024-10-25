using FlouPoint.CLI.TestGeneration.Strategies.ExpressionGenerator.AST;
using System.Linq.Expressions;

namespace FlouPoint.CLI.TestGeneration.Strategies.ExpressionGenerator.Interfaces
{
    public interface IBinaryExpression : IExpressionCompound
    {
        InfixExpression Left { get; }
        IdentifierExpression Operator { get; }
        TypeArguments? TypeArguments { get; }
        InfixExpression Right { get; }
    }
}
