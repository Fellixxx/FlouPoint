namespace FlouPoint.Test.DTO.Log
{
    using NUnit.Framework;
    using FluentAssertions;
    using System;
    using Newtonsoft.Json;
    using Domain.DTO.Log;
    using Domain.Interfaces.Log;
    using global::Domain.Interfaces.Log;

    /// <summary>
    /// Contains tests for the Log class to ensure its properties and functionality work as expected.
    /// </summary>
    [TestFixture]
    public class LogTests
    {
        /// <summary>
        /// Tests that when properties of the Log instance are set, 
        /// they should return the same values they were set to.
        /// </summary>
        [Test]
        public void When_LogProperties_Are_Set_Then_They_Should_Return_Same_Values()
        {
            // Setup expected values
            var expectedMessage = "Test Message";
            var expectedEntityName = "TestEntity";
            var expectedEntityValue = "{ \"id\": 1, \"name\": \"Test\" }";
            var expectedLevel = "Info";
            var expectedOperation = "Create";
            var expectedCreatedAt = DateTime.UtcNow;
            // Create and configure a new Log instance
            var log = new Log
            {
                Message = expectedMessage,
                EntityName = expectedEntityName,
                EntityValue = expectedEntityValue,
                Level = expectedLevel,
                Operation = expectedOperation,
                CreatedAt = expectedCreatedAt
            };
            // Assert that properties were set correctly
            log.Message.Should().Be(expectedMessage);
            log.EntityName.Should().Be(expectedEntityName);
            log.EntityValue.Should().Be(expectedEntityValue);
            log.Level.Should().Be(expectedLevel);
            log.Operation.Should().Be(expectedOperation);
            log.CreatedAt.Should().Be(expectedCreatedAt);
        }

        /// <summary>
        /// Tests its default properties for a newly initialized Log instance 
        /// should be null or default.
        /// </summary>
        [Test]
        public void When_LogIsInitialized_Then_DefaultValuesAreNullOrDefault()
        {
            // Initialize a new Log instance
            var log = new Log();
            // Assert that all properties are null or default
            log.Message.Should().BeNull();
            log.EntityName.Should().BeNull();
            log.EntityValue.Should().BeNull();
            log.Level.Should().BeNull();
            log.Operation.Should().BeNull();
            log.CreatedAt.Should().Be(default(DateTime));
        }

        /// <summary>
        /// Tests that properties of a Log instance can accept null values.
        /// </summary>
        [Test]
        public void When_LogProperties_Are_Null_Then_Should_Accept_Null()
        {
            // Initialize a Log instance with null values
            var log = new Log
            {
                Message = null,
                EntityName = null,
                EntityValue = null,
                Level = null,
                Operation = null,
                CreatedAt = DateTime.UtcNow
            };
            // Assert that properties accept null values
            log.Message.Should().BeNull();
            log.EntityName.Should().BeNull();
            log.EntityValue.Should().BeNull();
            log.Level.Should().BeNull();
            log.Operation.Should().BeNull();
        }

        /// <summary>
        /// Tests that properties of a Log instance can accept empty strings.
        /// </summary>
        [Test]
        public void When_LogProperties_Are_EmptyStrings_Then_Should_Accept_EmptyStrings()
        {
            // Initialize a Log instance with empty strings
            var log = new Log
            {
                Message = string.Empty,
                EntityName = string.Empty,
                EntityValue = string.Empty,
                Level = string.Empty,
                Operation = string.Empty,
                CreatedAt = DateTime.UtcNow
            };
            // Assert that properties accept empty strings
            log.Message.Should().Be(string.Empty);
            log.EntityName.Should().Be(string.Empty);
            log.EntityValue.Should().Be(string.Empty);
            log.Level.Should().Be(string.Empty);
            log.Operation.Should().Be(string.Empty);
        }

        /// <summary>
        /// Tests that properties of a Log instance can handle very long strings.
        /// </summary>
        [Test]
        public void When_LogProperties_Have_VeryLongStrings_Then_Should_Handle_Correctly()
        {
            // Setup a long string
            var longString = new string ('a', 10000);
            // Initialize a Log instance with long strings
            var log = new Log
            {
                Message = longString,
                EntityName = longString,
                EntityValue = longString,
                Level = longString,
                Operation = longString,
                CreatedAt = DateTime.UtcNow
            };
            // Assert that properties handle long strings correctly
            log.Message.Should().Be(longString);
            log.EntityName.Should().Be(longString);
            log.EntityValue.Should().Be(longString);
            log.Level.Should().Be(longString);
            log.Operation.Should().Be(longString);
        }

        /// <summary>
        /// Tests that properties of a Log instance can handle special characters.
        /// </summary>
        [Test]
        public void When_LogProperties_Have_SpecialCharacters_Then_Should_Handle_Correctly()
        {
            // Setup a string with special characters
            var specialString = "特殊字符!@#$%^&*()_+|~=`{}[]:\";'<>?,./\\";
            // Initialize a Log instance with special characters
            var log = new Log
            {
                Message = specialString,
                EntityName = specialString,
                EntityValue = specialString,
                Level = specialString,
                Operation = specialString,
                CreatedAt = DateTime.UtcNow
            };
            // Assert that properties handle special characters correctly
            log.Message.Should().Be(specialString);
            log.EntityName.Should().Be(specialString);
            log.EntityValue.Should().Be(specialString);
            log.Level.Should().Be(specialString);
            log.Operation.Should().Be(specialString);
        }

        /// <summary>
        /// Tests that a Log can be correctly serialized to JSON and deserialized back.
        /// </summary>
        [Test]
        public void When_Log_Is_Serialized_To_Json_Then_Should_Deserialize_Correctly()
        {
            // Setup a Log instance
            var log = new Log
            {
                Message = "Test Message",
                EntityName = "TestEntity",
                EntityValue = "{ \"id\": 1, \"name\": \"Test\" }",
                Level = "Info",
                Operation = "Create",
                CreatedAt = DateTime.UtcNow
            };
            // Serialize to JSON and then deserialize back
            var json = JsonConvert.SerializeObject(log);
            var deserializedLog = JsonConvert.DeserializeObject<Log>(json);
            // Assert that the deserialized log is equivalent to the original
            deserializedLog.Should().BeEquivalentTo(log, options => options.WithStrictOrdering());
        }

        /// <summary>
        /// Tests that a Log instance implements the ILog interface and functions correctly.
        /// </summary>
        [Test]
        public void When_Log_Is_Assigned_To_ILog_Interface_Then_Should_Work_Correctly()
        {
            // Setup a Log instance via the ILog interface
            ILog logInterface = new Log
            {
                Message = "Interface Test",
                EntityName = "InterfaceEntity",
                EntityValue = "{}",
                Level = "Warning",
                Operation = "Update",
                CreatedAt = DateTime.UtcNow
            };
            // Assert that the interface properties were set correctly
            logInterface.Message.Should().Be("Interface Test");
            logInterface.EntityName.Should().Be("InterfaceEntity");
            logInterface.EntityValue.Should().Be("{}");
            logInterface.Level.Should().Be("Warning");
            logInterface.Operation.Should().Be("Update");
            logInterface.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        /// <summary>
        /// Tests that the CreatedAt property of an uninitialized Log instance is DateTime.MinValue.
        /// </summary>
        [Test]
        public void When_Log_CreatedAt_Is_DefaultValue_Then_Should_Be_MinValue()
        {
            // Initialize a new Log instance
            var log = new Log();
            // Assert that the CreatedAt property is DateTime.MinValue
            log.CreatedAt.Should().Be(DateTime.MinValue);
        }

        /// <summary>
        /// Tests that the CreatedAt property can be set to a date in the future.
        /// </summary>
        [Test]
        public void When_Log_CreatedAt_Is_Set_To_FutureDate_Then_Should_Accept_Value()
        {
            // Setup a future date
            var futureDate = DateTime.UtcNow.AddYears(1);
            // Initialize a Log instance with a future CreatedAt date
            var log = new Log
            {
                CreatedAt = futureDate
            };
            // Assert that the CreatedAt property is set to the future date
            log.CreatedAt.Should().Be(futureDate);
        }

        /// <summary>
        /// Tests that an exception is thrown when CreatedAt is set to an invalid date.
        /// </summary>
        [Test]
        public void When_Log_CreatedAt_Is_Set_To_InvalidDate_Then_Should_Throw_Exception()
        {
            // Initialize a new Log instance
            var log = new Log();
            // Action to set CreatedAt to an invalid date
            Action action = () => log.CreatedAt = DateTime.MaxValue.AddDays(1);
            // Assert that an exception is thrown
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        /// <summary>
        /// Tests that properties of a Log instance can handle whitespace strings.
        /// </summary>
        [Test]
        public void When_LogProperties_Have_Whitespace_Then_Should_Handle_Correctly()
        {
            // Setup a whitespace string
            var whitespaceString = "   ";
            // Initialize a Log instance with whitespace strings
            var log = new Log
            {
                Message = whitespaceString,
                EntityName = whitespaceString,
                EntityValue = whitespaceString,
                Level = whitespaceString,
                Operation = whitespaceString,
                CreatedAt = DateTime.UtcNow
            };
            // Assert that properties handle whitespace strings correctly
            log.Message.Should().Be(whitespaceString);
            log.EntityName.Should().Be(whitespaceString);
            log.EntityValue.Should().Be(whitespaceString);
            log.Level.Should().Be(whitespaceString);
            log.Operation.Should().Be(whitespaceString);
        }
    }
}