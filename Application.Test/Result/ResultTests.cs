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
    [TestClass]
    public class ResultTests
    {
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

        [TestMethod]
        public void Result_Should_Initialize_With_Correct_Values()
        {
            // Arrange
            var isSuccess = true;
            var message = "Operation completed successfully.";
            var data = "TestData";
            var errorType = ErrorTypes.None;

            // Act
            var result = new TestResult(isSuccess, message, data, errorType);

            // Assert
            Assert.AreEqual(isSuccess, result.IsSuccessful);
            Assert.AreEqual(message, result.Message);
            Assert.AreEqual(data, result.Data);
        }

        [TestMethod]
        public void Result_Should_Return_Correct_Error_Custom_Name()
        {
            // Arrange
            var result = new TestResult(false, "An error occurred", "TestData", ErrorTypes.Database);

            // Act
            var errorName = result.Error;

            // Assert
            Assert.AreEqual("DATABASE_ERROR", errorName);
        }

        [TestMethod]
        public void Result_Should_Return_Default_Error_Custom_Name_When_No_Error()
        {
            // Arrange
            var result = new TestResult(true, "Operation completed successfully", "TestData", ErrorTypes.None);

            // Act
            var errorName = result.Error;

            // Assert
            Assert.AreEqual("NONE", errorName);
        }

        [TestMethod]
        public void Result_Should_Have_Null_Data_And_Message_When_Not_Set()
        {
            // Arrange
            var result = new TestResult(true, null, null, ErrorTypes.None);

            // Assert
            Assert.IsNull(result.Data);
            Assert.IsNull(result.Message);
            Assert.IsTrue(result.IsSuccessful);
        }

        [TestMethod]
        public void Result_Should_Indicate_Failure_With_ErrorType()
        {
            // Arrange
            var result = new TestResult(false, "An unexpected error occurred", null, ErrorTypes.Unexpected);

            // Assert
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual("UNEXPECTED_ERROR", result.Error);
            Assert.AreEqual("An unexpected error occurred", result.Message);
            Assert.IsNull(result.Data);
        }
    }
}
