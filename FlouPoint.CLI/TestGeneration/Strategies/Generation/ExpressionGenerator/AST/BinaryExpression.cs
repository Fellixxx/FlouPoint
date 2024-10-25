using FlouPoint.CLI.TestGeneration.Strategies.Generation.ExpressionGenerator.Interfaces;

namespace FlouPoint.CLI.TestGeneration.Strategies.Generation.ExpressionGenerator.AST
{
    public class BinaryExpression : InfixExpression, IBinaryExpression
    {
        public InfixExpression Left { get; }
        public IdentifierExpression Operator { get; }
        public TypeArguments? TypeArguments { get; }
        public InfixExpression Right { get; }

        public BinaryExpression(InfixExpression left, IdentifierExpression operatorExpr, TypeArguments? typeArgs, InfixExpression right)
        {
            Left = left;
            Operator = operatorExpr;
            TypeArguments = typeArgs;
            Right = right;
        }


        public override string ToString()
        {
            return $"{Left} {Operator} {Right}";
        }
    }
}
