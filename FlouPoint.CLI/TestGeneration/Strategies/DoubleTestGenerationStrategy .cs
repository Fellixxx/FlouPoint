using FlouPoint.CLI.TestGeneration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlouPoint.CLI.TestGeneration.Strategies
{
    public class DoubleTestGenerationStrategy : ITestGenerationStrategy
    {
        public string GenerateTestCode(string className, string propertyName)
        {
            var sb = new StringBuilder();

            // Test for a valid double value
            sb.AppendLine("[Test]");
            sb.AppendLine($"public void When_{propertyName}_IsSetToValidDouble_Then_ShouldReturnSameValue()");
            sb.AppendLine("{");
            sb.AppendLine("    // Given");
            sb.AppendLine($"    var instance = new {className}();");
            sb.AppendLine("    double expectedValue = 3.14;");
            sb.AppendLine();
            sb.AppendLine("    // When");
            sb.AppendLine($"    instance.{propertyName} = expectedValue;");
            sb.AppendLine($"    var actualValue = instance.{propertyName};");
            sb.AppendLine();
            sb.AppendLine("    // Then");
            sb.AppendLine("    actualValue.Should().Be(expectedValue);");
            sb.AppendLine("}");
            sb.AppendLine();

            // Test for a negative double value
            sb.AppendLine("[Test]");
            sb.AppendLine($"public void When_{propertyName}_IsSetToNegativeDouble_Then_ShouldReturnSameValue()");
            sb.AppendLine("{");
            sb.AppendLine("    // Given");
            sb.AppendLine($"    var instance = new {className}();");
            sb.AppendLine("    double expectedValue = -123.45;");
            sb.AppendLine();
            sb.AppendLine("    // When");
            sb.AppendLine($"    instance.{propertyName} = expectedValue;");
            sb.AppendLine($"    var actualValue = instance.{propertyName};");
            sb.AppendLine();
            sb.AppendLine("    // Then");
            sb.AppendLine("    actualValue.Should().Be(expectedValue);");
            sb.AppendLine("}");
            sb.AppendLine();

            // Test for zero
            sb.AppendLine("[Test]");
            sb.AppendLine($"public void When_{propertyName}_IsSetToZero_Then_ShouldReturnZero()");
            sb.AppendLine("{");
            sb.AppendLine("    // Given");
            sb.AppendLine($"    var instance = new {className}();");
            sb.AppendLine("    double expectedValue = 0.0;");
            sb.AppendLine();
            sb.AppendLine("    // When");
            sb.AppendLine($"    instance.{propertyName} = expectedValue;");
            sb.AppendLine($"    var actualValue = instance.{propertyName};");
            sb.AppendLine();
            sb.AppendLine("    // Then");
            sb.AppendLine("    actualValue.Should().Be(expectedValue);");
            sb.AppendLine("}");
            sb.AppendLine();

            return sb.ToString();
        }
    }
}
