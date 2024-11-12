using Application.Result.Error;
using Application.Result.Exceptions;
using Application.Result;

namespace Application.Test.Result
{
    [TestClass]
    public class OperationResultTests
    {
        [TestMethod]
        public void Success_Should_Create_Successful_OperationResult_With_Data_And_Message()
        {
            // Arrange
            string expectedMessage = "Operation succeeded.";
            var expectedData = "TestData";

            // Act
            var result = OperationResult<string>.Success(expectedData, expectedMessage);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(expectedData, result.Data);
            Assert.AreEqual(expectedMessage, result.Message);
        }

        [TestMethod]
        public void Success_Should_Create_Successful_OperationResult_With_Empty_Message_When_None_Provided()
        {
            // Arrange
            var expectedData = "TestData";

            // Act
            var result = OperationResult<string>.Success(expectedData);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(expectedData, result.Data);
            Assert.AreEqual(string.Empty, result.Message);
        }

        [TestMethod]
        public void Failure_Should_Create_Failed_OperationResult_With_Message_And_ErrorType()
        {
            // Arrange
            string expectedMessage = "Operation failed.";
            var expectedErrorType = ErrorTypes.BusinessValidation;

            // Act
            var result = OperationResult<string>.Failure(expectedMessage, expectedErrorType);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(expectedMessage, result.Message);
            Assert.AreEqual(expectedErrorType, result.ErrorType);
        }

        [TestMethod]
        public void AsType_Should_Convert_Failure_Result_To_Different_Generic_Type()
        {
            // Arrange
            var failureResult = OperationResult<string>.Failure("Business error", ErrorTypes.BusinessValidation);

            // Act
            var convertedResult = failureResult.AsType<int>();

            // Assert
            Assert.IsNotNull(convertedResult);
            Assert.IsFalse(convertedResult.IsSuccessful);
            Assert.AreEqual(failureResult.Message, convertedResult.Message);
            Assert.AreEqual(failureResult.ErrorType, convertedResult.ErrorType);
        }

        [TestMethod]
        public void AsType_Should_Throw_InvalidOperationResultException_If_IsSuccessful_Is_True()
        {
            // Arrange
            var successResult = OperationResult<string>.Success("Success");

            // Act & Assert
            var exception = Assert.ThrowsException<InvalidOperationResultException>(() => successResult.AsType<int>());

            Assert.AreEqual("This method can only be used if the value of IsSuccessful is false.", exception.Message);
        }

        [TestMethod]
        public void ToResultWithBoolType_Should_Convert_To_Bool_Type_When_Failure()
        {
            // Arrange
            var failureResult = OperationResult<string>.Failure("Failure occurred", ErrorTypes.BusinessValidation);

            // Act
            var boolResult = failureResult.ToResultWithBoolType();

            // Assert
            Assert.IsNotNull(boolResult);
            Assert.IsFalse(boolResult.IsSuccessful);
            Assert.AreEqual(failureResult.Message, boolResult.Message);
            Assert.AreEqual(failureResult.ErrorType, boolResult.ErrorType);
        }

        [TestMethod]
        public void ToResultWithIntType_Should_Convert_To_Int_Type_When_Failure()
        {
            // Arrange
            var failureResult = OperationResult<string>.Failure("Failure occurred", ErrorTypes.Database);

            // Act
            var intResult = failureResult.ToResultWithIntType();

            // Assert
            Assert.IsNotNull(intResult);
            Assert.IsFalse(intResult.IsSuccessful);
            Assert.AreEqual(failureResult.Message, intResult.Message);
            Assert.AreEqual(failureResult.ErrorType, intResult.ErrorType);
        }

        [TestMethod]
        public void ToResultWithStringType_Should_Convert_To_String_Type_When_Failure()
        {
            // Arrange
            var failureResult = OperationResult<int>.Failure("Failure occurred", ErrorTypes.ExternalService);

            // Act
            var stringResult = failureResult.ToResultWithStringType();

            // Assert
            Assert.IsNotNull(stringResult);
            Assert.IsFalse(stringResult.IsSuccessful);
            Assert.AreEqual(failureResult.Message, stringResult.Message);
            Assert.AreEqual(failureResult.ErrorType, stringResult.ErrorType);
        }

        [TestMethod]
        public void ToResultWithGenericType_Should_Return_Original_Generic_Type()
        {
            // Arrange
            var failureResult = OperationResult<string>.Failure("Generic failure", ErrorTypes.Unexpected);

            // Act
            var genericResult = failureResult.ToResultWithGenericType();

            // Assert
            Assert.IsNotNull(genericResult);
            Assert.IsFalse(genericResult.IsSuccessful);
            Assert.AreEqual(failureResult.Message, genericResult.Message);
            Assert.AreEqual(failureResult.ErrorType, genericResult.ErrorType);
        }

        [TestMethod]
        public void ToResultWithXType_Should_Convert_To_Specified_Generic_Type_When_Failure()
        {
            // Arrange
            var failureResult = OperationResult<string>.Failure("Failure occurred", ErrorTypes.Network);

            // Act
            var xResult = failureResult.ToResultWithXType<DateTime>();

            // Assert
            Assert.IsNotNull(xResult);
            Assert.IsFalse(xResult.IsSuccessful);
            Assert.AreEqual(failureResult.Message, xResult.Message);
            Assert.AreEqual(failureResult.ErrorType, xResult.ErrorType);
        }
    }
}
