namespace Application.Test.Result
{
    using System;
    using Application.Result;
    using Application.Result.Error;
    using Domain.EnumType;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using T = System.String;

    [TestClass]
    public class OperationBuilderTests
    {
        [TestMethod]
        public void CanCallFailureBusinessValidation()
        {
            // Arrange
            var message = "TestValue879633508";

            // Act
            var result = OperationBuilder<string>.FailureBusinessValidation(message);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(message, result.Message);
            Assert.AreEqual("BUSINESS_VALIDATION_ERROR", result.Error);
        }

        [TestMethod]
        public void CannotCallFailureBusinessValidationWithNullMessage()
        {
            // Arrange
            string message = null;

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => OperationBuilder<string>.FailureBusinessValidation(message));
        }

        [TestMethod]
        public void CanCallFailureConfigurationMissingError()
        {
            // Arrange
            var message = "TestValue826283408";

            // Act
            var result = OperationBuilder<string>.FailureConfigurationMissingError(message);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(message, result.Message);
            Assert.AreEqual("CONFIGURATION_MISSING_ERROR", result.Error);
        }

        [TestMethod]
        public void CannotCallFailureConfigurationMissingErrorWithNullMessage()
        {
            // Arrange
            string message = null;

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => OperationBuilder<string>.FailureConfigurationMissingError(message));
        }

        [TestMethod]
        public void CanCallFailureDatabase()
        {
            // Arrange
            var message = "TestValue1736562805";

            // Act
            var result = OperationBuilder<string>.FailureDatabase(message);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(message, result.Message);
            Assert.AreEqual("DATABASE_ERROR", result.Error);
        }

        [TestMethod]
        public void CannotCallFailureDatabaseWithNullMessage()
        {
            // Arrange
            string message = null;

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => OperationBuilder<string>.FailureDatabase(message));
        }

        [TestMethod]
        public void CanCallFailureDataSubmittedInvalid()
        {
            // Arrange
            var message = "TestValue1393732451";

            // Act
            var result = OperationBuilder<string>.FailureDataSubmittedInvalid(message);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(message, result.Message);
            Assert.AreEqual("DATA_SUBMITTED_INVALID", result.Error);
        }

        [TestMethod]
        public void CannotCallFailureDataSubmittedInvalidWithNullMessage()
        {
            // Arrange
            string message = null;

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => OperationBuilder<string>.FailureDataSubmittedInvalid(message));
        }

        [TestMethod]
        public void CanCallFailureExternalService()
        {
            // Arrange
            var message = "TestValue1769803281";

            // Act
            var result = OperationBuilder<string>.FailureExtenalService(message);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual("EXTERNAL_SERVICES_ERROR", result.Error);
        }

        [TestMethod]
        public void CannotCallFailureExternalServiceWithNullMessage()
        {
            // Arrange
            string message = null;

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => OperationBuilder<string>.FailureExtenalService(message));
        }

        [TestMethod]
        public void CanCallFailureUnexpectedError()
        {
            // Arrange
            var message = "TestValue2015692524";

            // Act
            var result = OperationBuilder<string>.FailureUnexpectedError(message);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(message, result.Message);
            Assert.AreEqual("UNEXPECTED_ERROR", result.Error);
        }

        [TestMethod]
        public void CannotCallFailureUnexpectedErrorWithNullMessage()
        {
            // Arrange
            string message = null;

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => OperationBuilder<string>.FailureUnexpectedError(message));
        }

        [TestMethod]
        public void CanCallFailureNetworkError()
        {
            // Arrange
            var message = "TestValue2053465";

            // Act
            var result = OperationBuilder<string>.FailureNetworkError(message);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(message, result.Message);
            Assert.AreEqual("NETWORK_ERROR", result.Error);
        }

        [TestMethod]
        public void CannotCallFailureNetworkErrorWithNullMessage()
        {
            // Arrange
            string message = null;

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => OperationBuilder<string>.FailureNetworkError(message));
        }
        [TestMethod]
        public void FailureBusinessValidation_Should_Return_OperationResult_With_BusinessValidationError()
        {
            // Arrange
            var expectedMessage = "Business validation failed.";

            // Act
            var result = OperationBuilder<string>.FailureBusinessValidation(expectedMessage);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("BUSINESS_VALIDATION_ERROR", result.Error);
            Assert.AreEqual("Business validation failed.", result.Message);
            Assert.IsFalse(result.IsSuccessful);
        }

        [TestMethod]
        public void FailureBusinessValidation_Should_Throw_ArgumentNullException_When_Message_Is_Null()
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => OperationBuilder<string>.FailureBusinessValidation(null));
        }

        [TestMethod]
        public void FailureConfigurationMissingError_Should_Return_OperationResult_With_ConfigurationMissingError()
        {
            // Arrange
            var expectedMessage = "Configuration is missing.";

            // Act
            var result = OperationBuilder<string>.FailureConfigurationMissingError(expectedMessage);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("CONFIGURATION_MISSING_ERROR", result.Error);
            Assert.AreEqual("Configuration is missing.", result.Message);
            Assert.IsFalse(result.IsSuccessful);
        }

        [TestMethod]
        public void FailureDatabase_Should_Return_OperationResult_With_DatabaseError()
        {
            // Arrange
            var expectedMessage = "Database failure occurred.";

            // Act
            var result = OperationBuilder<string>.FailureDatabase(expectedMessage);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("DATABASE_ERROR", result.Error);
            Assert.AreEqual("Database failure occurred.", result.Message);
            Assert.IsFalse(result.IsSuccessful);
        }

        [TestMethod]
        public void FailureDataSubmittedInvalid_Should_Return_OperationResult_With_DataSubmittedInvalidError()
        {
            // Arrange
            var expectedMessage = "Data submitted is invalid.";

            // Act
            var result = OperationBuilder<string>.FailureDataSubmittedInvalid(expectedMessage);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("DATA_SUBMITTED_INVALID", result.Error);
            Assert.AreEqual("Data submitted is invalid.", result.Message);
            Assert.IsFalse(result.IsSuccessful);
        }

        [TestMethod]
        public void FailureExternalService_Should_Return_OperationResult_With_ExternalServicesError()
        {
            // Arrange
            var expectedMessage = "External service failed.";

            // Act
            var result = OperationBuilder<string>.FailureExtenalService(expectedMessage);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("EXTERNAL_SERVICES_ERROR", result.Error);
            Assert.AreEqual("External service failed.", result.Message);
            Assert.IsFalse(result.IsSuccessful);
        }

        [TestMethod]
        public void FailureUnexpectedError_Should_Return_OperationResult_With_UnexpectedError()
        {
            // Arrange
            var expectedMessage = "An unexpected error occurred.";

            // Act
            var result = OperationBuilder<string>.FailureUnexpectedError(expectedMessage);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedMessage, result.Message);
            Assert.IsNull(result.Data);
            Assert.AreEqual("UNEXPECTED_ERROR", result.Error);
            Assert.IsFalse(result.IsSuccessful);
        }

        [TestMethod]
        public void FailureNetworkError_Should_Return_OperationResult_With_NetworkError()
        {
            // Arrange
            var expectedMessage = "Network error occurred.";

            // Act
            var result = OperationBuilder<string>.FailureNetworkError(expectedMessage);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("NETWORK_ERROR", result.Error);
            Assert.AreEqual("Network error occurred.", result.Message);
            Assert.IsFalse(result.IsSuccessful);
        }

        [TestMethod]
        public void CannotCallFailureBusinessValidationWithEmptyMessage()
        {
            // Arrange
            string message = "";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => OperationBuilder<string>.FailureBusinessValidation(message));
        }

        [TestMethod]
        public void CannotCallFailureConfigurationMissingErrorWithEmptyMessage()
        {
            // Arrange
            string message = "";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => OperationBuilder<string>.FailureConfigurationMissingError(message));
        }

        [TestMethod]
        public void CannotCallFailureDatabaseWithEmptyMessage()
        {
            // Arrange
            string message = "";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => OperationBuilder<string>.FailureDatabase(message));
        }

        [TestMethod]
        public void CannotCallFailureDataSubmittedInvalidWithEmptyMessage()
        {
            // Arrange
            string message = "";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => OperationBuilder<string>.FailureDataSubmittedInvalid(message));
        }

        [TestMethod]
        public void CannotCallFailureExternalServiceWithEmptyMessage()
        {
            // Arrange
            string message = "";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => OperationBuilder<string>.FailureExtenalService(message));
        }

        [TestMethod]
        public void CannotCallFailureUnexpectedErrorWithEmptyMessage()
        {
            // Arrange
            string message = "";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => OperationBuilder<string>.FailureUnexpectedError(message));
        }

        [TestMethod]
        public void CannotCallFailureNetworkErrorWithEmptyMessage()
        {
            // Arrange
            string message = "";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => OperationBuilder<string>.FailureNetworkError(message));
        }

        // Additional tests for whitespace message
        [TestMethod]
        public void CannotCallFailureBusinessValidationWithWhitespaceMessage()
        {
            // Arrange
            string message = "   ";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => OperationBuilder<string>.FailureBusinessValidation(message));
        }

        [TestMethod]
        public void CannotCallFailureConfigurationMissingErrorWithWhitespaceMessage()
        {
            // Arrange
            string message = "   ";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => OperationBuilder<string>.FailureConfigurationMissingError(message));
        }

        [TestMethod]
        public void CannotCallFailureDatabaseWithWhitespaceMessage()
        {
            // Arrange
            string message = "   ";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => OperationBuilder<string>.FailureDatabase(message));
        }

        [TestMethod]
        public void CannotCallFailureDataSubmittedInvalidWithWhitespaceMessage()
        {
            // Arrange
            string message = "   ";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => OperationBuilder<string>.FailureDataSubmittedInvalid(message));
        }

        [TestMethod]
        public void CannotCallFailureExternalServiceWithWhitespaceMessage()
        {
            // Arrange
            string message = "   ";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => OperationBuilder<string>.FailureExtenalService(message));
        }

        [TestMethod]
        public void CannotCallFailureUnexpectedErrorWithWhitespaceMessage()
        {
            // Arrange
            string message = "   ";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => OperationBuilder<string>.FailureUnexpectedError(message));
        }

        [TestMethod]
        public void CannotCallFailureNetworkErrorWithWhitespaceMessage()
        {
            // Arrange
            string message = "   ";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => OperationBuilder<string>.FailureNetworkError(message));
        }

        // Additional assertions for Data field being null
        [TestMethod]
        public void FailureBusinessValidation_Should_Have_Null_Data()
        {
            // Arrange
            var message = "Business validation failed.";

            // Act
            var result = OperationBuilder<string>.FailureBusinessValidation(message);

            // Assert
            Assert.IsNull(result.Data);
        }

        [TestMethod]
        public void FailureConfigurationMissingError_Should_Have_Null_Data()
        {
            // Arrange
            var message = "Configuration is missing.";

            // Act
            var result = OperationBuilder<string>.FailureConfigurationMissingError(message);

            // Assert
            Assert.IsNull(result.Data);
        }

        [TestMethod]
        public void FailureDatabase_Should_Have_Null_Data()
        {
            // Arrange
            var message = "Database failure occurred.";

            // Act
            var result = OperationBuilder<string>.FailureDatabase(message);

            // Assert
            Assert.IsNull(result.Data);
        }

        [TestMethod]
        public void FailureDataSubmittedInvalid_Should_Have_Null_Data()
        {
            // Arrange
            var message = "Data submitted is invalid.";

            // Act
            var result = OperationBuilder<string>.FailureDataSubmittedInvalid(message);

            // Assert
            Assert.IsNull(result.Data);
        }

        [TestMethod]
        public void FailureExternalService_Should_Have_Null_Data()
        {
            // Arrange
            var message = "External service failed.";

            // Act
            var result = OperationBuilder<string>.FailureExtenalService(message);

            // Assert
            Assert.IsNull(result.Data);
        }

        [TestMethod]
        public void FailureUnexpectedError_Should_Have_Null_Data()
        {
            // Arrange
            var message = "An unexpected error occurred.";

            // Act
            var result = OperationBuilder<string>.FailureUnexpectedError(message);

            // Assert
            Assert.IsNull(result.Data);
        }

        [TestMethod]
        public void FailureNetworkError_Should_Have_Null_Data()
        {
            // Arrange
            var message = "Network error occurred.";

            // Act
            var result = OperationBuilder<string>.FailureNetworkError(message);

            // Assert
            Assert.IsNull(result.Data);
        }

    }
}