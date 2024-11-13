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
        public Task When_CreateException_ValidMessage_Then_Success()
        {
            // Arrange
            var expectedMessage = "Test Exception Message";
            // Act
            InvalidOperationResultException exception = new InvalidOperationResultException(expectedMessage);
            var actualMessage = exception.Message;
            // Assert
            actualMessage.Should().Be(expectedMessage);
            // Return a completed task as NUnit supports asynchronous testing
            return Task.CompletedTask;
        }
    }
}