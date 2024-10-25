using FlouPoint.CLI.TestGeneration.Interfaces;

namespace FlouPoint.CLI.TestGeneration.Strategies
{
    public abstract class BaseTestGenerationStrategy : ITestGenerationStrategy
    {
        public virtual string GenerateTestCode(string className, string propertyName, string expectedValue, string testCase, string expectedResult)
        {
            throw new NotImplementedException();
        }

        public virtual List<KeyValuePair<string, string?>> GetInvalidValues()
        {
            throw new NotImplementedException();
        }

        public virtual List<KeyValuePair<string, string>> GetValidValues()
        {
            throw new NotImplementedException();
        }
    }
}
