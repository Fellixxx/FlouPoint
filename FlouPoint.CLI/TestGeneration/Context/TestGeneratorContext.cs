using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlouPoint.CLI.TestGeneration.Interfaces;

namespace FlouPoint.CLI.TestGeneration.Context
{
    public class TestGeneratorContext
    {
        private readonly ITestGenerationStrategy _strategy;

        public TestGeneratorContext(ITestGenerationStrategy strategy)
        {
            _strategy = strategy;
        }

        public string GenerateTestCode(string className, string propertyName, string expectedValue, string caseTest, string resultExpected)
        {
            return _strategy.GenerateTestCode(className, propertyName, expectedValue, caseTest, resultExpected);
        }
    }
}
