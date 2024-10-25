using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FlouPoint.CLI.TestGeneration.Strategies.ExpressionGenerator.Strategies.AST
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
