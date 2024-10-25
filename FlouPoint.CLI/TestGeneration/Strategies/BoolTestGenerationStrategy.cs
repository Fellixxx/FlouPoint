using FlouPoint.CLI.TestGeneration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlouPoint.CLI.TestGeneration.Strategies
{
    public class BoolTestGenerationStrategy : ITestGenerationStrategy
    {
        public string GenerateTestCode(string className, string propertyName)
        {
            return $@"
[Test]
public void When_{propertyName}_IsSetToTrue_Then_ShouldReturnTrue()
{{
    // Given
    var instance = new {className}();
    bool expectedValue = true;

    // When
    instance.{propertyName} = expectedValue;
    var actualValue = instance.{propertyName};

    // Then
    actualValue.Should().BeTrue();
}}

[Test]
public void When_{propertyName}_IsSetToFalse_Then_ShouldReturnFalse()
{{
    // Given
    var instance = new {className}();
    bool expectedValue = false;

    // When
    instance.{propertyName} = expectedValue;
    var actualValue = instance.{propertyName};

    // Then
    actualValue.Should().BeFalse();
}}
";
        }
    }

}
