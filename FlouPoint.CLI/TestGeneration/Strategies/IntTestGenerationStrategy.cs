using FlouPoint.CLI.TestGeneration.Interfaces;
using FlouPoint.CLI.TestGeneration.Strategies.Given;
using FlouPoint.CLI.TestGeneration.Strategies.Then;
using FlouPoint.CLI.TestGeneration.Strategies.When;

namespace FlouPoint.CLI.TestGeneration.Strategies
{
    public class IntTestGenerationStrategy : ITestGenerationStrategy
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
