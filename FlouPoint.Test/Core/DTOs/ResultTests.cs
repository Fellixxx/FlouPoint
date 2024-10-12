namespace FlouPoint.Test.Application.Result
{
    using global::Application.Result;
    using global::Application.Result.Error;
    using global::Domain.EnumType;
    using FluentAssertions;
    using NUnit.Framework;
    using System.Threading.Tasks;

    public class TestableResult<T> : Result<T>
    {
        public void SetIsSuccessful(bool value) => IsSuccessful = value;
        public void SetData(T value) => Data = value;
        public void SetMessage(string value) => Message = value;
        public void SetErrorType(ErrorTypes value) => ErrorType = value;
    }

    [TestFixture]
    public class ResultTests
    {
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
