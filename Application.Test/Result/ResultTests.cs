using Application.Result.Error;
using Application.Result;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Test.Result
{
    /// <summary>
    /// This class contains unit tests for the Result class.
    /// </summary>
    [TestClass]
    public class ResultTests
    {
        /// <summary>
        /// Nested class representing a test result.
        /// </summary>
        private class TestResult : Result<string>
        {
            public TestResult(bool isSuccess, string message, string data, ErrorTypes errorType)
            {
                IsSuccessful = isSuccess;
                Message = message;
                Data = data;
                Type = errorType;
            }
        }

        /// <summary>
        /// Test method to verify initialization of Result with correct values.
        /// </summary>
        [TestMethod]
        public void Result_ShouldInitialize_WithCorrectValues()
        {
            // Arrange
            bool isSuccess = true;
            string message = "Operation completed successfully.";
            string data = "TestData";
            ErrorTypes errorType = ErrorTypes.None;
            // Act
            var result = new TestResult(isSuccess, message, data, errorType);
            // Assert
            Assert.AreEqual(isSuccess, result.IsSuccessful);
            Assert.AreEqual(message, result.Message);
            Assert.AreEqual(data, result.Data);
            Assert.AreEqual(errorType, result.Type);
        }

        /// <summary>
        /// Test method to verify custom error name for Result.
        /// </summary>
        [TestMethod]
        public void Result_ShouldReturn_CorrectErrorCustomName()
        {
            // Arrange
            var result = new TestResult(false, "An error occurred", "TestData", ErrorTypes.Database);
            // Act
            string errorName = result.Error;
            // Assert
            Assert.AreEqual("DATABASE_ERROR", errorName);
        }

        /// <summary>
        /// Test method to verify default error name for Result when no error occurs.
        /// </summary>
        [TestMethod]
        public void Result_ShouldReturn_DefaultErrorCustomName_WhenNoError()
        {
            // Arrange
            var result = new TestResult(true, "Operation completed successfully", "TestData", ErrorTypes.None);
            // Act
            string errorName = result.Error;
            // Assert
            Assert.AreEqual("NONE", errorName);
        }

        /// <summary>
        /// Test method to verify that Result has null data and message when not set.
        /// </summary>
        [TestMethod]
        public void Result_ShouldHave_NullDataAndMessage_WhenNotSet()
        {
            // Arrange
            var result = new TestResult(true, null, null, ErrorTypes.None);
            // Assert
            Assert.IsNull(result.Data);
            Assert.IsNull(result.Message);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("NONE", result.Error);
        }

        /// <summary>
        /// Test method to verify failure indication with error type for Result.
        /// </summary>
        [TestMethod]
        public void Result_ShouldIndicate_Failure_WithErrorType()
        {
            // Arrange
            var result = new TestResult(false, "An unexpected error occurred", null, ErrorTypes.Unexpected);
            // Assert
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual("UNEXPECTED_ERROR", result.Error);
            Assert.AreEqual("An unexpected error occurred", result.Message);
            Assert.IsNull(result.Data);
        }

        /// <summary>
        /// Test method to verify creation of Result with empty string message and data.
        /// </summary>
        [TestMethod]
        public void Result_Should_Handle_EmptyStringDataAndMessage()
        {
            // Arrange
            var result = new TestResult(true, "", "", ErrorTypes.None);
            // Assert
            Assert.AreEqual("", result.Data);
            Assert.AreEqual("", result.Message);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("NONE", result.Error);
        }

        /// <summary>
        /// Test method to verify the behavior for non-existing error types.
        /// </summary>
        [TestMethod]
        public void Result_Should_Handle_NonExistent_ErrorType()
        {
        // Note: This test would depend on how your application handles unknown error types.
        // Missing implementation details would be needed for this to work correctly.
        }
    }
}