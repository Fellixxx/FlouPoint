namespace FlouPoint.LayerApplication.Test.Result
{
    using Application.Result;
    using Application.Result.Error;
    using FluentAssertions;
    using Application.Result.Error;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class OperationBuilderTests
    {
        [Test]
        public void FailureBusinessValidation_Should_Return_Correct_OperationResult()
        {
            // Arrange
            string expectedMessage = "Business validation failed.";
            var expectedErrorType = ErrorTypes.BusinessValidationError;

            // Act
            var result = OperationBuilder<string>.FailureBusinessValidation(expectedMessage);

            // Assert
            result.Should().NotBeNull();
            result.Message.Should().Be(expectedMessage);
        }



        [Test]
        public void FailureConfigurationMissingError_Should_Return_Correct_OperationResult()
        {
            // Arrange
            string expectedMessage = "Configuration is missing.";
            var expectedErrorType = ErrorTypes.ConfigurationMissingError;

            // Act
            var result = OperationBuilder<string>.FailureConfigurationMissingError(expectedMessage);

            // Assert
            result.Should().NotBeNull();
            result.Message.Should().Be(expectedMessage);
        }

        [Test]
        public void FailureDatabase_Should_Return_Correct_OperationResult()
        {
            // Arrange
            string expectedMessage = "Database error occurred.";
            var expectedErrorType = ErrorTypes.DatabaseError;

            // Act
            var result = OperationBuilder<string>.FailureDatabase(expectedMessage);

            // Assert
            result.Should().NotBeNull();
            result.Message.Should().Be(expectedMessage);
        }

        [Test]
        public void FailureDataSubmittedInvalid_Should_Return_Correct_OperationResult()
        {
            // Arrange
            string expectedMessage = "Data submitted is invalid.";
            var expectedErrorType = ErrorTypes.DataSubmittedInvalid;

            // Act
            var result = OperationBuilder<string>.FailureDataSubmittedInvalid(expectedMessage);

            // Assert
            result.Should().NotBeNull();
            result.Message.Should().Be(expectedMessage);
        }

        [Test]
        public void FailureExtenalService_Should_Return_Correct_OperationResult()
        {
            // Arrange
            string expectedMessage = "External service error.";
            var expectedErrorType = ErrorTypes.ExternalServicesError;

            // Act
            var result = OperationBuilder<string>.FailureExtenalService(expectedMessage);

            // Assert
            result.Should().NotBeNull();
            result.Message.Should().Be(expectedMessage);
        }

        [Test]
        public void FailureUnexpectedError_Should_Return_Correct_OperationResult()
        {
            // Arrange
            string expectedMessage = "Unexpected error occurred.";
            var expectedErrorType = ErrorTypes.UnexpectedError;

            // Act
            var result = OperationBuilder<string>.FailureUnexpectedError(expectedMessage);

            // Assert
            result.Should().NotBeNull();
            result.Message.Should().Be(expectedMessage);
        }

        [Test]
        public void FailureNetworkError_Should_Return_Correct_OperationResult()
        {
            // Arrange
            string expectedMessage = "Network error occurred.";
            var expectedErrorType = ErrorTypes.NetworkError;

            // Act
            var result = OperationBuilder<string>.FailureNetworkError(expectedMessage);

            // Assert
            result.Should().NotBeNull();
            result.Message.Should().Be(expectedMessage);
        }
    }
}
