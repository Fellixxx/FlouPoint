using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlouPoint.CLI.TestGeneration.Interfaces
{
    public interface ITestGenerationStrategy
    {
        string GenerateTestCode(string className, string propertyName);
    }
}
