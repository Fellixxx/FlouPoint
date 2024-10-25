using FlouPoint.CLI.TestGeneration.Interfaces;
using FlouPoint.CLI.TestGeneration.Strategies.Generation.ExpressionGenerator;

namespace FlouPoint.CLI.TestGeneration.Strategies
{
    public abstract class BaseTestGenerationStrategy : ITestGenerationStrategy
    {
        public string GenerateTestCode(string className, string propertyName, string expectedValue, string testCase, string expectedResult)
        {
            var createInstance = CreateInstance(className);
            var defineExpectedValue = DefineExpectedValue(expectedValue);
            var assignProperty = AssignProperty(propertyName);
            var retrieveActualValue = RetrieveActualValue(propertyName);
            var assertion = Assertion();
            return $@"
                    [Test]
                    public void When_{propertyName}_{testCase}_Then_{expectedResult}()
                    {{
                        // Given
                        {createInstance}
                        {defineExpectedValue}

                        // When
                        {assignProperty}
                        {retrieveActualValue}

                        // Then
                        {assertion}
                    }}";
        }

        private static string Assertion()
        {
            return TestExpressionGenerator.GenerateAssertion(
                "actualValue",
                "expectedValue"
            );
        }

        private static string RetrieveActualValue(string propertyName)
        {
            return TestExpressionGenerator.GenerateAssignment(
                "actualValue",
                $"instance.{propertyName}"
            );
        }

        private static string AssignProperty(string propertyName)
        {
            return TestExpressionGenerator.GenerateAssignment(
                            $"instance.{propertyName}",
                            "expectedValue"
                        );
        }

        private static string DefineExpectedValue(string expectedValue)
        {
            return TestExpressionGenerator.GenerateAssignment(
                "var expectedValue",
                expectedValue
            );
        }

        private static string CreateInstance(string className)
        {
            return TestExpressionGenerator.GenerateAssignment(
                "var instance",
                $"new {className}()"
            );
        }

        public abstract List<KeyValuePair<string, string>> GetValidValues();

        public abstract List<KeyValuePair<string, string?>> GetInvalidValues();
    }
}
