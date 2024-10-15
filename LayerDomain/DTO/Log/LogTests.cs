using FluentAssertions;
using NUnit.Framework;


namespace LayerDomain.DTO.Log
{
    using Domain.DTO.Log;
    [TestFixture]
    public class LogTests
    {
        [Test]
        public void Log_Should_Store_Message_Property_Correctly()
        {
            // Arrange
            string expectedMessage = "This is a log message.";

            // Act
            var log = new Log
            {
                Message = expectedMessage
            };

            // Assert
            log.Message.Should().Be(expectedMessage);
        }

        [Test]
        public void Log_Should_Store_EntityName_Property_Correctly()
        {
            // Arrange
            string expectedEntityName = "Customer";

            // Act
            var log = new Log
            {
                EntityName = expectedEntityName
            };

            // Assert
            log.EntityName.Should().Be(expectedEntityName);
        }

        [Test]
        public void Log_Should_Store_EntityValue_Property_Correctly()
        {
            // Arrange
            string expectedEntityValue = "{ \"id\": 1, \"name\": \"John Doe\" }";

            // Act
            var log = new Log
            {
                EntityValue = expectedEntityValue
            };

            // Assert
            log.EntityValue.Should().Be(expectedEntityValue);
        }

        [Test]
        public void Log_Should_Store_Level_Property_Correctly()
        {
            // Arrange
            string expectedLevel = "Error";

            // Act
            var log = new Log
            {
                Level = expectedLevel
            };

            // Assert
            log.Level.Should().Be(expectedLevel);
        }

        [Test]
        public void Log_Should_Store_Operation_Property_Correctly()
        {
            // Arrange
            string expectedOperation = "Update";

            // Act
            var log = new Log
            {
                Operation = expectedOperation
            };

            // Assert
            log.Operation.Should().Be(expectedOperation);
        }

        [Test]
        public void Log_Should_Store_CreatedAt_Property_Correctly()
        {
            // Arrange
            DateTime expectedCreatedAt = DateTime.UtcNow;

            // Act
            var log = new Log
            {
                CreatedAt = expectedCreatedAt
            };

            // Assert
            log.CreatedAt.Should().Be(expectedCreatedAt);
        }

        [Test]
        public void Log_Should_Initialize_With_Null_Properties_By_Default()
        {
            // Act
            var log = new Log();

            // Assert
            log.Message.Should().BeNull();
            log.EntityName.Should().BeNull();
            log.EntityValue.Should().BeNull();
            log.Level.Should().BeNull();
            log.Operation.Should().BeNull();
        }
    }
}
