namespace FlouPoint.Application.Test.Infrastructure.Repository
{
    using global::Application.Result;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using global::Application.Result.Error;
    using global::Domain.EnumType;

    [TestFixture]
    public class OperationBuilderTests
    {
        [Test]
        public Task When_FailureBusinessValidation_Called_Then_CorrectResult()
        {
            // Given
            var expectedMessage = "Business validation failed";

            // When
            var result = OperationBuilder<string>.FailureBusinessValidation(expectedMessage);

            // Then
            result.Message.Should().Be(expectedMessage);
            var expected = ErrorTypes.BusinessValidationError.GetCustomName();
            result.Error.Should().Be(expected);
            return Task.CompletedTask;
        }

        [Test]
        public Task When_FailureConfigurationMissingError_Called_Then_CorrectResult()
        {
            // Given
            string expectedMessage = "Configuration missing";

            // When
            OperationResult<string> result = OperationBuilder<string>.FailureConfigurationMissingError(expectedMessage);

            // Then
            result.Message.Should().Be(expectedMessage);
            string expected = ErrorTypes.ConfigurationMissingError.GetCustomName();
            result.Error.Should().Be(expected);
            return Task.CompletedTask;
        }

        [Test]
        public Task When_FailureDatabase_Called_Then_CorrectResult()
        {
            // Given
            string expectedMessage = "Database error";

            // When
            OperationResult<string> result = OperationBuilder<string>.FailureDatabase(expectedMessage);

            // Then
            result.Message.Should().Be(expectedMessage);
            string expected = ErrorTypes.DatabaseError.GetCustomName();
            result.Error.Should().Be(expected);
            return Task.CompletedTask;
        }

        [Test]
        public Task When_FailureDataSubmittedInvalid_Called_Then_CorrectResult()
        {
            // Given
            string expectedMessage = "Invalid data submitted";

            // When
            OperationResult<string> result = OperationBuilder<string>.FailureDataSubmittedInvalid(expectedMessage);

            // Then
            result.Message.Should().Be(expectedMessage);
            string expected = ErrorTypes.DataSubmittedInvalid.GetCustomName();
            result.Error.Should().Be(expected);
            return Task.CompletedTask;
        }

        [Test]
        public Task When_FailureExternalService_Called_Then_CorrectResult()
        {
            // Given
            string expectedMessage = "External service error";

            // When
            OperationResult<string> result = OperationBuilder<string>.FailureExtenalService(expectedMessage);

            // Then
            result.Message.Should().Be(expectedMessage);
            string expected = ErrorTypes.ExternalServicesError.GetCustomName();
            result.Error.Should().Be(expected);
            return Task.CompletedTask;
        }

        [Test]
        public Task When_FailureUnexpectedError_Called_Then_CorrectResult()
        {
            // Given
            string expectedMessage = "Unexpected error";

            // When
            OperationResult<string> result = OperationBuilder<string>.FailureUnexpectedError(expectedMessage);

            // Then
            result.Message.Should().Be(expectedMessage);
            string expected = ErrorTypes.UnexpectedError.GetCustomName();
            result.Error.Should().Be(expected);
            return Task.CompletedTask;
        }
    }
}
