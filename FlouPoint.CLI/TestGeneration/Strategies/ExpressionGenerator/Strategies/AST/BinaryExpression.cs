using FlouPoint.CLI.TestGeneration.Strategies.ExpressionGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FlouPoint.CLI.TestGeneration.Strategies.ExpressionGenerator.Strategies.AST
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

        InfixExpression IBinaryExpression.Left => this.Left;
        IdentifierExpression IBinaryExpression.Operator => this.Operator;
        TypeArguments? IBinaryExpression.TypeArguments => this.TypeArguments;
        InfixExpression IBinaryExpression.Right => this.Right;
    }
}
