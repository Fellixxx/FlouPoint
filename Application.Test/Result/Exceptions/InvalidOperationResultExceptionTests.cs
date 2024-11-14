namespace Application.Test.Result.Exceptions
{
    using System;
    using Application.Result.Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Provides unit tests for the <see cref = "InvalidOperation"/> class, which represents
    /// exceptions that occur when an invalid operation results in an error.
    /// </summary>
    [TestClass]
    public class InvalidOperationResultExceptionTests
    {
        private InvalidOperation _testClass;
        private string _message;
        /// <summary>
        /// Initializes test resources before each test method runs.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            _message = "TestValue1658752453";
            _testClass = new InvalidOperation(_message);
        }

        /// <summary>
        /// Tests the construction of the <see cref = "InvalidOperation"/> instance.
        /// </summary>
        [TestMethod]
        public void Constructor_Initializes_Instance()
        {
            // Act
            var instance = new InvalidOperation(_message);
            // Assert
            Assert.IsNotNull(instance);
        }

        /// <summary>
        /// Tests that constructing an <see cref = "InvalidOperation"/> with an invalid 
        /// message throws an <see cref = "ArgumentNullException"/>.
        /// </summary>
        /// <param name = "value">The invalid message value.</param>
        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void Constructor_Throws_ArgumentNullException_For_Invalid_Message(string value)
        {
            Assert.ThrowsException<ArgumentNullException>(() => new InvalidOperation(value));
        }

        /// <summary>
        /// Verifies the exception message is correctly set and retrieved.
        /// </summary>
        [TestMethod]
        public void Message_Should_Match_Provided_Value()
        {
            // Arrange
            var expectedMessage = "This is a custom error message for an invalid operation result.";
            // Act
            var exception = new InvalidOperation(expectedMessage);
            // Assert
            Assert.AreEqual(expectedMessage, exception.Message);
        }

        /// <summary>
        /// Validates that the exception can be thrown and caught successfully.
        /// </summary>
        [TestMethod]
        public void Exception_Can_Be_Thrown_And_Caught()
        {
            // Arrange
            var expectedMessage = "This operation is invalid.";
            // Act & Assert
            var exception = Assert.ThrowsException<InvalidOperation>(() =>
            {
                throw new InvalidOperation(expectedMessage);
            });
            // Assert
            Assert.AreEqual(expectedMessage, exception.Message);
        }

        /// <summary>
        /// Validates inheritance from the <see cref = "Exception"/> class.
        /// </summary>
        [TestMethod]
        public void Inherits_From_Exception()
        {
            // Arrange & Act
            var exception = new InvalidOperation("Test inheritance");
            // Assert
            Assert.IsInstanceOfType(exception, typeof(Exception));
        }

        /// <summary>
        /// Checks that the default inner exception is null.
        /// </summary>
        [TestMethod]
        public void InnerException_Should_Be_Null_By_Default()
        {
            // Arrange
            var message = "Test message";
            // Act
            var exception = new InvalidOperation(message);
            // Assert
            Assert.IsNull(exception.InnerException);
        }

        /// <summary>
        /// Checks the data dictionary is initially empty.
        /// </summary>
        [TestMethod]
        public void Data_Should_Be_Empty_By_Default()
        {
            // Arrange
            var exception = new InvalidOperation("Test message");
            // Act & Assert
            Assert.AreEqual(0, exception.Data.Count);
        }

        /// <summary>
        /// Tests adding key-value pairs to the data dictionary.
        /// </summary>
        [TestMethod]
        public void Can_Add_Data()
        {
            // Arrange
            var exception = new InvalidOperation("Test message");
            // Act
            exception.Data["ErrorCode"] = 404;
            // Assert
            Assert.AreEqual(1, exception.Data.Count);
            Assert.AreEqual(404, exception.Data["ErrorCode"]);
        }

        /// <summary>
        /// Verifies setting and getting the HelpLink property.
        /// </summary>
        [TestMethod]
        public void Can_Set_And_Get_HelpLink()
        {
            // Arrange
            var exception = new InvalidOperation("Test message");
            var helpLink = "http://example.com/help";
            // Act
            exception.HelpLink = helpLink;
            // Assert
            Assert.AreEqual(helpLink, exception.HelpLink);
        }

        /// <summary>
        /// Ensures the ToString() method includes the exception message.
        /// </summary>
        [TestMethod]
        public void ToString_Should_Contain_Message()
        {
            // Arrange
            var message = "Test message";
            var exception = new InvalidOperation(message);
            // Act
            var result = exception.ToString();
            // Assert
            Assert.IsTrue(result.Contains(message));
        }

        /// <summary>
        /// Validates proper stack trace population when thrown.
        /// </summary>
        [TestMethod]
        public void StackTrace_Should_Be_Populated_When_Thrown()
        {
            // Arrange
            var message = "Test message";
            InvalidOperation exception = null;
            // Act
            try
            {
                throw new InvalidOperation(message);
            }
            catch (InvalidOperation ex)
            {
                exception = ex;
            }

            // Assert
            Assert.IsNotNull(exception);
            Assert.IsNotNull(exception.StackTrace);
        }

        /// <summary>
        /// Verifies the exception does not inherit from <see cref = "SystemException"/>.
        /// </summary>
        [TestMethod]
        public void Should_Not_Inherit_From_SystemException()
        {
            // Arrange & Act
            var exception = new InvalidOperation("Test message");
            // Assert
            Assert.IsNotInstanceOfType(exception, typeof(SystemException));
        }

        /// <summary>
        /// Tests the default HResult value.
        /// </summary>
        [TestMethod]
        public void HResult_Should_Have_Default_Value()
        {
            // Arrange
            var exception = new InvalidOperation("Test message");
            // Act
            var hResult = exception.HResult;
            // Assert
            Assert.AreEqual(-2146233088, hResult); // Default HResult for exceptions
        }

        /// <summary>
        /// Verifies handling of very long messages.
        /// </summary>
        [TestMethod]
        public void Can_Handle_Long_Message()
        {
            // Arrange
            var longMessage = new string ('a', 10000);
            // Act
            var exception = new InvalidOperation(longMessage);
            // Assert
            Assert.AreEqual(longMessage, exception.Message);
        }
    }
}