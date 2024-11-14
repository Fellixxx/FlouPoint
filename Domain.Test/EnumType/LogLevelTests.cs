namespace Domain.Test.EnumType
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Reflection;
    using Domain.EnumType;
    using Domain.EnumType.Extensions;

    /// <summary>
    /// A set of unit tests for verifying the LogLevel enumeration and its associated EnumMetadata.
    /// </summary>
    [TestClass]
    public class LogLevelTests
    {
        private const int TraceValue = 0;
        private const int DebugValue = 1;
        private const int InformationValue = 2;
        private const int WarningValue = 3;
        private const int ErrorValue = 4;
        private const int FatalValue = 5;
        /// <summary>
        /// Tests that each LogLevel matches the expected integer value.
        /// </summary>
        [DataTestMethod]
        [DataRow(LogLevel.Trace, TraceValue)]
        [DataRow(LogLevel.Debug, DebugValue)]
        [DataRow(LogLevel.Information, InformationValue)]
        [DataRow(LogLevel.Warning, WarningValue)]
        [DataRow(LogLevel.Error, ErrorValue)]
        [DataRow(LogLevel.Fatal, FatalValue)]
        public void LogLevel_Should_Have_Expected_Integer_Values(LogLevel logLevel, int expectedValue)
        {
            Assert.AreEqual(expectedValue, (int)logLevel, $"Expected {logLevel} to have value {expectedValue}");
        }

        /// <summary>
        /// Verifies that each LogLevel value has the correct EnumMetadata properties.
        /// </summary>
        [DataTestMethod]
        [DataRow(LogLevel.Trace, "Trace", "Used for the most detailed log outputs, including fine-grained information about the application's state.")]
        [DataRow(LogLevel.Debug, "Debug", "Used for interactive investigation during development, providing insights into the application behavior.")]
        [DataRow(LogLevel.Information, "Information", "Used to track the general flow of the application, providing key insights and operational data.")]
        [DataRow(LogLevel.Warning, "Warning", "Used for logs that highlight the abnormal or unexpected events in the application flow, which may need attention.")]
        [DataRow(LogLevel.Error, "Error", "Used for logs that highlight when the current flow of execution is stopped due to a failure or significant issue.")]
        [DataRow(LogLevel.Fatal, "Fatal", "Used to log unhandled exceptions or critical errors that cause the program to crash or terminate.")]
        public void LogLevel_EnumMetadata_Should_Match_Expected_Details(LogLevel logLevel, string expectedName, string expectedDescription)
        {
            var attribute = GetEnumMetadataAttribute(logLevel);
            Assert.IsNotNull(attribute, $"LogLevel {logLevel} should have an EnumMetadata attribute.");
            Assert.AreEqual(expectedName, attribute.Name, $"LogLevel {logLevel} should have name '{expectedName}'.");
            Assert.AreEqual(expectedDescription, attribute.Description, $"LogLevel {logLevel} should have description '{expectedDescription}'.");
        }

        /// <summary>
        /// Ensures each LogLevel value has an associated EnumMetadata attribute.
        /// </summary>
        [TestMethod]
        public void All_LogLevel_Values_Should_Have_Assigned_EnumMetadata()
        {
            foreach (LogLevel logLevel in Enum.GetValues(typeof(LogLevel)))
            {
                var attribute = GetEnumMetadataAttribute(logLevel);
                Assert.IsNotNull(attribute, $"LogLevel {logLevel} should have an EnumMetadata attribute.");
            }
        }

        /// <summary>
        /// Validates that the EnumMetadata attribute is properly used.
        /// </summary>
        [TestMethod]
        public void EnumMetadata_Attribute_Should_Have_Correct_Usage()
        {
            var attributeUsage = typeof(EnumMetadata).GetCustomAttribute<AttributeUsageAttribute>();
            Assert.IsNotNull(attributeUsage, "EnumMetadata should have an AttributeUsage attribute.");
            Assert.AreEqual(AttributeTargets.Field, attributeUsage.ValidOn, "EnumMetadata should be applicable only to fields.");
            Assert.IsFalse(attributeUsage.AllowMultiple, "EnumMetadata should not allow multiple instances.");
        }

        /// <summary>
        /// Retrieves the EnumMetadata attribute for a given LogLevel.
        /// </summary>
        /// <param name = "logLevel">The log level for which to retrieve the EnumMetadata.</param>
        /// <returns>The EnumMetadata attribute associated with the given log level.</returns>
        private EnumMetadata GetEnumMetadataAttribute(LogLevel logLevel)
        {
            var fieldInfo = logLevel.GetType().GetField(logLevel.ToString());
            return fieldInfo.GetCustomAttribute<EnumMetadata>();
        }
    }
}