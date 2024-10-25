using FlouPoint.CLI.TestGeneration.Strategies.ExpressionGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FlouPoint.CLI.TestGeneration.Strategies.ExpressionGenerator.AST
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

        InfixExpression IBinaryExpression.Left => Left;
        IdentifierExpression IBinaryExpression.Operator => Operator;
        TypeArguments? IBinaryExpression.TypeArguments => TypeArguments;
        InfixExpression IBinaryExpression.Right => Right;

        public override string ToString()
        {
            var left = Left != null ? Left.ToString() : "null";
            var right = Right != null ? Right.ToString() : "null";
            var operatorExpr = Operator != null ? Operator.ToString() : "=";
            return $"var {left} {operatorExpr} {right};";
        }
    }
}
