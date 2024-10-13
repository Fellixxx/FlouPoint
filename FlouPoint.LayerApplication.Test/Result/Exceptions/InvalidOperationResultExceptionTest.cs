namespace FlouPoint.LayerApplication.Test.Result.Exceptions
{
    using Application.Result.Exceptions;
    using FluentAssertions;
    using global::Application.Result.Exceptions;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class InvalidOperationResultExceptionTests
    {
        [Test]
        public void InvalidOperationResultException_Should_Contain_Provided_Message()
        {
            // Arrange
            var expectedMessage = "This is a custom error message for an invalid operation result.";

            // Act
            var exception = new InvalidOperationResultException(expectedMessage);

            // Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(expectedMessage);
        }


        [Test]
        public void InvalidOperationResultException_Should_Work_With_Throw_And_Catch()
        {
            // Arrange
            var expectedMessage = "This operation is invalid.";

            // Act & Assert
            Action action = () => throw new InvalidOperationResultException(expectedMessage);

            action.Should().Throw<InvalidOperationResultException>()
                .WithMessage(expectedMessage);
        }

        [Test]
        public void InvalidOperationResultException_Should_Inherit_From_Exception_Class()
        {
            // Arrange & Act
            var exception = new InvalidOperationResultException("Test inheritance");

            // Assert
            exception.Should().BeAssignableTo<Exception>();
        }
    }
}

