namespace FlouPoint.Application.Test.DTO.Log
{
    using NUnit.Framework;
    using FluentAssertions;
    using System;
    using Newtonsoft.Json;
    using Domain.DTO.Log;
    using Domain.Interfaces.Log;
    using global::Domain.Interfaces.Log;

    [TestFixture]
    public class LogTests
    {
        [Test]
        public void When_LogProperties_Are_Set_Then_They_Should_Return_Same_Values()
        {
            // Given
            var expectedMessage = "Test Message";
            var expectedEntityName = "TestEntity";
            var expectedEntityValue = "{ \"id\": 1, \"name\": \"Test\" }";
            var expectedLevel = "Info";
            var expectedOperation = "Create";
            var expectedCreatedAt = DateTime.UtcNow;

            // When
            var log = new Log
            {
                Message = expectedMessage,
                EntityName = expectedEntityName,
                EntityValue = expectedEntityValue,
                Level = expectedLevel,
                Operation = expectedOperation,
                CreatedAt = expectedCreatedAt
            };

            // Then
            log.Message.Should().Be(expectedMessage);
            log.EntityName.Should().Be(expectedEntityName);
            log.EntityValue.Should().Be(expectedEntityValue);
            log.Level.Should().Be(expectedLevel);
            log.Operation.Should().Be(expectedOperation);
            log.CreatedAt.Should().Be(expectedCreatedAt);
        }

        [Test]
        public void When_LogIsInitialized_Then_DefaultValuesAreNullOrDefault()
        {
            // When
            var log = new Log();

            // Then
            log.Message.Should().BeNull();
            log.EntityName.Should().BeNull();
            log.EntityValue.Should().BeNull();
            log.Level.Should().BeNull();
            log.Operation.Should().BeNull();
            log.CreatedAt.Should().Be(default(DateTime));
        }

        [Test]
        public void When_LogProperties_Are_Null_Then_Should_Accept_Null()
        {
            // Given
            var log = new Log
            {
                Message = null,
                EntityName = null,
                EntityValue = null,
                Level = null,
                Operation = null,
                CreatedAt = DateTime.UtcNow
            };

            // Then
            log.Message.Should().BeNull();
            log.EntityName.Should().BeNull();
            log.EntityValue.Should().BeNull();
            log.Level.Should().BeNull();
            log.Operation.Should().BeNull();
        }

        [Test]
        public void When_LogProperties_Are_EmptyStrings_Then_Should_Accept_EmptyStrings()
        {
            // Given
            var log = new Log
            {
                Message = string.Empty,
                EntityName = string.Empty,
                EntityValue = string.Empty,
                Level = string.Empty,
                Operation = string.Empty,
                CreatedAt = DateTime.UtcNow
            };

            // Then
            log.Message.Should().Be(string.Empty);
            log.EntityName.Should().Be(string.Empty);
            log.EntityValue.Should().Be(string.Empty);
            log.Level.Should().Be(string.Empty);
            log.Operation.Should().Be(string.Empty);
        }

        [Test]
        public void When_LogProperties_Have_VeryLongStrings_Then_Should_Handle_Correctly()
        {
            // Given
            var longString = new string('a', 10000);
            var log = new Log
            {
                Message = longString,
                EntityName = longString,
                EntityValue = longString,
                Level = longString,
                Operation = longString,
                CreatedAt = DateTime.UtcNow
            };

            // Then
            log.Message.Should().Be(longString);
            log.EntityName.Should().Be(longString);
            log.EntityValue.Should().Be(longString);
            log.Level.Should().Be(longString);
            log.Operation.Should().Be(longString);
        }

        [Test]
        public void When_LogProperties_Have_SpecialCharacters_Then_Should_Handle_Correctly()
        {
            // Given
            var specialString = "特殊字符!@#$%^&*()_+|~=`{}[]:\";'<>?,./\\";
            var log = new Log
            {
                Message = specialString,
                EntityName = specialString,
                EntityValue = specialString,
                Level = specialString,
                Operation = specialString,
                CreatedAt = DateTime.UtcNow
            };

            // Then
            log.Message.Should().Be(specialString);
            log.EntityName.Should().Be(specialString);
            log.EntityValue.Should().Be(specialString);
            log.Level.Should().Be(specialString);
            log.Operation.Should().Be(specialString);
        }

        [Test]
        public void When_Log_Is_Serialized_To_Json_Then_Should_Deserialize_Correctly()
        {
            // Given
            var log = new Log
            {
                Message = "Test Message",
                EntityName = "TestEntity",
                EntityValue = "{ \"id\": 1, \"name\": \"Test\" }",
                Level = "Info",
                Operation = "Create",
                CreatedAt = DateTime.UtcNow
            };

            // When
            var json = JsonConvert.SerializeObject(log);
            var deserializedLog = JsonConvert.DeserializeObject<Log>(json);

            // Then
            deserializedLog.Should().BeEquivalentTo(log, options => options.WithStrictOrdering());
        }

        [Test]
        public void When_Log_Is_Assigned_To_ILog_Interface_Then_Should_Work_Correctly()
        {
            // Given
            ILog logInterface = new Log
            {
                Message = "Interface Test",
                EntityName = "InterfaceEntity",
                EntityValue = "{}",
                Level = "Warning",
                Operation = "Update",
                CreatedAt = DateTime.UtcNow
            };

            // Then
            logInterface.Message.Should().Be("Interface Test");
            logInterface.EntityName.Should().Be("InterfaceEntity");
            logInterface.EntityValue.Should().Be("{}");
            logInterface.Level.Should().Be("Warning");
            logInterface.Operation.Should().Be("Update");
            logInterface.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Test]
        public void When_Log_CreatedAt_Is_DefaultValue_Then_Should_Be_MinValue()
        {
            // Given
            var log = new Log();

            // Then
            log.CreatedAt.Should().Be(DateTime.MinValue);
        }

        [Test]
        public void When_Log_CreatedAt_Is_Set_To_FutureDate_Then_Should_Accept_Value()
        {
            // Given
            var futureDate = DateTime.UtcNow.AddYears(1);
            var log = new Log
            {
                CreatedAt = futureDate
            };

            // Then
            log.CreatedAt.Should().Be(futureDate);
        }

        [Test]
        public void When_Log_CreatedAt_Is_Set_To_InvalidDate_Then_Should_Throw_Exception()
        {
            // Given
            var log = new Log();

            // When
            Action action = () => log.CreatedAt = DateTime.MaxValue.AddDays(1);

            // Then
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void When_LogProperties_Have_Whitespace_Then_Should_Handle_Correctly()
        {
            // Given
            var whitespaceString = "   ";
            var log = new Log
            {
                Message = whitespaceString,
                EntityName = whitespaceString,
                EntityValue = whitespaceString,
                Level = whitespaceString,
                Operation = whitespaceString,
                CreatedAt = DateTime.UtcNow
            };

            // Then
            log.Message.Should().Be(whitespaceString);
            log.EntityName.Should().Be(whitespaceString);
            log.EntityValue.Should().Be(whitespaceString);
            log.Level.Should().Be(whitespaceString);
            log.Operation.Should().Be(whitespaceString);
        }
    }
}
