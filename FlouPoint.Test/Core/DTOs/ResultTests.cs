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
        /// <summary>Sets the value of the IsSuccessful property.</summary>
        public void SetIsSuccessful(bool value) => IsSuccessful = value;
        /// <summary>Sets the value of the Data property.</summary>
        public void SetData(T value) => Data = value;
        /// <summary>Sets the value of the Message property.</summary>
        public void SetMessage(string value) => Message = value;
        /// <summary>Sets the value of the ErrorType property.</summary>
        public void SetErrorType(ErrorTypes value) => ErrorType = value;
    }

    /// <summary>
    /// Test fixture for testing the Result class.
    /// </summary>
    [TestFixture]
    public class ResultTests
    {
        private TestableResult<int> _result;
        /// <summary>Initializes a new instance of the TestableResult before each test.</summary>
        [SetUp]
        public void Setup()
        {
            _result = new TestableResult<int>();
        }

        /// <summary>Clears the initialized TestableResult after each test.</summary>
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
        public void ErrorType_ShouldReturnExpectedErrorString_WhenSet(ErrorTypes errorType)
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
        public void IsSuccessful_ShouldHaveExpectedValue_WhenSet()
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
        public void Data_ShouldHaveExpectedValue_WhenSet()
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
        public void Message_ShouldHaveExpectedValue_WhenSet()
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