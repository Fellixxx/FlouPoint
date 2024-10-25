using FlouPoint.CLI.TestGeneration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlouPoint.CLI.TestGeneration.Strategies
{
    public class DecimalTestGenerationStrategy : ITestGenerationStrategy
    {
        public string GenerateTestCode(string className, string propertyName, string expectedValue, string testCase, string expectedResult)
        {
            return $@"
[Test]
public void When_{propertyName}_IsSetToValidDecimal_Then_ShouldReturnSameValue()
{{
    // Given
    var instance = new {className}();
    decimal expectedValue = 99.99M;

    // When
    instance.{propertyName} = expectedValue;
    var actualValue = instance.{propertyName};

    // Then
    actualValue.Should().Be(expectedValue);
}}

[Test]
public void When_{propertyName}_IsSetToNegativeDecimal_Then_ShouldReturnSameValue()
{{
    // Given
    var instance = new {className}();
    decimal expectedValue = -99.99M;

    // When
    instance.{propertyName} = expectedValue;
    var actualValue = instance.{propertyName};

    // Then
    actualValue.Should().Be(expectedValue);
}}

[Test]
public void When_{propertyName}_IsSetToZeroDecimal_Then_ShouldReturnZero()
{{
    // Given
    var instance = new {className}();
    decimal expectedValue = 0.00M;

    // When
    instance.{propertyName} = expectedValue;
    var actualValue = instance.{propertyName};

    // Then
    actualValue.Should().Be(expectedValue);
}}
";
        }

        public List<KeyValuePair<string, string?>> GetInvalidValues()
        {
            throw new NotImplementedException();
        }

        public List<KeyValuePair<string, string>> GetSuccessValues()
        {
            throw new NotImplementedException();
        }
    }
}
