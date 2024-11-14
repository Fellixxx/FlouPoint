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
        public void SetIsSuccessful(bool value) => IsSuccessful = value;
        public void SetData(T value) => Data = value;
        public void SetMessage(string value) => Message = value;
        public void SetErrorType(ErrorTypes value) => ErrorType = value;
    }

    /// <summary>
    /// Test fixture for testing the Result class.
    /// </summary>
    [TestFixture]
    public class ResultTests
    {
        private TestableResult<int> _result;
        [SetUp]
        public void Setup()
        {
            _result = new TestableResult<int>();
        }

        [TearDown]
        public void Teardown()
        {
            _result = null;
        }

#region ErrorType Tests
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
        public void When_Result_Set_ErrorType_Then_Get_Expected_ErrorString(ErrorTypes errorType)
        {
            // Given
            _result.SetErrorType(errorType);
            // When
            var actualError = _result.Error;
            var expected = errorType.GetCustomName();
            // Then
            actualError.Should().Be(expected);
        }

#endregion
#region IsSuccessful Tests
        [Test]
        public void When_Result_Set_IsSuccessful_Then_Property_Should_Have_Expected_Value()
        {
            // Given
            bool expected = true;
            // When
            _result.SetIsSuccessful(expected);
            // Then
            _result.IsSuccessful.Should().Be(expected);
        }

#endregion
#region Data Tests
        [Test]
        public void When_Result_Set_Data_Then_Property_Should_Have_Expected_Value()
        {
            // Given
            int expectedData = 42;
            // When
            _result.SetData(expectedData);
            // Then
            _result.Data.Should().Be(expectedData);
        }

#endregion
#region Message Tests
        [Test]
        public void When_Result_Set_Message_Then_Property_Should_Have_Expected_Value()
        {
            // Given
            string expectedMessage = "Test message";
            // When
            _result.SetMessage(expectedMessage);
            // Then
            _result.Message.Should().Be(expectedMessage);
        }
#endregion
    }
}