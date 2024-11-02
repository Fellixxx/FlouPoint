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
            Assert.AreEqual(ErrorTypes.BusinessValidationError.GetDescription(), result.Error);
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
            Assert.AreEqual(ErrorTypes.ConfigurationMissingError.GetDescription(), result.Error);
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
            Assert.AreEqual(ErrorTypes.ConfigurationMissingError.GetDescription(), result.Error);
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
            Assert.AreEqual(ErrorTypes.ConfigurationMissingError.GetDescription(), result.Error);
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
            Assert.AreEqual(message, result.Error);
            Assert.AreEqual(ErrorTypes.ExternalServicesError.GetDescription(), result.Error);
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
            Assert.AreEqual(ErrorTypes.ConfigurationMissingError.GetDescription(), result.Error);
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
            Assert.AreEqual(ErrorTypes.ConfigurationMissingError.GetDescription(), result.Error);
        }

        [TestMethod]
        public void CannotCallFailureNetworkErrorWithNullMessage()
        {
            // Arrange
            string message = null;

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => OperationBuilder<string>.FailureNetworkError(message));
        }
    }
}