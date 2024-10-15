using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerDomain.EnumType.OperationExecute
{
    using global::Domain.EnumType.OperationExecute;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class OperationExecuteTests
    {
        [Test]
        public Task When_OperationExecute_Add_Should_Match()
        {
            // Given
            var expectedValue = 0;

            // When
            var actualValue = (int)OperationExecute.Add;

            // Then
            expectedValue.Should().Be(actualValue);
            return Task.CompletedTask;
        }

        [Test]
        public Task When_OperationExecute_Modified_Should_Match()
        {
            // Given
            var expectedValue = 1;

            // When
            var actualValue = (int)OperationExecute.Modified;

            // Then
            expectedValue.Should().Be(actualValue);
            return Task.CompletedTask;
        }

        [Test]
        public Task When_OperationExecute_Validate_AllValues()
        {
            // Given
            var enumValues = Enum.GetValues(typeof(OperationExecute)).Cast<int>();

            // When
            var actualCount = enumValues.Count();

            // Then
            actualCount.Should().BeGreaterThan(0);
            return Task.CompletedTask;
        }

        [Test]
        public Task When_OperationExecute_ParseFromString_Should_Match()
        {
            // Given
            var stringValue = "Add";

            // When
            var parsedValue = Enum.Parse<OperationExecute>(stringValue);

            // Then
            parsedValue.Should().Be(OperationExecute.Add);
            return Task.CompletedTask;
        }
    }
}
