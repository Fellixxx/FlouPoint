namespace FlouPoint.Test._02.Core.DTOs
{
    using FluentAssertions;
    using global::Domain.EnumType;
    using global::Application.Result;
    using global::Application.Result.Error;

    namespace FlouPoint.Test.Application.Result
    {
        /// <summary>
        /// Unit tests for the OperationResult class.
        /// </summary>
        internal class OperationResultTest
        {
            private const string MessageFailure = "Operation failed.";
            private const string SuccessData = "Test data";
            /// <summary>
            /// Initializes common resources for the tests.
            /// </summary>
            [SetUp]
            public void Setup()
            {
            // Common setup could be placed here if needed, like mocking dependencies.
            }

            /// <summary>
            /// Validates that a successful operation creates a success object.
            /// </summary>
            [Test]
            public void SuccessfullyCreatesSuccessObject_WhenOperationSucceeds()
            {
                ValidateSuccess(SuccessData, OperationResult<string>.Success(SuccessData));
            }

            /// <summary>
            /// Validates that a business validation failure creates a failure object.
            /// </summary>
            [Test]
            public void CreatesFailureObject_WhenOperationFailsWithBusinessValidationError()
            {
                ValidateFailure(ErrorTypes.BusinessValidationError, OperationBuilder<string>.FailureBusinessValidation(MessageFailure));
            }

            /// <summary>
            /// Validates that a database failure creates a failure object.
            /// </summary>
            [Test]
            public void CreatesFailureObject_WhenOperationFailsWithDatabaseError()
            {
                ValidateFailure(ErrorTypes.DatabaseError, OperationBuilder<string>.FailureDatabase(MessageFailure));
            }

            /// <summary>
            /// Validates that an external service failure creates a failure object.
            /// </summary>
            [Test]
            public void CreatesFailureObject_WhenOperationFailsWithExternalServiceError()
            {
                ValidateFailure(ErrorTypes.ExternalServicesError, OperationBuilder<string>.FailureExternalService(MessageFailure));
            }

            /// <summary>
            /// Validates that an unexpected error failure creates a failure object.
            /// </summary>
            [Test]
            public void CreatesFailureObject_WhenOperationFailsWithUnexpectedError()
            {
                ValidateFailure(ErrorTypes.UnexpectedError, OperationBuilder<string>.FailureUnexpectedError(MessageFailure));
            }

            /// <summary>
            /// Validates that a data submission failure creates a failure object.
            /// </summary>
            [Test]
            public void CreatesFailureObject_WhenOperationFailsWithDataSubmittedInvalid()
            {
                ValidateFailure(ErrorTypes.DataSubmittedInvalid, OperationBuilder<string>.FailureDataSubmittedInvalid(MessageFailure));
            }

            /// <summary>
            /// Validates that a configuration missing error creates a failure object.
            /// </summary>
            [Test]
            public void CreatesFailureObject_WhenOperationFailsWithConfigurationMissingError()
            {
                ValidateFailure(ErrorTypes.ConfigurationMissingError, OperationBuilder<string>.FailureConfigurationMissingError(MessageFailure));
            }

            /// <summary>
            /// Validates that a successful operation with null data creates a success object with no data.
            /// </summary>
            [Test]
            public void CreatesSuccessObjectWithNoData_WhenOperationSucceedsWithNullData()
            {
                ValidateSuccess(null, OperationResult<string>.Success(null));
            }

            /// <summary>
            /// Asserts that the result represents a successful operation and matches expected data.
            /// </summary>
            private void ValidateSuccess(string expectedData, OperationResult<string> result)
            {
                result.Should().NotBeNull();
                result.IsSuccessful.Should().BeTrue();
                result.Message.Should().BeNullOrEmpty();
                result.Data.Should().Be(expectedData);
            }

            /// <summary>
            /// Asserts that the result represents a failed operation with the expected error.
            /// </summary>
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