using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlouPoint.CLI.TestGeneration.Interfaces
{
    public interface IThenExpression
    {
        string GenerateThen(string propertyName, string expectedValue);
    }
}
