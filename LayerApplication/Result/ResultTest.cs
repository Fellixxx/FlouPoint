using Application.Result.Error;
using Application.Result;
using NUnit.Framework;
using FluentAssertions;

namespace LayerApplication.Result
{
    [TestFixture]
    public class ResultTests
    {
        private class TestResult : Result<string>
        {
            public TestResult(bool isSuccess, string message, string data, ErrorTypes errorType)
            {
                IsSuccessful = isSuccess;
                Message = message;
                Data = data;
                ErrorType = errorType;
            }
        }

        [Test]
        public void Result_Should_Initialize_With_Correct_Values()
        {
            // Arrange
            var isSuccess = true;
            var message = "Operation completed successfully.";
            var data = "TestData";
            var errorType = ErrorTypes.None;

            // Act
            var result = new TestResult(isSuccess, message, data, errorType);

            // Assert
            result.IsSuccessful.Should().Be(isSuccess);
            result.Message.Should().Be(message);
            result.Data.Should().Be(data);
        }

        [Test]
        public void Result_Should_Return_Correct_Error_Custom_Name()
        {
            // Arrange
            var result = new TestResult(false, "An error occurred", "TestData", ErrorTypes.DatabaseError);

            // Act
            var errorName = result.Error;

            // Assert
            errorName.Should().Be("DATABASE_ERROR");
        }

        [Test]
        public void Result_Should_Return_Default_Error_Custom_Name_When_No_Error()
        {
            // Arrange
            var result = new TestResult(true, "Operation completed successfully", "TestData", ErrorTypes.None);

            // Act
            var errorName = result.Error;

            // Assert
            errorName.Should().Be("NONE");
        }

        [Test]
        public void Result_Should_Have_Null_Data_And_Message_When_Not_Set()
        {
            // Arrange
            var result = new TestResult(true, null, null, ErrorTypes.None);

            // Assert
            result.Data.Should().BeNull();
            result.Message.Should().BeNull();
            result.IsSuccessful.Should().BeTrue();
        }

        [Test]
        public void Result_Should_Indicate_Failure_With_ErrorType()
        {
            // Arrange
            var result = new TestResult(false, "An unexpected error occurred", null, ErrorTypes.UnexpectedError);

            // Assert
            result.IsSuccessful.Should().BeFalse();
            result.Error.Should().Be("UNEXPECTED_ERROR");
            result.Message.Should().Be("An unexpected error occurred");
            result.Data.Should().BeNull();
        }
    }
}
