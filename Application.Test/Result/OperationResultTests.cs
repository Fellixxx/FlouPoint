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
        public void Success_CreatesResultWithProvidedDataAndMessage()
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
        public void Success_CreatesResultWithEmptyMessageWhenNoneProvided()
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
        public void Failure_CreatesResultWithMessageAndErrorType()
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
        public void FailureResult_CanBeConvertedToDifferentGenericType()
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
        /// Ensures that converting a successful result to a different type throws an exception.
        /// </summary>
        [TestMethod]
        public void SuccessResult_AsTypeThrowsInvalidOperationResultException()
        {
            // Arrange
            var successResult = Operation<string>.Success("Success");
            // Act & Assert
            var exception = Assert.ThrowsException<InvalidOperation>(() => successResult.AsType<int>());
            Assert.AreEqual("This method can only be used if the value of IsSuccessful is false.", exception.Message);
        }

        /// <summary>
        /// Checks that a failure operation result can be converted to a boolean generic type.
        /// </summary>
        [TestMethod]
        public void FailureResult_ConvertsToBoolType()
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
        /// Verifies that a failure operation result can be converted to an integer generic type.
        /// </summary>
        [TestMethod]
        public void FailureResult_ConvertsToIntType()
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
        /// Tests that a failure operation result can be converted to a string generic type.
        /// </summary>
        [TestMethod]
        public void FailureResult_ConvertsToStringType()
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
        /// Ensures that a failure result can be returned as the same type.
        /// </summary>
        [TestMethod]
        public void FailureResult_ReturnsAsOriginalGenericType()
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
        /// Checks conversion of a failure result to a specified generic type.
        /// </summary>
        [TestMethod]
        public void FailureResult_ConvertsToSpecificGenericType()
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