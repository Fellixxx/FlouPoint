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
            _message = "TestValue1658752453";
            _testClass = new InvalidOperationResultException(_message);
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

        // Additional Tests

        [TestMethod]
        public void InvalidOperationResultException_InnerException_Should_Be_Null_By_Default()
        {
            // Arrange
            var message = "Test message";

            // Act
            var exception = new InvalidOperationResultException(message);

            // Assert
            Assert.IsNull(exception.InnerException);
        }

        [TestMethod]
        public void InvalidOperationResultException_Data_Should_Be_Empty_By_Default()
        {
            // Arrange
            var exception = new InvalidOperationResultException("Test message");

            // Act & Assert
            Assert.AreEqual(0, exception.Data.Count);
        }

        [TestMethod]
        public void InvalidOperationResultException_Can_Add_Data()
        {
            // Arrange
            var exception = new InvalidOperationResultException("Test message");

            // Act
            exception.Data["ErrorCode"] = 404;

            // Assert
            Assert.AreEqual(1, exception.Data.Count);
            Assert.AreEqual(404, exception.Data["ErrorCode"]);
        }

        [TestMethod]
        public void InvalidOperationResultException_Can_Set_And_Get_HelpLink()
        {
            // Arrange
            var exception = new InvalidOperationResultException("Test message");
            var helpLink = "http://example.com/help";

            // Act
            exception.HelpLink = helpLink;

            // Assert
            Assert.AreEqual(helpLink, exception.HelpLink);
        }

        [TestMethod]
        public void InvalidOperationResultException_ToString_Should_Contain_Message()
        {
            // Arrange
            var message = "Test message";
            var exception = new InvalidOperationResultException(message);

            // Act
            var result = exception.ToString();

            // Assert
            Assert.IsTrue(result.Contains(message));
        }

        [TestMethod]
        public void InvalidOperationResultException_StackTrace_Should_Be_Populated_When_Thrown()
        {
            // Arrange
            var message = "Test message";
            InvalidOperationResultException exception = null;

            // Act
            try
            {
                throw new InvalidOperationResultException(message);
            }
            catch (InvalidOperationResultException ex)
            {
                exception = ex;
            }

            // Assert
            Assert.IsNotNull(exception);
            Assert.IsNotNull(exception.StackTrace);
        }


        [TestMethod]
        public void InvalidOperationResultException_Should_Not_Inherit_From_SystemException()
        {
            // Arrange & Act
            var exception = new InvalidOperationResultException("Test message");

            // Assert
            Assert.IsNotInstanceOfType(exception, typeof(SystemException));
        }

        [TestMethod]
        public void InvalidOperationResultException_HResult_Should_Have_Default_Value()
        {
            // Arrange
            var exception = new InvalidOperationResultException("Test message");

            // Act
            var hResult = exception.HResult;

            // Assert
            Assert.AreEqual(-2146233088, hResult); // Default HResult for exceptions
        }

        [TestMethod]
        public void InvalidOperationResultException_Can_Handle_Long_Message()
        {
            // Arrange
            var longMessage = new string('a', 10000);

            // Act
            var exception = new InvalidOperationResultException(longMessage);

            // Assert
            Assert.AreEqual(longMessage, exception.Message);
        }
    }
}