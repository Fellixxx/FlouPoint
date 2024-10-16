using Application.Result.Error;
using Application.Result.Exceptions;
using Application.Result;
using FluentAssertions;
using NUnit.Framework;

namespace LayerApplication.Result
{
    [TestFixture]
    public class OperationResultTests
    {
        [Test]
        public void Success_Should_Create_Successful_OperationResult()
        {
            // Arrange
            var data = "TestData";
            var message = "Operation successful";

            // Act
            var result = OperationResult<string>.Success(data, message);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeTrue();
            result.Data.Should().Be(data);
            result.Message.Should().Be(message);
 
        }

        [Test]
        public void Failure_Should_Create_Failed_OperationResult_With_Correct_ErrorType()
        {
            // Arrange
            var message = "Operation failed due to a database error";
            var errorType = ErrorTypes.DatabaseError;

            // Act
            var result = OperationResult<string>.Failure(message, errorType);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeFalse();
            result.Message.Should().Be(message);
        }

        [Test]
        public void AsType_Should_Throw_InvalidOperationResultException_When_IsSuccessful_Is_True()
        {
            // Arrange
            var result = OperationResult<string>.Success("TestData", "Operation successful");

            // Act
            Action action = () => result.AsType<int>();

            // Assert
            action.Should().Throw<InvalidOperationResultException>()
                  .WithMessage("This method can only be used if the value of IsSuccessful is false.");
        }

        [Test]
        public void AsType_Should_Convert_FailureResult_To_Different_Generic_Type()
        {
            // Arrange
            var message = "Operation failed due to a network error";
            var result = OperationResult<string>.Failure(message, ErrorTypes.NetworkError);

            // Act
            var resultAsInt = result.AsType<int>();

            // Assert
            resultAsInt.Should().NotBeNull();
            resultAsInt.IsSuccessful.Should().BeFalse();
            resultAsInt.Message.Should().Be(message);
        }

        [Test]
        public void ToResultWithBoolType_Should_Convert_FailureResult_To_Bool()
        {
            // Arrange
            var result = OperationResult<string>.Failure("Operation failed", ErrorTypes.UnexpectedError);

            // Act
            var boolResult = result.ToResultWithBoolType();

            // Assert
            boolResult.Should().NotBeNull();
            boolResult.IsSuccessful.Should().BeFalse();
            boolResult.Message.Should().Be("Operation failed");
        }

        [Test]
        public void ToResultWithIntType_Should_Convert_FailureResult_To_Int()
        {
            // Arrange
            var result = OperationResult<string>.Failure("Operation failed", ErrorTypes.DatabaseError);

            // Act
            var intResult = result.ToResultWithIntType();

            // Assert
            intResult.Should().NotBeNull();
            intResult.IsSuccessful.Should().BeFalse();
            intResult.Message.Should().Be("Operation failed");
        }

        [Test]
        public void ToResultWithStringType_Should_Convert_FailureResult_To_String()
        {
            // Arrange
            var result = OperationResult<string>.Failure("Operation failed", ErrorTypes.ConfigurationMissingError);

            // Act
            var stringResult = result.ToResultWithStringType();

            // Assert
            stringResult.Should().NotBeNull();
            stringResult.IsSuccessful.Should().BeFalse();
            stringResult.Message.Should().Be("Operation failed");
        }

        [Test]
        public void ToResultWithGenericType_Should_Convert_FailureResult_To_Generic_Type()
        {
            // Arrange
            var result = OperationResult<string>.Failure("Operation failed", ErrorTypes.ResourceError);

            // Act
            var genericResult = result.ToResultWithGenericType();

            // Assert
            genericResult.Should().NotBeNull();
            genericResult.IsSuccessful.Should().BeFalse();
            genericResult.Message.Should().Be("Operation failed");
        }

        [Test]
        public void Failure_Should_Create_OperationResult_With_Empty_Message_When_Message_Is_Null()
        {
            // Arrange
            var errorType = ErrorTypes.NetworkError;

            // Act
            var result = OperationResult<string>.Failure(null, errorType);

            // Assert
            result.Should().NotBeNull();
            result.Message.Should().BeNull();
            result.IsSuccessful.Should().BeFalse();
        }
    }
}
