namespace FlouPoint.CLI.TestGeneration.Interfaces
{
    public interface ITestGenerationStrategy
    {
        string GenerateTestCode(string className, string propertyName, string expectedValue, string testCase, string expectedResult);
        List<KeyValuePair<string, string>> GetValidValues();
        List<KeyValuePair<string, string?>> GetInvalidValues();
    }
}
