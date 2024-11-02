namespace Application.Test.Result
{
    using System;
    using Application.Result;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using T = System.String;

    [TestClass]
    public class OperationBuilder_1Tests
    {
        [TestMethod]
        public void CanCallFailureBusinessValidation()
        {
            // Arrange
            var message = "TestValue879633508";

            // Act
            var result = OperationBuilder<T>.FailureBusinessValidation(message);

            // Assert
            Assert.Fail("Create or modify test");
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("   ")]
        public void CannotCallFailureBusinessValidationWithInvalidMessage(string value)
        {
            Assert.ThrowsException<ArgumentNullException>(() => OperationBuilder<T>.FailureBusinessValidation(value));
        }

        [TestMethod]
        public void CanCallFailureConfigurationMissingError()
        {
            // Arrange
            var message = "TestValue826283408";

            // Act
            var result = OperationBuilder<T>.FailureConfigurationMissingError(message);

            // Assert
            Assert.Fail("Create or modify test");
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void CannotCallFailureConfigurationMissingErrorWithInvalidMessage(string value)
        {
            Assert.ThrowsException<ArgumentNullException>(() => OperationBuilder<T>.FailureConfigurationMissingError(value));
        }

        [TestMethod]
        public void CanCallFailureDatabase()
        {
            // Arrange
            var message = "TestValue1736562805";

            // Act
            var result = OperationBuilder<T>.FailureDatabase(message);

            // Assert
            Assert.Fail("Create or modify test");
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void CannotCallFailureDatabaseWithInvalidMessage(string value)
        {
            Assert.ThrowsException<ArgumentNullException>(() => OperationBuilder<T>.FailureDatabase(value));
        }

        [TestMethod]
        public void CanCallFailureDataSubmittedInvalid()
        {
            // Arrange
            var message = "TestValue1393732451";

            // Act
            var result = OperationBuilder<T>.FailureDataSubmittedInvalid(message);

            // Assert
            Assert.Fail("Create or modify test");
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void CannotCallFailureDataSubmittedInvalidWithInvalidMessage(string value)
        {
            Assert.ThrowsException<ArgumentNullException>(() => OperationBuilder<T>.FailureDataSubmittedInvalid(value));
        }

        [TestMethod]
        public void CanCallFailureExtenalService()
        {
            // Arrange
            var message = "TestValue1769803281";

            // Act
            var result = OperationBuilder<T>.FailureExtenalService(message);

            // Assert
            Assert.Fail("Create or modify test");
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void CannotCallFailureExtenalServiceWithInvalidMessage(string value)
        {
            Assert.ThrowsException<ArgumentNullException>(() => OperationBuilder<T>.FailureExtenalService(value));
        }

        [TestMethod]
        public void CanCallFailureUnexpectedError()
        {
            // Arrange
            var message = "TestValue2015692524";

            // Act
            var result = OperationBuilder<T>.FailureUnexpectedError(message);

            // Assert
            Assert.Fail("Create or modify test");
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void CannotCallFailureUnexpectedErrorWithInvalidMessage(string value)
        {
            Assert.ThrowsException<ArgumentNullException>(() => OperationBuilder<T>.FailureUnexpectedError(value));
        }

        [TestMethod]
        public void CanCallFailureNetworkError()
        {
            // Arrange
            var message = "TestValue2053465";

            // Act
            var result = OperationBuilder<T>.FailureNetworkError(message);

            // Assert
            Assert.Fail("Create or modify test");
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void CannotCallFailureNetworkErrorWithInvalidMessage(string value)
        {
            Assert.ThrowsException<ArgumentNullException>(() => OperationBuilder<T>.FailureNetworkError(value));
        }
    }
}