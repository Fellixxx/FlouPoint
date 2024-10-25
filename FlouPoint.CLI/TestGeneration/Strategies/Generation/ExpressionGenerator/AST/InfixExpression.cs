namespace FlouPoint.CLI.TestGeneration.Strategies.Generation.ExpressionGenerator.AST
{
    public abstract class InfixExpression : ExpressionCompound
    {
        internal virtual OperatorPrecedence Precedence => OperatorPrecedence.Max;
    }
}
