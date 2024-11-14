namespace FlouPoint.Test._02.Core.DTOs
{
    using FluentAssertions;
    using global::Domain.EnumType;
    using global::Application.Result;
    using global::Application.Result.Error;

    namespace FlouPoint.Test.Application.Result
    {
        internal class OperationResultTest
        {
            private const string MessageFailure = "Operation failed.";
            private const string SuccessData = "Test data";
            [SetUp]
            public void Setup()
            {
            // Common setup could be placed here if needed, like mocking dependencies.
            }

            [Test]
            public void When_Operation_Success_Then_SuccessObjectCreated()
            {
                ValidateSuccess(SuccessData, OperationResult<string>.Success(SuccessData));
            }

            [Test]
            public void When_Operation_FailureBusinessValidation_Then_FailureObjectCreated()
            {
                ValidateFailure(ErrorTypes.BusinessValidationError, OperationBuilder<string>.FailureBusinessValidation(MessageFailure));
            }

            [Test]
            public void When_Operation_FailureDatabase_Then_FailureObjectCreated()
            {
                ValidateFailure(ErrorTypes.DatabaseError, OperationBuilder<string>.FailureDatabase(MessageFailure));
            }

            [Test]
            public void When_Operation_FailureExternalService_Then_FailureObjectCreated()
            {
                ValidateFailure(ErrorTypes.ExternalServicesError, OperationBuilder<string>.FailureExternalService(MessageFailure));
            }

            [Test]
            public void When_Operation_FailureUnexpectedError_Then_FailureObjectCreated()
            {
                ValidateFailure(ErrorTypes.UnexpectedError, OperationBuilder<string>.FailureUnexpectedError(MessageFailure));
            }

            [Test]
            public void When_Operation_FailureDataSubmittedInvalid_Then_FailureObjectCreated()
            {
                ValidateFailure(ErrorTypes.DataSubmittedInvalid, OperationBuilder<string>.FailureDataSubmittedInvalid(MessageFailure));
            }

            [Test]
            public void When_Operation_FailureConfigurationMissingError_Then_FailureObjectCreated()
            {
                ValidateFailure(ErrorTypes.ConfigurationMissingError, OperationBuilder<string>.FailureConfigurationMissingError(MessageFailure));
            }

            [Test]
            public void When_Operation_SuccessWithNullData_Then_SuccessObjectCreatedWithNoData()
            {
                ValidateSuccess(null, OperationResult<string>.Success(null));
            }

            private void ValidateSuccess(string expectedData, OperationResult<string> result)
            {
                result.Should().NotBeNull();
                result.IsSuccessful.Should().BeTrue();
                result.Message.Should().BeNullOrEmpty();
                result.Data.Should().Be(expectedData);
            }

            private void ValidateFailure(ErrorTypes expectedErrorType, OperationResult<string> result)
            {
                string expectedError = expectedErrorType.GetCustomName();
                result.Should().NotBeNull();
                result.IsSuccessful.Should().BeFalse();
                result.Error.Should().Be(expectedError);
                result.Message.Should().Be(MessageFailure);
                result.Data.Should().BeNull();
            }
        }
    }
}