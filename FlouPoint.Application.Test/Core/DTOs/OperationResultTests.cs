namespace FlouPoint.Application.Test._02.Core.DTOs
{
    using FluentAssertions;
    using global::Domain.EnumType;
    using global::Application.Result;
    using global::Application.Result.Error;

    namespace FlouPoint.Application.Test.Application.Result
    {
        internal class OperationResultTest
        {
            private const string MessageFailure = "Operation failed.";

            [SetUp]
            public void Setup()
            {
            }

            [Test]
            public void When_Operation_Success_Then_SuccessObjectCreated()
            {
                // Given
                string data = "Test data";

                // When
                OperationResult<string> result = OperationResult<string>.Success(data);

                // Then
                result.Should().NotBeNull();
                result.IsSuccessful.Should().BeTrue();
                result.Message.Should().BeNullOrEmpty();
                result.Data.Should().Be(data);
            }

            [Test]
            public void When_Operation_FailureBusinessValidation_Then_FailureObjectCreated()
            {
                // When
                OperationResult<string> result = OperationBuilder<string>.FailureBusinessValidation(MessageFailure);

                // Then
                result.Should().NotBeNull();
                result.IsSuccessful.Should().BeFalse();
                string expected = ErrorTypes.BusinessValidationError.GetCustomName();
                result.Error.Should().Be(expected);
                result.Message.Should().Be(MessageFailure);
                result.Data.Should().BeNull();
            }

            [Test]
            public void When_Operation_FailureDatabase_Then_FailureObjectCreated()
            {
                // When
                OperationResult<string> result = OperationBuilder<string>.FailureDatabase(MessageFailure);

                // Then
                result.Should().NotBeNull();
                result.IsSuccessful.Should().BeFalse();
                string expected = ErrorTypes.DatabaseError.GetCustomName();
                result.Error.Should().Be(expected);
                result.Message.Should().Be(MessageFailure);
                result.Data.Should().BeNull();
            }

            [Test]
            public void When_Operation_FailureExternalService_Then_FailureObjectCreated()
            {
                // When
                OperationResult<string> result = OperationBuilder<string>.FailureExtenalService(MessageFailure);

                // Then
                result.Should().NotBeNull();
                result.IsSuccessful.Should().BeFalse();
                string expected = ErrorTypes.ExternalServicesError.GetCustomName();
                result.Error.Should().Be(expected);
                result.Message.Should().Be(MessageFailure);
                result.Data.Should().BeNull();
            }

            [Test]
            public void When_Operation_FailureUnexpectedError_Then_FailureObjectCreated()
            {
                // When
                OperationResult<string> result = OperationBuilder<string>.FailureUnexpectedError(MessageFailure);

                // Then
                result.Should().NotBeNull();
                result.IsSuccessful.Should().BeFalse();
                string expected = ErrorTypes.UnexpectedError.GetCustomName();
                result.Error.Should().Be(expected);
                result.Message.Should().Be(MessageFailure);
                result.Data.Should().BeNull();
            }

            [Test]
            public void When_Operation_FailureDataSubmittedInvalid_Then_FailureObjectCreated()
            {
                // When
                OperationResult<string> result = OperationBuilder<string>.FailureDataSubmittedInvalid(MessageFailure);

                // Then
                result.Should().NotBeNull();
                result.IsSuccessful.Should().BeFalse();
                string expected = ErrorTypes.DataSubmittedInvalid.GetCustomName();
                result.Error.Should().Be(expected);
                result.Message.Should().Be(MessageFailure);
                result.Data.Should().BeNull();
            }

            [Test]
            public void When_Operation_FailureConfigurationMissingError_Then_FailureObjectCreated()
            {
                // When
                OperationResult<string> result = OperationBuilder<string>.FailureConfigurationMissingError(MessageFailure);

                // Then
                result.Should().NotBeNull();
                result.IsSuccessful.Should().BeFalse();
                string expected = ErrorTypes.ConfigurationMissingError.GetCustomName();
                result.Error.Should().Be(expected);
                result.Message.Should().Be(MessageFailure);
                result.Data.Should().BeNull();
            }
        }
    }

}
