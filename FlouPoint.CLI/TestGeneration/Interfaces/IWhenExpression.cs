using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlouPoint.CLI.TestGeneration.Interfaces
{
    public interface IWhenExpression
    {
        string GenerateWhen(string className, string propertyName);
    }
}
