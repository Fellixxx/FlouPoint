namespace FlouPoint.Test.Infrastructure.Repository
{
    using FluentAssertions;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using global::Application.Result.Exceptions;

    [TestFixture]
    public class InvalidOperationResultExceptionTests
    {
        [Test]
        public Task When_CreateException_ValidMessage_Then_Success()
        {
            // Given
            var expectedMessage = "Test Exception Message";

            // When
            InvalidOperationResultException exception = new InvalidOperationResultException(expectedMessage);
            var actualMessage = exception.Message;

            // Then
            actualMessage.Should().Be(expectedMessage);
            return Task.CompletedTask;
        }
    }
}
