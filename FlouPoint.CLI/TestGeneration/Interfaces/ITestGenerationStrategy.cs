using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlouPoint.CLI.TestGeneration.Interfaces
{
    public interface ITestGenerationStrategy
    {
        string GenerateTestCode(string className, string propertyName, string expectedValue, string testCase, string expectedResult);
        List<KeyValuePair<string, string>> GetValidValues();
        List<KeyValuePair<string, string?>> GetInvalidValues();
    }
}
