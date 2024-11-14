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
        /// Tests if each value in the ErrorTypes enumeration matches the expected integer value.
        /// </summary>
        /// <param name = "errorType">The error type to check.</param>
        /// <param name = "expectedValue">The expected integer value of the error type.</param>
        [TestCase(ErrorTypes.None, 0)]
        [TestCase(ErrorTypes.BusinessValidationError, 1)] // assuming these as expected values, adjust accordingly
        [TestCase(ErrorTypes.DatabaseError, 2)]
        [TestCase(ErrorTypes.ExternalServicesError, 3)]
        [TestCase(ErrorTypes.UnexpectedError, 4)]
        [TestCase(ErrorTypes.DataSubmittedInvalid, 5)]
        [TestCase(ErrorTypes.ConfigurationMissingError, 6)]
        [TestCase(ErrorTypes.NetworkError, 7)]
        [TestCase(ErrorTypes.UserInputError, 8)]
        [TestCase(ErrorTypes.NotFoundError, 9)]
        [TestCase(ErrorTypes.AuthenticationError, 10)]
        [TestCase(ErrorTypes.AuthorizationError, 11)]
        [TestCase(ErrorTypes.ResourceError, 12)]
        [TestCase(ErrorTypes.TimeoutError, 13)]
        public void When_ErrorTypeIsChecked_Then_HasExpectedValue(ErrorTypes errorType, int expectedValue)
        {
            errorType.Should().Be(expectedValue);
        }
    }
}