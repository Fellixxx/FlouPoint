using FlouPoint.CLI.TestGeneration.Interfaces;
using FlouPoint.CLI.TestGeneration.Strategies.ExpressionGenerator.Strategies;

namespace FlouPoint.CLI.TestGeneration.Strategies
{
    public class StringTestGenerationStrategy : ITestGenerationStrategy
    {
        public string GenerateTestCode(string className, string propertyName)
        {
            return GenerateTestExpression(className, propertyName);
        }

        // Método para generar la expresión completa para el test
        private static string GenerateTestExpression(string className, string propertyName)
        {
            var testExpressionGenerator = new TestExpressionGenerator();
            // Crear la instancia de la clase
            var createInstance = TestExpressionGenerator.GenerateAssignment(
                "instance",
                $"new {className}()"
            );

            // Definir el valor esperado
            var defineExpectedValue = TestExpressionGenerator.GenerateAssignment(
                "expectedValue",
                "\"Test String\""
            );

            // Asignar el valor esperado a la propiedad
            var assignProperty = TestExpressionGenerator.GenerateAssignment(
                $"instance.{propertyName}",
                "expectedValue"
            );

            // Recuperar el valor actual
            var retrieveActualValue = TestExpressionGenerator.GenerateAssignment(
                "actualValue",
                $"instance.{propertyName}"
            );

            // Aserción de que el valor actual es igual al esperado
            var assertion = TestExpressionGenerator.GenerateAssertion(
                "actualValue",
                "expectedValue"
            );

            // Combinar todo en el método de prueba
            return $@"
                    [Test]
                    public void When_{propertyName}_IsValidString_Then_Success()
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
    }
}


