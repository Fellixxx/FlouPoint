namespace Application.Test.Result
{
    using System;
    using Application.Result;
    using Domain.Entities;
    using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class OperationBuilderTests
    {
        [TestMethod]
        public void CanCallFailureBusinessValidation()
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

        [TestMethod]
        public void CannotCallFailureBusinessValidationWithNullMessage()
        {
            // Arrange
            string message = null;

            // Act & Assert
            var business = new BusinessStrategy<string>();
            Assert.ThrowsException<ArgumentNullException>(() => OperationStrategy<string>.Fail(message, business));
        }

        [TestMethod]
        public void CanCallFailureConfigurationMissingError()
        {
            // Arrange
            var message = "TestValue826283408";

            // Act
            var result = OperationBuilder<string>.FailConfig(message);

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
            Assert.ThrowsException<ArgumentNullException>(() => OperationBuilder<string>.FailConfig(message));
        }

        [TestMethod]
        public void CanCallFailureDatabase()
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

        [TestMethod]
        public void CannotCallFailureDatabaseWithNullMessage()
        {
            // Arrange
            string message = null;

            // Act & Assert
            var strategy = new DatabaseStrategy<string>();
            Assert.ThrowsException<ArgumentNullException>(() => OperationStrategy<string>.Fail(message, strategy));
        }

        [TestMethod]
        public void CanCallFailureDataSubmittedInvalid()
        {
            // Arrange
            var message = "TestValue1393732451";

            // Act
            var result = OperationBuilder<string>.FailInvalidData(message);

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
            Assert.ThrowsException<ArgumentNullException>(() => OperationBuilder<string>.FailInvalidData(message));
        }

        [TestMethod]
        public void CanCallFailureExternalService()
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

        [TestMethod]
        public void CannotCallFailureExternalServiceWithNullMessage()
        {
            // Arrange
            string message = null;

            // Act & Assert
            var strategy = new ExternalServiceStrategy<string>();
            Assert.ThrowsException<ArgumentNullException>(() => OperationStrategy<string>.Fail(message, strategy));
        }

        [TestMethod]
        public void CanCallFailureUnexpectedError()
        {
            // Arrange
            var message = "TestValue2015692524";

            // Act
            var result = OperationBuilder<string>.FailUnexpected(message);

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
            Assert.ThrowsException<ArgumentNullException>(() => OperationBuilder<string>.FailUnexpected(message));
        }

        [TestMethod]
        public void CanCallFailureNetworkError()
        {
            // Arrange
            var message = "TestValue2053465";

            // Act
            var result = OperationBuilder<string>.FailNetwork(message);

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
            Assert.ThrowsException<ArgumentNullException>(() => OperationBuilder<string>.FailNetwork(message));
        }
        [TestMethod]
        public void FailureBusinessValidation_Should_Return_OperationResult_With_BusinessValidationError()
        {
            // Arrange
            var expectedMessage = "Business validation failed.";

            // Act
            var business = new BusinessStrategy<string>();
            var result = OperationStrategy<string>.Fail(expectedMessage, business);

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
            var business = new BusinessStrategy<string>();
            Assert.ThrowsException<ArgumentNullException>(() => OperationStrategy<string>.Fail(null, business));
        }

        [TestMethod]
        public void FailureConfigurationMissingError_Should_Return_OperationResult_With_ConfigurationMissingError()
        {
            // Arrange
            var expectedMessage = "Configuration is missing.";

            // Act
            var result = OperationBuilder<string>.FailConfig(expectedMessage);

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
            var strategy = new DatabaseStrategy<string>();
            var result = OperationStrategy<string>.Fail(expectedMessage, strategy);

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
            var result = OperationBuilder<string>.FailInvalidData(expectedMessage);

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
            var strategy = new ExternalServiceStrategy<string>();
            var result = OperationStrategy<string>.Fail(expectedMessage, strategy);

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
            var result = OperationBuilder<string>.FailUnexpected(expectedMessage);

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
            var result = OperationBuilder<string>.FailNetwork(expectedMessage);

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
            var business = new BusinessStrategy<string>();
            Assert.ThrowsException<ArgumentException>(() => OperationStrategy<string>.Fail(message, business));
        }

        [TestMethod]
        public void CannotCallFailureConfigurationMissingErrorWithEmptyMessage()
        {
            // Arrange
            string message = "";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => OperationBuilder<string>.FailConfig(message));
        }

        [TestMethod]
        public void CannotCallFailureDatabaseWithEmptyMessage()
        {
            // Arrange
            string message = "";

            // Act & Assert
            var strategy = new DatabaseStrategy<string>();
            Assert.ThrowsException<ArgumentException>(() => OperationStrategy<string>.Fail(message, strategy));
        }

        [TestMethod]
        public void CannotCallFailureDataSubmittedInvalidWithEmptyMessage()
        {
            // Arrange
            string message = "";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => OperationBuilder<string>.FailInvalidData(message));
        }

        [TestMethod]
        public void CannotCallFailureExternalServiceWithEmptyMessage()
        {
            // Arrange
            string message = "";

            // Act & Assert
            var strategy = new ExternalServiceStrategy<string>();
            Assert.ThrowsException<ArgumentException>(() => OperationStrategy<string>.Fail(message, strategy));
        }

        [TestMethod]
        public void CannotCallFailureUnexpectedErrorWithEmptyMessage()
        {
            // Arrange
            string message = "";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => OperationBuilder<string>.FailUnexpected(message));
        }

        [TestMethod]
        public void CannotCallFailureNetworkErrorWithEmptyMessage()
        {
            // Arrange
            string message = "";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => OperationBuilder<string>.FailNetwork(message));
        }

        // Additional tests for whitespace message
        [TestMethod]
        public void CannotCallFailureBusinessValidationWithWhitespaceMessage()
        {
            // Arrange
            string message = "   ";

            // Act & Assert
            var business = new BusinessStrategy<string>();
            Assert.ThrowsException<ArgumentException>(() => OperationStrategy<string>.Fail(message, business));
        }

        [TestMethod]
        public void CannotCallFailureConfigurationMissingErrorWithWhitespaceMessage()
        {
            // Arrange
            string message = "   ";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => OperationBuilder<string>.FailConfig(message));
        }

        [TestMethod]
        public void CannotCallFailureDatabaseWithWhitespaceMessage()
        {
            // Arrange
            string message = "   ";

            // Act & Assert
            var strategy = new DatabaseStrategy<string>();
            Assert.ThrowsException<ArgumentException>(() => OperationStrategy<string>.Fail(message, strategy));
        }

        [TestMethod]
        public void CannotCallFailureDataSubmittedInvalidWithWhitespaceMessage()
        {
            // Arrange
            string message = "   ";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => OperationBuilder<string>.FailInvalidData(message));
        }

        [TestMethod]
        public void CannotCallFailureExternalServiceWithWhitespaceMessage()
        {
            // Arrange
            string message = "   ";

            // Act & Assert
            var strategy = new ExternalServiceStrategy<string>();
            Assert.ThrowsException<ArgumentException>(() => OperationStrategy<string>.Fail(message, strategy));
        }

        [TestMethod]
        public void CannotCallFailureUnexpectedErrorWithWhitespaceMessage()
        {
            // Arrange
            string message = "   ";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => OperationBuilder<string>.FailUnexpected(message));
        }

        [TestMethod]
        public void CannotCallFailureNetworkErrorWithWhitespaceMessage()
        {
            // Arrange
            string message = "   ";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => OperationBuilder<string>.FailNetwork(message));
        }

        // Additional assertions for Data field being null
        [TestMethod]
        public void FailureBusinessValidation_Should_Have_Null_Data()
        {
            // Arrange
            var message = "Business validation failed.";

            // Act
            var business = new BusinessStrategy<string>();
            var result = OperationStrategy<string>.Fail(message, business); 

            // Assert
            Assert.IsNull(result.Data);
        }

        [TestMethod]
        public void FailureConfigurationMissingError_Should_Have_Null_Data()
        {
            // Arrange
            var message = "Configuration is missing.";

            // Act
            var result = OperationBuilder<string>.FailConfig(message);

            // Assert
            Assert.IsNull(result.Data);
        }

        [TestMethod]
        public void FailureDatabase_Should_Have_Null_Data()
        {
            // Arrange
            var message = "Database failure occurred.";

            // Act
            var strategy = new DatabaseStrategy<string>();
            var result = OperationStrategy<string>.Fail(message, strategy);

            // Assert
            Assert.IsNull(result.Data);
        }

        [TestMethod]
        public void FailureDataSubmittedInvalid_Should_Have_Null_Data()
        {
            // Arrange
            var message = "Data submitted is invalid.";

            // Act
            var result = OperationBuilder<string>.FailInvalidData(message);

            // Assert
            Assert.IsNull(result.Data);
        }

        [TestMethod]
        public void FailureExternalService_Should_Have_Null_Data()
        {
            // Arrange
            var message = "External service failed.";

            // Act
            var strategy = new ExternalServiceStrategy<string>();
            var result = OperationStrategy<string>.Fail(message, strategy);

            // Assert
            Assert.IsNull(result.Data);
        }

        [TestMethod]
        public void FailureUnexpectedError_Should_Have_Null_Data()
        {
            // Arrange
            var message = "An unexpected error occurred.";

            // Act
            var result = OperationBuilder<string>.FailUnexpected(message);

            // Assert
            Assert.IsNull(result.Data);
        }

        [TestMethod]
        public void FailureNetworkError_Should_Have_Null_Data()
        {
            // Arrange
            var message = "Network error occurred.";

            // Act
            var result = OperationBuilder<string>.FailNetwork(message);

            // Assert
            Assert.IsNull(result.Data);
        }

    }
}