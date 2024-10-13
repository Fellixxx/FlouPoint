namespace FlouPoint.LayerApplication.Test.Result
{
    using Application.Result;
    using Application.Result.Error;
    using Application.Result.Exceptions;
    using FluentAssertions;
    using Application.Result.Error;
    using Application.Result.Exceptions;
    using Application.Result;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class OperationResultTests
    {
        [Test]
        public void Success_Should_Create_Successful_OperationResult_With_Data_And_Message()
        {
            // Arrange
            string expectedMessage = "Operation succeeded.";
            var expectedData = "TestData";

            // Act
            var result = OperationResult<string>.Success(expectedData, expectedMessage);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeTrue();
            result.Data.Should().Be(expectedData);
            result.Message.Should().Be(expectedMessage);
        }

        [Test]
        public void Success_Should_Create_Successful_OperationResult_With_Empty_Message_When_None_Provided()
        {
            // Arrange
            var expectedData = "TestData";

            // Act
            var result = OperationResult<string>.Success(expectedData);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeTrue();
            result.Data.Should().Be(expectedData);
            result.Message.Should().BeEmpty();
        }

        [Test]
        public void Failure_Should_Create_Failed_OperationResult_With_Message_And_ErrorType()
        {
            // Arrange
            string expectedMessage = "Operation failed.";
            var expectedErrorType = ErrorTypes.BusinessValidationError;

            // Act
            var result = OperationResult<string>.Failure(expectedMessage, expectedErrorType);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeFalse();
            result.Message.Should().Be(expectedMessage);
        }

        [Test]
        public void AsType_Should_Convert_Failure_Result_To_Different_Generic_Type()
        {
            // Arrange
            var failureResult = OperationResult<string>.Failure("Business error", ErrorTypes.BusinessValidationError);

            // Act
            var convertedResult = failureResult.AsType<int>();

            // Assert
            convertedResult.Should().NotBeNull();
            convertedResult.IsSuccessful.Should().BeFalse();
            convertedResult.Message.Should().Be(failureResult.Message);
        }

        [Test]
        public void AsType_Should_Throw_InvalidOperationResultException_If_IsSuccessful_Is_True()
        {
            // Arrange
            var successResult = OperationResult<string>.Success("Success");

            // Act
            Action act = () => successResult.AsType<int>();

            // Assert
            act.Should().Throw<InvalidOperationResultException>()
                .WithMessage("This method can only be used if the value of IsSuccessful is false.");
        }

        [Test]
        public void ToResultWithBoolType_Should_Convert_To_Bool_Type_When_Failure()
        {
            // Arrange
            var failureResult = OperationResult<string>.Failure("Failure occurred", ErrorTypes.BusinessValidationError);

            // Act
            var boolResult = failureResult.ToResultWithBoolType();

            // Assert
            boolResult.Should().NotBeNull();
            boolResult.IsSuccessful.Should().BeFalse();
            boolResult.Message.Should().Be(failureResult.Message);
        }

        [Test]
        public void ToResultWithIntType_Should_Convert_To_Int_Type_When_Failure()
        {
            // Arrange
            var failureResult = OperationResult<string>.Failure("Failure occurred", ErrorTypes.DatabaseError);

            // Act
            var intResult = failureResult.ToResultWithIntType();

            // Assert
            intResult.Should().NotBeNull();
            intResult.IsSuccessful.Should().BeFalse();
            intResult.Message.Should().Be(failureResult.Message);
        }

        [Test]
        public void ToResultWithStringType_Should_Convert_To_String_Type_When_Failure()
        {
            // Arrange
            var failureResult = OperationResult<int>.Failure("Failure occurred", ErrorTypes.ExternalServicesError);

            // Act
            var stringResult = failureResult.ToResultWithStringType();

            // Assert
            stringResult.Should().NotBeNull();
            stringResult.IsSuccessful.Should().BeFalse();
            stringResult.Message.Should().Be(failureResult.Message);
        }

        [Test]
        public void ToResultWithGenericType_Should_Return_Original_Generic_Type()
        {
            // Arrange
            var failureResult = OperationResult<string>.Failure("Generic failure", ErrorTypes.UnexpectedError);

            // Act
            var genericResult = failureResult.ToResultWithGenericType();

            // Assert
            genericResult.Should().NotBeNull();
            genericResult.IsSuccessful.Should().BeFalse();
            genericResult.Message.Should().Be(failureResult.Message);
        }

        [Test]
        public void ToResultWithXType_Should_Convert_To_Specified_Generic_Type_When_Failure()
        {
            // Arrange
            var failureResult = OperationResult<string>.Failure("Failure occurred", ErrorTypes.NetworkError);

            // Act
            var xResult = failureResult.ToResultWithXType<DateTime>();

            // Assert
            xResult.Should().NotBeNull();
            xResult.IsSuccessful.Should().BeFalse();
            xResult.Message.Should().Be(failureResult.Message);
        }
    }
}

