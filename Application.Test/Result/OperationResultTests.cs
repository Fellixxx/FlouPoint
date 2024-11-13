using Application.Result.Error;
using Application.Result.Exceptions;
using Application.Result;

namespace Application.Test.Result
{
    /// <summary>
    /// Contains unit tests for the <c>OperationResult</c> class, validating the behavior of
    /// success and failure operations and the conversion between different generic types.
    /// </summary>
    [TestClass]
    public class OperationResultTests
    {
        /// <summary>
        /// Tests that a successful operation result is correctly created with provided data and message.
        /// </summary>
        [TestMethod]
        public void Success_Should_Create_Successful_OperationResult_With_Data_And_Message()
        {
            // Arrange
            string expectedMessage = "Operation succeeded.";
            var expectedData = "TestData";
            // Act
            var result = Operation<string>.Success(expectedData, expectedMessage);
            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(expectedData, result.Data);
            Assert.AreEqual(expectedMessage, result.Message);
        }

        /// <summary>
        /// Tests that a successful operation result is created with data and an empty message when no message is provided.
        /// </summary>
        [TestMethod]
        public void Success_Should_Create_Successful_OperationResult_With_Empty_Message_When_None_Provided()
        {
            // Arrange
            var expectedData = "TestData";
            // Act
            var result = Operation<string>.Success(expectedData);
            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(expectedData, result.Data);
            Assert.AreEqual(string.Empty, result.Message);
        }

        /// <summary>
        /// Verifies that a failure operation result is correctly created with a message and error type.
        /// </summary>
        [TestMethod]
        public void Failure_Should_Create_Failed_OperationResult_With_Message_And_ErrorType()
        {
            // Arrange
            string expectedMessage = "Operation failed.";
            var expectedErrorType = ErrorTypes.BusinessValidation;
            // Act
            var result = Operation<string>.Failure(expectedMessage, expectedErrorType);
            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(expectedMessage, result.Message);
            Assert.AreEqual(expectedErrorType, result.Type);
        }

        /// <summary>
        /// Tests that a failure operation result can be converted to a different generic type, preserving the failure state.
        /// </summary>
        [TestMethod]
        public void AsType_Should_Convert_Failure_Result_To_Different_Generic_Type()
        {
            // Arrange
            var failureResult = Operation<string>.Failure("Business error", ErrorTypes.BusinessValidation);
            // Act
            var convertedResult = failureResult.AsType<int>();
            // Assert
            Assert.IsNotNull(convertedResult);
            Assert.IsFalse(convertedResult.IsSuccessful);
            Assert.AreEqual(failureResult.Message, convertedResult.Message);
            Assert.AreEqual(failureResult.Type, convertedResult.Type);
        }

        /// <summary>
        /// Ensures that converting a successful result to a different type throws an exception, highlighting that it can only be done for failed results.
        /// </summary>
        [TestMethod]
        public void AsType_Should_Throw_InvalidOperationResultException_If_IsSuccessful_Is_True()
        {
            // Arrange
            var successResult = Operation<string>.Success("Success");
            // Act & Assert
            var exception = Assert.ThrowsException<InvalidOperation>(() => successResult.AsType<int>());
            Assert.AreEqual("This method can only be used if the value of IsSuccessful is false.", exception.Message);
        }

        /// <summary>
        /// Checks that a failure operation result can be converted to a boolean generic type, keeping the failure state intact.
        /// </summary>
        [TestMethod]
        public void ToResultWithBoolType_Should_Convert_To_Bool_Type_When_Failure()
        {
            // Arrange
            var failureResult = Operation<string>.Failure("Failure occurred", ErrorTypes.BusinessValidation);
            // Act
            var boolResult = failureResult.ConvertTo<bool>();
            // Assert
            Assert.IsNotNull(boolResult);
            Assert.IsFalse(boolResult.IsSuccessful);
            Assert.AreEqual(failureResult.Message, boolResult.Message);
            Assert.AreEqual(failureResult.Type, boolResult.Type);
        }

        /// <summary>
        /// Verifies that a failure operation result can be converted to an integer generic type while maintaining the failure state.
        /// </summary>
        [TestMethod]
        public void ToResultWithIntType_Should_Convert_To_Int_Type_When_Failure()
        {
            // Arrange
            var failureResult = Operation<string>.Failure("Failure occurred", ErrorTypes.Database);
            // Act
            var intResult = failureResult.ConvertTo<int>();
            // Assert
            Assert.IsNotNull(intResult);
            Assert.IsFalse(intResult.IsSuccessful);
            Assert.AreEqual(failureResult.Message, intResult.Message);
            Assert.AreEqual(failureResult.Type, intResult.Type);
        }

        /// <summary>
        /// Tests that a failure operation result can be converted to a string generic type, retaining the failure status.
        /// </summary>
        [TestMethod]
        public void ToResultWithStringType_Should_Convert_To_String_Type_When_Failure()
        {
            // Arrange
            var failureResult = Operation<int>.Failure("Failure occurred", ErrorTypes.ExternalService);
            // Act
            var stringResult = failureResult.ConvertTo<string>();
            // Assert
            Assert.IsNotNull(stringResult);
            Assert.IsFalse(stringResult.IsSuccessful);
            Assert.AreEqual(failureResult.Message, stringResult.Message);
            Assert.AreEqual(failureResult.Type, stringResult.Type);
        }

        /// <summary>
        /// Ensures that a failure result of a certain generic type can be returned as the same type, preserving the failure condition.
        /// </summary>
        [TestMethod]
        public void ToResultWithGenericType_Should_Return_Original_Generic_Type()
        {
            // Arrange
            var failureResult = Operation<string>.Failure("Generic failure", ErrorTypes.Unexpected);
            // Act
            var genericResult = failureResult.ConvertTo<string>();
            // Assert
            Assert.IsNotNull(genericResult);
            Assert.IsFalse(genericResult.IsSuccessful);
            Assert.AreEqual(failureResult.Message, genericResult.Message);
            Assert.AreEqual(failureResult.Type, genericResult.Type);
        }

        /// <summary>
        /// Checks conversion of a failure result to a specified generic type (e.g., DateTime), ensuring the conversion preserves the failure.
        /// </summary>
        [TestMethod]
        public void ToResultWithXType_Should_Convert_To_Specified_Generic_Type_When_Failure()
        {
            // Arrange
            var failureResult = Operation<string>.Failure("Failure occurred", ErrorTypes.Network);
            // Act
            var xResult = failureResult.ConvertTo<DateTime>();
            // Assert
            Assert.IsNotNull(xResult);
            Assert.IsFalse(xResult.IsSuccessful);
            Assert.AreEqual(failureResult.Message, xResult.Message);
            Assert.AreEqual(failureResult.Type, xResult.Type);
        }
    }
}