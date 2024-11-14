namespace Application.Test.Result
{
    using System;
    using Application.Result;
    using Domain.Entities;
    using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests for the OperationBuilder, ensuring that different failure scenarios are correctly handled within the strategy.
    /// Each method checks various error handling techniques, such as null, empty, and whitespace error messages, 
    /// ensuring the strategy pattern responds correctly by throwing exceptions or returning expected results.
    /// </summary>
    [TestClass]
    public class OperationBuilderTests
    {
        /// <summary>
        /// Tests that a BusinessValidation failure can be created with a non-null error message and verifies the error type.
        /// </summary>
        [TestMethod]
        public void Fail_WhenBusinessValidationError_ShouldReturnExpectedResult()
        {
            // Arrange
            var message = "TestValue879633508";
            // Act
            var result = OperationStrategy<string>.Fail(message, new BusinessStrategy<string>());
            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(message, result.Message);
            Assert.AreEqual("BUSINESS_VALIDATION_ERROR", result.Error);
        }

        /// <summary>
        /// Verifies that passing a null message to the BusinessValidation failure throws an ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void Fail_WhenBusinessValidationErrorWithNullMessage_ShouldThrowArgumentNullException()
        {
            // Arrange
            string message = null;
            // Act & Assert
            var business = new BusinessStrategy<string>();
            Assert.ThrowsException<ArgumentNullException>(() => OperationStrategy<string>.Fail(message, business));
        }

        /// <summary>
        /// Tests that a ConfigurationMissing failure can be created with a non-null error message and verifies the error type.
        /// </summary>
        [TestMethod]
        public void Fail_WhenConfigurationMissingError_ShouldReturnExpectedResult()
        {
            // Arrange
            var message = "TestValue826283408";
            // Act
            var strategy = new ConfigMissingStrategy<string>();
            var result = OperationStrategy<string>.Fail(message, strategy);
            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(message, result.Message);
            Assert.AreEqual("CONFIGURATION_MISSING_ERROR", result.Error);
        }

        /// <summary>
        /// Verifies that passing a null message to the ConfigurationMissing failure throws an ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void Fail_WhenConfigurationMissingErrorWithNullMessage_ShouldThrowArgumentNullException()
        {
            // Arrange
            string message = null;
            // Act & Assert
            var strategy = new ConfigMissingStrategy<string>();
            Assert.ThrowsException<ArgumentNullException>(() => OperationStrategy<string>.Fail(message, strategy));
        }

        /// <summary>
        /// Tests that a Database failure can be created with a non-null error message and verifies the error type.
        /// </summary>
        [TestMethod]
        public void Fail_WhenDatabaseError_ShouldReturnExpectedResult()
        {
            // Arrange
            var message = "TestValue1736562805";
            // Act
            var strategy = new DatabaseStrategy<string>();
            var result = OperationStrategy<string>.Fail(message, strategy);
            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(message, result.Message);
            Assert.AreEqual("DATABASE_ERROR", result.Error);
        }

        /// <summary>
        /// Verifies that passing a null message to the Database failure throws an ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void Fail_WhenDatabaseErrorWithNullMessage_ShouldThrowArgumentNullException()
        {
            // Arrange
            string message = null;
            // Act & Assert
            var strategy = new InvalidDataStrategy<string>();
            Assert.ThrowsException<ArgumentNullException>(() => OperationStrategy<string>.Fail(message, strategy));
        }

        /// <summary>
        /// Tests that submitting invalid data results in a corresponding operation failure and error type.
        /// </summary>
        [TestMethod]
        public void Fail_WhenDataSubmittedInvalid_ShouldReturnExpectedResult()
        {
            // Arrange
            var message = "TestValue1393732451";
            // Act
            var strategy = new InvalidDataStrategy<string>();
            var result = OperationStrategy<string>.Fail(message, strategy);
            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(message, result.Message);
            Assert.AreEqual("DATA_SUBMITTED_INVALID", result.Error);
        }

        /// <summary>
        /// Verifies that passing a null message to the DataSubmittedInvalid failure throws an ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void Fail_WhenDataSubmittedInvalidWithNullMessage_ShouldThrowArgumentNullException()
        {
            // Arrange
            string message = null;
            // Act & Assert
            var strategy = new NetworkErrorStrategy<string>();
            Assert.ThrowsException<ArgumentNullException>(() => OperationStrategy<string>.Fail(message, strategy));
        }

        /// <summary>
        /// Tests that an ExternalService failure can be created with a non-null error message and verifies the error type.
        /// </summary>
        [TestMethod]
        public void Fail_WhenExternalServiceError_ShouldReturnExpectedResult()
        {
            // Arrange
            var message = "TestValue1769803281";
            // Act
            var strategy = new ExternalServiceStrategy<string>();
            var result = OperationStrategy<string>.Fail(message, strategy);
            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual("EXTERNAL_SERVICES_ERROR", result.Error);
        }

        /// <summary>
        /// Verifies that passing a null message to the ExternalService failure throws an ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void Fail_WhenExternalServiceErrorWithNullMessage_ShouldThrowArgumentNullException()
        {
            // Arrange
            string message = null;
            // Act & Assert
            var strategy = new ExternalServiceStrategy<string>();
            Assert.ThrowsException<ArgumentNullException>(() => OperationStrategy<string>.Fail(message, strategy));
        }

        /// <summary>
        /// Tests that an UnexpectedError failure can be created with a non-null error message and verifies the error type.
        /// </summary>
        [TestMethod]
        public void Fail_WhenUnexpectedError_ShouldReturnExpectedResult()
        {
            // Arrange
            var message = "TestValue2015692524";
            // Act
            var strategy = new UnexpectedErrorStrategy<string>();
            var result = OperationStrategy<string>.Fail(message, strategy);
            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(message, result.Message);
            Assert.AreEqual("UNEXPECTED_ERROR", result.Error);
        }

        /// <summary>
        /// Verifies that passing a null message to the UnexpectedError failure throws an ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void Fail_WhenUnexpectedErrorWithNullMessage_ShouldThrowArgumentNullException()
        {
            // Arrange
            string message = null;
            // Act & Assert
            var strategy = new UnexpectedErrorStrategy<string>();
            Assert.ThrowsException<ArgumentNullException>(() => OperationStrategy<string>.Fail(message, strategy));
        }

        /// <summary>
        /// Tests that a NetworkError failure can be created with a non-null error message and verifies the error type.
        /// </summary>
        [TestMethod]
        public void Fail_WhenNetworkError_ShouldReturnExpectedResult()
        {
            // Arrange
            var message = "TestValue2053465";
            // Act
            var strategy = new NetworkErrorStrategy<string>();
            var result = OperationStrategy<string>.Fail(message, strategy);
            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(message, result.Message);
            Assert.AreEqual("NETWORK_ERROR", result.Error);
        }

        /// <summary>
        /// Verifies that passing a null message to the NetworkError failure throws an ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void Fail_WhenNetworkErrorWithNullMessage_ShouldThrowArgumentNullException()
        {
            // Arrange
            string message = null;
            // Act & Assert
            var strategy = new NetworkErrorStrategy<string>();
            Assert.ThrowsException<ArgumentNullException>(() => OperationStrategy<string>.Fail(message, strategy));
        }

        /// <summary>
        /// Tests that the BusinessValidation failure includes the expected error message and error type.
        /// </summary>
        [TestMethod]
        public void BusinessValidationError_ShouldIncludeExpectedErrorMessageAndErrorType()
        {
            // Arrange
            var expectedMessage = "Business validation failed.";
            // Act
            var business = new BusinessStrategy<string>();
            var result = OperationStrategy<string>.Fail(expectedMessage, business);
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("BUSINESS_VALIDATION_ERROR", result.Error);
            Assert.AreEqual(expectedMessage, result.Message);
            Assert.IsFalse(result.IsSuccessful);
        }
    }
}