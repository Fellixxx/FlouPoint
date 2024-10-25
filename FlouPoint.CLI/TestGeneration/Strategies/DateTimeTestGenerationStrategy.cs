using FlouPoint.CLI.TestGeneration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlouPoint.CLI.TestGeneration.Strategies
{
    public class DateTimeTestGenerationStrategy : ITestGenerationStrategy
    {
        public string GenerateTestCode(string className, string propertyName, string expectedValue, string caseTest, string resultExpected)
        {
            return $@"
[Test]
public void When_{propertyName}_IsSetToValidDateTime_Then_ShouldReturnSameValue()
{{
    // Given
    var instance = new {className}();
    DateTime expectedValue = new DateTime(2023, 1, 1);

    // When
    instance.{propertyName} = expectedValue;
    var actualValue = instance.{propertyName};

    // Then
    actualValue.Should().Be(expectedValue);
}}

[Test]
public void When_{propertyName}_IsSetToDefaultDateTime_Then_ShouldReturnSameValue()
{{
    // Given
    var instance = new {className}();
    DateTime expectedValue = default(DateTime);

    // When
    instance.{propertyName} = expectedValue;
    var actualValue = instance.{propertyName};

    // Then
    actualValue.Should().Be(expectedValue);
}}
";
        }
    }

}
