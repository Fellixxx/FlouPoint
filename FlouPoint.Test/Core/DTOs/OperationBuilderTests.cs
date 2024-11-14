namespace FlouPoint.Test.Infrastructure.Repository
{
    using global::Application.Result;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using global::Application.Result.Error;
    using global::Domain.EnumType;

    /// <summary>
    /// A suite of unit tests for verifying the behavior of the OperationBuilder class 
    /// which constructs various types of failure OperationResults.
    /// </summary>
    [TestFixture]
    public class OperationBuilderTests
    {
        /// <summary>
        /// Sets up resources before each test run.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
        // Perform any necessary setup activities here.
        }

        /// <summary>
        /// Tests that FailureBusinessValidation method returns the correct operation result.
        /// </summary>
        [Test]
        public void FailureBusinessValidation_ShouldReturn_CorrectResult()
        {
            // Arrange
            var expectedMessage = "Business validation failed";
            var expectedError = ErrorTypes.BusinessValidationError.GetCustomName();
            // Act
            var result = OperationBuilder<string>.FailureBusinessValidation(expectedMessage);
            // Assert
            result.Message.Should().Be(expectedMessage);
            result.Error.Should().Be(expectedError);
        }

        /// <summary>
        /// Tests that FailureConfigurationMissingError method returns the correct operation result.
        /// </summary>
        [Test]
        public void FailureConfigurationMissingError_ShouldReturn_CorrectResult()
        {
            // Arrange
            var expectedMessage = "Configuration missing";
            var expectedError = ErrorTypes.ConfigurationMissingError.GetCustomName();
            // Act
            var result = OperationBuilder<string>.FailureConfigurationMissingError(expectedMessage);
            // Assert
            result.Message.Should().Be(expectedMessage);
            result.Error.Should().Be(expectedError);
        }

        /// <summary>
        /// Tests that FailureDatabase method returns the correct operation result.
        /// </summary>
        [Test]
        public void FailureDatabase_ShouldReturn_CorrectResult()
        {
            // Arrange
            var expectedMessage = "Database error";
            var expectedError = ErrorTypes.DatabaseError.GetCustomName();
            // Act
            var result = OperationBuilder<string>.FailureDatabase(expectedMessage);
            // Assert
            result.Message.Should().Be(expectedMessage);
            result.Error.Should().Be(expectedError);
        }

        /// <summary>
        /// Tests that FailureDataSubmittedInvalid method returns the correct operation result.
        /// </summary>
        [Test]
        public void FailureDataSubmittedInvalid_ShouldReturn_CorrectResult()
        {
            // Arrange
            var expectedMessage = "Invalid data submitted";
            var expectedError = ErrorTypes.DataSubmittedInvalid.GetCustomName();
            // Act
            var result = OperationBuilder<string>.FailureDataSubmittedInvalid(expectedMessage);
            // Assert
            result.Message.Should().Be(expectedMessage);
            result.Error.Should().Be(expectedError);
        }

        /// <summary>
        /// Tests that FailureExternalService method returns the correct operation result.
        /// </summary>
        [Test]
        public void FailureExternalService_ShouldReturn_CorrectResult()
        {
            // Arrange
            var expectedMessage = "External service error";
            var expectedError = ErrorTypes.ExternalServicesError.GetCustomName();
            // Act
            var result = OperationBuilder<string>.FailureExternalService(expectedMessage);
            // Assert
            result.Message.Should().Be(expectedMessage);
            result.Error.Should().Be(expectedError);
        }

        /// <summary>
        /// Tests that FailureUnexpectedError method returns the correct operation result.
        /// </summary>
        [Test]
        public void FailureUnexpectedError_ShouldReturn_CorrectResult()
        {
            // Arrange
            var expectedMessage = "Unexpected error";
            var expectedError = ErrorTypes.UnexpectedError.GetCustomName();
            // Act
            var result = OperationBuilder<string>.FailureUnexpectedError(expectedMessage);
            // Assert
            result.Message.Should().Be(expectedMessage);
            result.Error.Should().Be(expectedError);
        }
    }
}