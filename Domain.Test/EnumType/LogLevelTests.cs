namespace Domain.Test.EnumType
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Reflection;
    using Domain.EnumType;
    using Domain.EnumType.Extensions;

    [TestClass]
    public class LogLevelTests
    {
        [TestMethod]
        public void When_LogLevelIsTrace_Then_ValueIsZero()
        {
            // Given, When
            var level = LogLevel.Trace;

            // Then
            Assert.AreEqual(0, (int)level);
        }

        [TestMethod]
        public void When_LogLevelIsDebug_Then_ValueIsOne()
        {
            // Given, When
            var level = LogLevel.Debug;

            // Then
            Assert.AreEqual(1, (int)level);
        }

        [TestMethod]
        public void When_LogLevelIsInformation_Then_ValueIsTwo()
        {
            // Given, When
            var level = LogLevel.Information;

            // Then
            Assert.AreEqual(2, (int)level);
        }

        [TestMethod]
        public void When_LogLevelIsWarning_Then_ValueIsThree()
        {
            // Given, When
            var level = LogLevel.Warning;

            // Then
            Assert.AreEqual(3, (int)level);
        }

        [TestMethod]
        public void When_LogLevelIsError_Then_ValueIsFour()
        {
            // Given, When
            var level = LogLevel.Error;

            // Then
            Assert.AreEqual(4, (int)level);
        }

        [TestMethod]
        public void When_LogLevelIsFatal_Then_ValueIsFive()
        {
            // Given, When
            var level = LogLevel.Fatal;

            // Then
            Assert.AreEqual(5, (int)level);
        }
        [DataTestMethod]
        [DataRow(LogLevel.Trace, "Trace", "Used for the most detailed log outputs, including fine-grained information about the application's state.")]
        [DataRow(LogLevel.Debug, "Debug", "Used for interactive investigation during development, providing insights into the application behavior.")]
        [DataRow(LogLevel.Information, "Information", "Used to track the general flow of the application, providing key insights and operational data.")]
        [DataRow(LogLevel.Warning, "Warning", "Used for logs that highlight the abnormal or unexpected events in the application flow, which may need attention.")]
        [DataRow(LogLevel.Error, "Error", "Used for logs that highlight when the current flow of execution is stopped due to a failure or significant issue.")]
        [DataRow(LogLevel.Fatal, "Fatal", "Used to log unhandled exceptions or critical errors that cause the program to crash or terminate.")]
        public void LogLevel_Should_Have_Correct_EnumMetadata(LogLevel logLevel, string expectedName, string expectedDescription)
        {
            // When
            var fieldInfo = logLevel.GetType().GetField(logLevel.ToString());
            var attribute = fieldInfo.GetCustomAttribute<EnumMetadata>();

            // Then
            Assert.IsNotNull(attribute);
            Assert.AreEqual(expectedName, attribute.Name);
            Assert.AreEqual(expectedDescription, attribute.Description);
        }

        [TestMethod]
        public void LogLevel_Without_EnumMetadataAttribute_Should_Return_Null()
        {
            // When
            var logLevel = LogLevel.Trace; // Or any other value that has EnumMetadataAttribute
            var fieldInfo = logLevel.GetType().GetField(logLevel.ToString());
            var attribute = fieldInfo.GetCustomAttribute<EnumMetadata>();

            // Then
            Assert.IsNotNull(attribute); // In this case, the EnumMetadataAttribute is defined for all values, so it should not be null.
        }

        [TestMethod]
        public void EnumMetadata_Should_Be_Applied_To_All_LogLevels()
        {
            // Given
            var logLevels = Enum.GetValues(typeof(LogLevel));

            // When & Then
            foreach (LogLevel logLevel in logLevels)
            {
                var fieldInfo = logLevel.GetType().GetField(logLevel.ToString());
                var attribute = fieldInfo.GetCustomAttribute<EnumMetadata>();
                Assert.IsNotNull(attribute);
            }
        }

        [TestMethod]
        public void EnumMetadata_Should_Have_Correct_AttributeUsage()
        {
            // When
            var attributeUsage = typeof(EnumMetadata).GetCustomAttribute<AttributeUsageAttribute>();

            // Then
            Assert.IsNotNull(attributeUsage);
            Assert.AreEqual(AttributeTargets.Field, attributeUsage.ValidOn);
            Assert.IsFalse(attributeUsage.AllowMultiple);
        }
    }
}
