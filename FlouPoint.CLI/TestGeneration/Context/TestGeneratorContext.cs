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

        public string GenerateTestCode(string className, string propertyName, string expectedValue, string testCase, string expectedResult)
        {
            return _strategy.GenerateTestCode(className, propertyName, expectedValue, testCase, expectedResult);
        }
    }
}
