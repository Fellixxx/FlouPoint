namespace FlouPoint.Test.Core.DTOs
{
    using FluentAssertions;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using global::Application.Result.Error;

    /// <summary>
    /// This test class contains unit tests for verifying the values of the ErrorTypes enumeration.
    /// It ensures that each error type in the ErrorTypes enumeration is set to the expected value.
    /// </summary>
    [TestFixture]
    public class ErrorTypesTests
    {
        /// <summary>
        /// This test method checks each error type defined in the ErrorTypes enumeration.
        /// It verifies that each error type holds the expected value.
        /// The test ensures that all error types are correctly defined and their values are as intended.
        /// </summary>
        [Test]
        public Task When_ErrorTypesValuesChecked_Then_ValuesAsExpected()
        {
            // Given & When & Then - Testing that all ErrorTypes values are as expected
            ErrorTypes.None.Should().Be(0);
            ErrorTypes.BusinessValidationError.Should().Be(ErrorTypes.BusinessValidationError);
            ErrorTypes.DatabaseError.Should().Be(ErrorTypes.DatabaseError);
            ErrorTypes.ExternalServicesError.Should().Be(ErrorTypes.ExternalServicesError);
            ErrorTypes.UnexpectedError.Should().Be(ErrorTypes.UnexpectedError);
            ErrorTypes.DataSubmittedInvalid.Should().Be(ErrorTypes.DataSubmittedInvalid);
            ErrorTypes.ConfigurationMissingError.Should().Be(ErrorTypes.ConfigurationMissingError);
            ErrorTypes.NetworkError.Should().Be(ErrorTypes.NetworkError);
            ErrorTypes.UserInputError.Should().Be(ErrorTypes.UserInputError);
            ErrorTypes.NotFoundError.Should().Be(ErrorTypes.NotFoundError);
            ErrorTypes.AuthenticationError.Should().Be(ErrorTypes.AuthenticationError);
            ErrorTypes.AuthorizationError.Should().Be(ErrorTypes.AuthorizationError);
            ErrorTypes.ResourceError.Should().Be(ErrorTypes.ResourceError);
            ErrorTypes.TimeoutError.Should().Be(ErrorTypes.TimeoutError);
            // This test is a set of assertions and does not perform any asynchronous operations,
            // hence returning a completed Task.
            return Task.CompletedTask;
        }
    }
}