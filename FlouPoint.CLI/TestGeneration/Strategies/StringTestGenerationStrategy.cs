using FlouPoint.CLI.TestGeneration.Interfaces;
using FlouPoint.CLI.TestGeneration.Strategies.ExpressionGenerator;

namespace FlouPoint.CLI.TestGeneration.Strategies
{
    public class StringTestGenerationStrategy : ITestGenerationStrategy
    {
        public string GenerateTestCode(string className, string propertyName, string expectedValue, string testCase, string expectedResult)
        {
            return GenerateTestExpression(className, propertyName, expectedValue, testCase, expectedResult);
        }

        // Método para generar la expresión completa para el test
        private static string GenerateTestExpression(string className, string propertyName, string expectedValue, string testCase, string expectedResult)
        {
            // Crear la instancia de la clase
            var createInstance = CreateInstance(className);

            // Definir el valor esperado
            var defineExpectedValue = DefineExpectedValue(expectedValue);

            // Asignar el valor esperado a la propiedad
            var assignProperty = AssignProperty(propertyName);

            // Recuperar el valor actual
            var retrieveActualValue = RetrieveActualValue(propertyName);

            // Aserción de que el valor actual es igual al esperado
            var assertion = Assertion();

            // Combinar todo en el método de prueba
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
                "expectedValue",
                expectedValue
            );
        }

        private static string CreateInstance(string className)
        {
            return TestExpressionGenerator.GenerateAssignment(
                "instance",
                $"new {className}()"
            );
        }

        public List<KeyValuePair<string, string>> GetSuccessValues()
        {
            // Create a list of valid string values with descriptive test case names
            return
            [
                new KeyValuePair<string, string>("TestString", "Test String"),
                new KeyValuePair<string, string>("HelloWorld", "Hello World"),
                new KeyValuePair<string, string>("ValidValue", "ValidValue"),
                new KeyValuePair<string, string>("SampleText", "SampleText"),
                new KeyValuePair<string, string>("AnotherValidString", "AnotherValidString"),
                new KeyValuePair<string, string>("UserInputWithNumbers", "UserInput123"),
                new KeyValuePair<string, string>("SimpleExample", "Example"),
                new KeyValuePair<string, string>("BasicName", "Name"),
                new KeyValuePair<string, string>("AddressField", "Address"),
                new KeyValuePair<string, string>("DescriptionField", "Description")
            ];
        }

        public List<KeyValuePair<string, string?>> GetInvalidValues()
        {
            // Create a list of invalid string values with test case names
            return
            [
                new KeyValuePair<string, string?>("EmptyString", ""),                  // Empty string
                new KeyValuePair<string, string?>("WhitespaceOnly", "   "),            // Whitespace only
                new KeyValuePair<string, string?>("NullValue", null),                  // Null value
                new KeyValuePair<string, string?>("TabCharacter", "\t"),               // Tab character
                new KeyValuePair<string, string?>("NewlineCharacter", "\n"),           // Newline character
                new KeyValuePair<string, string?>("SpecialCharacters", "Invalid@@@###"), // String with special characters
                new KeyValuePair<string, string?>("ExcessivelyLongString", new string('a', 1001)), // Excessively long string (1001 characters)
                new KeyValuePair<string, string?>("LeadingTrailingWhitespace", "   Invalid Text   "), // Leading and trailing whitespaces
                new KeyValuePair<string, string?>("QuotedText", "\"Quoted Text\""),    // String with quotes
                new KeyValuePair<string, string?>("Emojis", "😊😀😁"),                  // String with emojis or non-ASCII characters
            ];
        }
    }
}


