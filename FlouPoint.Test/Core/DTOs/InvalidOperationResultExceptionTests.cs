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
        /// Test to ensure that creating an InvalidOperationResultException with a valid
        /// message stores the message correctly in the exception instance.
        /// </summary>
        [Test]
        public void Constructor_WithValidMessage_ShouldStoreMessageCorrectly()
        {
            // Arrange
            var expectedMessage = "Test Exception Message";
            // Act
            var exception = new InvalidOperationResultException(expectedMessage);
            // Assert
            exception.Message.Should().Be(expectedMessage);
        }

        /// <summary>
        /// Test to ensure that creating an InvalidOperationResultException with a null
        /// message does not throw an error and sets the message in the exception as null.
        /// </summary>
        [Test]
        public void Constructor_WithNullMessage_ShouldHaveNullMessage()
        {
            // Act
            var exception = new InvalidOperationResultException(null);
            // Assert
            exception.Message.Should().BeNull();
        }
    // Add more tests here for different scenarios if applicable
    }
}