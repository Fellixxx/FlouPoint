using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlouPoint.CLI.TestGeneration.Strategies.ExpressionGenerator.AST
{
    public abstract class InfixExpression : ExpressionCompound
    {
        // Propiedad que define la precedencia del operador
        internal virtual OperatorPrecedence Precedence => OperatorPrecedence.Max;
    }
}
