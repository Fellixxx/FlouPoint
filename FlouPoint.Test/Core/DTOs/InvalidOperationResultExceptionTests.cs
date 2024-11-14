namespace FlouPoint.Test.Infrastructure.Repository
{
    using FluentAssertions;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using global::Application.Result.Exceptions;

    /// <summary>
    /// Unit test class for testing the InvalidOperationResultException.
    /// Validates that the exception can be created with a specified message,
    /// and that the message is correctly stored in the exception.
    /// </summary>
    [TestFixture]
    public class InvalidOperationResultExceptionTests
    {
        /// <summary>
        /// Test to ensure that an InvalidOperationResultException can be 
        /// instantiated with a valid message and that the message 
        /// is accurately retrieved from the exception.
        /// </summary>
        [Test]
        public void When_CreateException_WithValidMessage_Then_MessageIsStoredCorrectly()
        {
            // Arrange
            var expectedMessage = "Test Exception Message";
            // Act
            var exception = new InvalidOperationResultException(expectedMessage);
            // Assert
            exception.Message.Should().Be(expectedMessage);
        }

        /// <summary>
        /// Test to ensure that an InvalidOperationResultException can handle
        /// a null message without throwing an unexpected error.
        /// </summary>
        [Test]
        public void When_CreateException_WithNullMessage_Then_MessageIsNull()
        {
            // Act
            var exception = new InvalidOperationResultException(null);
            // Assert
            exception.Message.Should().BeNull();
        }
    // Add more tests here for different scenarios if applicable
    }
}