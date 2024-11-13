namespace FlouPoint.Test._02.Core.DTOs
{
    using FluentAssertions;
    using global::Domain.EnumType;
    using global::Application.Result;
    using global::Application.Result.Error;

    namespace FlouPoint.Test.Application.Result
    {
        /// <summary>
        /// Contains unit tests for validating the <see cref = "OperationResult{T}"/> behavior
        /// in various success and failure scenarios.
        /// </summary>
        internal class OperationResultTest
        {
            private const string MessageFailure = "Operation failed.";
            /// <summary>
            /// Setup method executed before each test. Used for initializing common resources.
            /// </summary>
            [SetUp]
            public void Setup()
            {
            }

            /// <summary>
            /// Validates that a successful operation results in a successfully created object
            /// with the correct data and no error message.
            /// </summary>
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

            /// <summary>
            /// Validates that an operation failing due to business validation error
            /// results in correct error type and message being assigned.
            /// </summary>
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

            /// <summary>
            /// Validates that an operation failing due to a database error
            /// results in correct error type and message being assigned.
            /// </summary>
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

            /// <summary>
            /// Validates that an operation failing due to external service error
            /// results in correct error type and message being assigned.
            /// </summary>
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

            /// <summary>
            /// Validates that an operation failing due to an unexpected error
            /// results in correct error type and message being assigned.
            /// </summary>
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

            /// <summary>
            /// Validates that an operation failing due to invalid data submission
            /// results in correct error type and message being assigned.
            /// </summary>
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

            /// <summary>
            /// Validates that an operation failing due to missing configuration
            /// results in correct error type and message being assigned.
            /// </summary>
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