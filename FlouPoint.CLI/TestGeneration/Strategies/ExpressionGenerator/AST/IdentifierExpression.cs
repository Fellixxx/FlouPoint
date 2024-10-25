namespace FlouPoint.CLI.TestGeneration.Strategies.ExpressionGenerator.AST
{
    public class IdentifierExpression : InfixExpression
    {
        public string Name { get; set; }

        public IdentifierExpression(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
