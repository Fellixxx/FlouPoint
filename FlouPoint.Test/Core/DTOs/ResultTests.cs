namespace FlouPoint.Test.Application.Result
{
    using global::Application.Result;
    using global::Application.Result.Error;
    using global::Domain.EnumType;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Threading.Tasks;

    /// <summary>
    /// This class extends the Result class by allowing explicit setting of properties for testing purposes.
    /// </summary>
    public class TestableResult<T> : Result<T>
    {
        /// <summary>
        /// Sets the IsSuccessful property of the result.
        /// </summary>
        public void SetIsSuccessful(bool value) => IsSuccessful = value;
        /// <summary>
        /// Sets the Data property of the result.
        /// </summary>
        public void SetData(T value) => Data = value;
        /// <summary>
        /// Sets the Message property of the result.
        /// </summary>
        public void SetMessage(string value) => Message = value;
        /// <summary>
        /// Sets the ErrorType property of the result.
        /// </summary>
        public void SetErrorType(ErrorTypes value) => ErrorType = value;
    }

    /// <summary>
    /// Test fixture for testing the Result class.
    /// </summary>
    [TestFixture]
    public class ResultTests
    {
        /// <summary>
        /// Tests that setting the ErrorType property of a TestableResult results in the expected error string.
        /// </summary>
        [TestCase(ErrorTypes.None)]
        [TestCase(ErrorTypes.BusinessValidationError)]
        [TestCase(ErrorTypes.DatabaseError)]
        [TestCase(ErrorTypes.ExternalServicesError)]
        [TestCase(ErrorTypes.UnexpectedError)]
        [TestCase(ErrorTypes.DataSubmittedInvalid)]
        [TestCase(ErrorTypes.ConfigurationMissingError)]
        [TestCase(ErrorTypes.NetworkError)]
        [TestCase(ErrorTypes.UserInputError)]
        [TestCase(ErrorTypes.NotFoundError)]
        [TestCase(ErrorTypes.AuthenticationError)]
        [TestCase(ErrorTypes.AuthorizationError)]
        [TestCase(ErrorTypes.ResourceError)]
        [TestCase(ErrorTypes.TimeoutError)]
        public Task When_Result_Set_ErrorType_Then_Get_Expected_ErrorString(ErrorTypes errorType)
        {
            // Given
            var result = new TestableResult<int>();
            result.SetErrorType(errorType);
            // When
            var actualError = result.Error;
            var expected = errorType.GetCustomName();
            // Then
            actualError.Should().Be(expected);
            return Task.CompletedTask;
        }
    }
}