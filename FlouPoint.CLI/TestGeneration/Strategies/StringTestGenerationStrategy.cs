using FlouPoint.CLI.TestGeneration.Interfaces;
using FlouPoint.CLI.TestGeneration.Strategies.ExpressionGenerator;

namespace FlouPoint.CLI.TestGeneration.Strategies
{
    public class StringTestGenerationStrategy : ITestGenerationStrategy
    {
        public string GenerateTestCode(string className, string propertyName, string expectedValue, string caseTest, string resultExpected)
        {
            return GenerateTestExpression(className, propertyName, expectedValue, caseTest, resultExpected);
        }

        // Método para generar la expresión completa para el test
        private static string GenerateTestExpression(string className, string propertyName, string expectedValue, string caseTest, string resultExpected)
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
                    public void When_{propertyName}_{caseTest}_Then_{resultExpected}()
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
    }
}


