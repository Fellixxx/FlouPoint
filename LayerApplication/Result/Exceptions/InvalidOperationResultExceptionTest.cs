using Application.Result.Exceptions;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerApplication.Result.Exceptions
{
    [TestFixture]
    public class InvalidOperationResultExceptionTests
    {
        [Test]
        public void InvalidOperationResultException_Should_Derive_From_Exception()
        {
            // Act
            var exception = new InvalidOperationResultException("An error occurred");

            // Assert
            exception.Should().BeOfType<InvalidOperationResultException>();
            exception.Should().BeAssignableTo<Exception>();
        }

        [Test]
        public void InvalidOperationResultException_Should_Set_Error_Message()
        {
            // Arrange
            var expectedMessage = "This operation is invalid";

            // Act
            var exception = new InvalidOperationResultException(expectedMessage);

            // Assert
            exception.Message.Should().Be(expectedMessage);
        }

        [Test]
        public void InvalidOperationResultException_Should_Throw_With_Valid_Message()
        {
            // Arrange
            var expectedMessage = "This is an invalid operation";

            // Act & Assert
            Action action = () => throw new InvalidOperationResultException(expectedMessage);

            action.Should().Throw<InvalidOperationResultException>()
                .WithMessage(expectedMessage);
        }
    }
}
