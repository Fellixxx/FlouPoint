using FlouPoint.CLI.TestGeneration.Interfaces;

namespace FlouPoint.CLI.TestGeneration.Strategies
{
    public class DecimalTestGenerationStrategy : ITestGenerationStrategy
    {
        public string GenerateTestCode(string className, string propertyName, string expectedValue, string testCase, string expectedResult)
        {
            throw new NotImplementedException();
        }

        public List<KeyValuePair<string, string?>> GetInvalidValues()
        {
            throw new NotImplementedException();
        }

        public List<KeyValuePair<string, string>> GetValidValues()
        {
            throw new NotImplementedException();
        }
    }
}
