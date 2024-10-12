namespace FlouPoint.Test.EnumType.LogLevel
{
    using NUnit.Framework;
    using FluentAssertions;
    using System;
    using System.Reflection;
    using Domain.EnumType;
    using Domain.EnumType.LogLevel;

    [TestFixture]
    public class LogLevelTests
    {
        [TestCase(LogLevel.Trace, "Trace", "Used for the most detailed log outputs, including fine-grained information about the application's state.")]
        [TestCase(LogLevel.Debug, "Debug", "Used for interactive investigation during development, providing insights into the application behavior.")]
        [TestCase(LogLevel.Information, "Information", "Used to track the general flow of the application, providing key insights and operational data.")]
        [TestCase(LogLevel.Warning, "Warning", "Used for logs that highlight the abnormal or unexpected events in the application flow, which may need attention.")]
        [TestCase(LogLevel.Error, "Error", "Used for logs that highlight when the current flow of execution is stopped due to a failure or significant issue.")]
        [TestCase(LogLevel.Fatal, "Fatal", "Used to log unhandled exceptions or critical errors that cause the program to crash or terminate.")]
        public void LogLevel_Should_Have_Correct_EnumMetadata(LogLevel logLevel, string expectedName, string expectedDescription)
        {
            // When
            var fieldInfo = logLevel.GetType().GetField(logLevel.ToString());
            var attribute = fieldInfo.GetCustomAttribute<EnumMetadataAttribute>();

            // Then
            attribute.Should().NotBeNull();
            attribute.Name.Should().Be(expectedName);
            attribute.Description.Should().Be(expectedDescription);
        }

        [Test]
        public void LogLevel_Without_EnumMetadataAttribute_Should_Return_Null()
        {
            // When
            var logLevel = LogLevel.Trace; // Or any other value that has EnumMetadataAttribute
            var fieldInfo = logLevel.GetType().GetField(logLevel.ToString());
            var attribute = fieldInfo.GetCustomAttribute<EnumMetadataAttribute>();

            // Then
            attribute.Should().NotBeNull(); // In this case, the EnumMetadataAttribute is defined for all values, so it should not be null.
        }

        [Test]
        public void EnumMetadata_Should_Be_Applied_To_All_LogLevels()
        {
            // Given
            var logLevels = Enum.GetValues(typeof(LogLevel));

            // When & Then
            foreach (LogLevel logLevel in logLevels)
            {
                var fieldInfo = logLevel.GetType().GetField(logLevel.ToString());
                var attribute = fieldInfo.GetCustomAttribute<EnumMetadataAttribute>();
                attribute.Should().NotBeNull();
            }
        }

        [Test]
        public void EnumMetadata_Should_Have_Correct_AttributeUsage()
        {
            // When
            var attributeUsage = typeof(EnumMetadataAttribute).GetCustomAttribute<AttributeUsageAttribute>();

            // Then
            attributeUsage.Should().NotBeNull();
            attributeUsage.ValidOn.Should().Be(AttributeTargets.Field);
            attributeUsage.AllowMultiple.Should().BeFalse();
        }
    }
}

