using Application.Result.Error;
using Application.Result;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerApplication.Result
{
    [TestFixture]
    public class OperationBuilderTests
    {
        [Test]
        public void FailureBusinessValidation_Should_Return_OperationResult_With_BusinessValidationError()
        {
            // Arrange
            var expectedMessage = "Business validation failed.";

            // Act
            var result = OperationBuilder<string>.FailureBusinessValidation(expectedMessage);

            // Assert
            result.Should().NotBeNull();
            result.Error.Should().Be("BUSINESS_VALIDATION_ERROR");
            result.Message.Should().Be("Business validation failed.");
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public void FailureBusinessValidation_Should_Throw_ArgumentNullException_When_Message_Is_Null()
        {
            // Act
            Action action = () => OperationBuilder<string>.FailureBusinessValidation(null);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void FailureConfigurationMissingError_Should_Return_OperationResult_With_ConfigurationMissingError()
        {
            // Arrange
            var expectedMessage = "Configuration is missing.";

            // Act
            var result = OperationBuilder<string>.FailureConfigurationMissingError(expectedMessage);

            // Assert
            result.Should().NotBeNull();
            result.Error.Should().Be("CONFIGURATION_MISSING_ERROR");
            result.Message.Should().Be("Configuration is missing.");
            result.IsSuccessful.Should().Be(false);
        }

        [Test]
        public void FailureDatabase_Should_Return_OperationResult_With_DatabaseError()
        {
            // Arrange
            var expectedMessage = "Database failure occurred.";

            // Act
            var result = OperationBuilder<string>.FailureDatabase(expectedMessage);

            // Assert
            result.Should().NotBeNull();
            result.Error.Should().Be("DATABASE_ERROR"); ;
            result.Message.Should().Be("Database failure occurred.");
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public void FailureDataSubmittedInvalid_Should_Return_OperationResult_With_DataSubmittedInvalidError()
        {
            // Arrange
            var expectedMessage = "Data submitted is invalid.";

            // Act
            var result = OperationBuilder<string>.FailureDataSubmittedInvalid(expectedMessage);

            // Assert
            result.Should().NotBeNull();
            result.Error.Should().Be("DATA_SUBMITTED_INVALID");
            result.Message.Should().Be("Data submitted is invalid.");
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public void FailureExternalService_Should_Return_OperationResult_With_ExternalServicesError()
        {
            // Arrange
            var expectedMessage = "External service failed.";

            // Act
            var result = OperationBuilder<string>.FailureExtenalService(expectedMessage);

            // Assert
            result.Should().NotBeNull();
            result.Error.Should().Be("EXTERNAL_SERVICES_ERROR");
            result.Message.Should().Be("External service failed.");
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public void FailureUnexpectedError_Should_Return_OperationResult_With_UnexpectedError()
        {
            // Arrange
            var expectedMessage = "An unexpected error occurred.";

            // Act
            var result = OperationBuilder<string>.FailureUnexpectedError(expectedMessage);

            // Assert
            result.Should().NotBeNull();
            result.Message.Should().Be(expectedMessage);
            result.Data.Should().BeNull();
            result.Error.Should().Be("UNEXPECTED_ERROR");
            result.IsSuccessful.Should().Be(false);
        }

        [Test]
        public void FailureNetworkError_Should_Return_OperationResult_With_NetworkError()
        {
            // Arrange
            var expectedMessage = "Network error occurred.";

            // Act
            var result = OperationBuilder<string>.FailureNetworkError(expectedMessage);

            // Assert
            result.Should().NotBeNull();
            result.Error.Should().Be("NETWORK_ERROR");
            result.Message.Should().Be("Network error occurred.");
            result.IsSuccessful.Should().Be(false);
        }
    }
}
