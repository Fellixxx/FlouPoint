namespace Application.Test.Result.Exceptions
{
    using System;
    using Application.Result.Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class InvalidOperationResultExceptionTests
    {
        private InvalidOperationResultException _testClass;
        private string _message;

        [TestInitialize]
        public void SetUp()
        {
            _message="TestValue1658752453";
            _testClass=new InvalidOperationResultException(_message);
        }

        [TestMethod]
        public void CanConstruct()
        {
            // Act
            var instance = new InvalidOperationResultException(_message);

            // Assert
            Assert.IsNotNull(instance);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void CannotConstructWithInvalidMessage(string value)
        {
            Assert.ThrowsException<ArgumentNullException>(() => new InvalidOperationResultException(value));
        }

        [TestMethod]
        public void InvalidOperationResultException_Should_Contain_Provided_Message()
        {
            // Arrange
            var expectedMessage = "This is a custom error message for an invalid operation result.";

            // Act
            var exception = new InvalidOperationResultException(expectedMessage);

            // Assert
            Assert.IsNotNull(exception);
            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [TestMethod]
        public void InvalidOperationResultException_Should_Work_With_Throw_And_Catch()
        {
            // Arrange
            var expectedMessage = "This operation is invalid.";

            // Act & Assert
            var exception = Assert.ThrowsException<InvalidOperationResultException>(() =>
            {
                throw new InvalidOperationResultException(expectedMessage);
            });

            // Assert
            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [TestMethod]
        public void InvalidOperationResultException_Should_Inherit_From_Exception_Class()
        {
            // Arrange & Act
            var exception = new InvalidOperationResultException("Test inheritance");

            // Assert
            Assert.IsInstanceOfType(exception, typeof(Exception));
        }
    }
}