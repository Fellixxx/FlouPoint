namespace Domain.Test.DTO.Log
{
    using System;
    using Domain.DTO.Log;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LogTests
    {
        private Log _testClass;

        [TestInitialize]
        public void SetUp()
        {
            _testClass=new Log();
        }

        [TestMethod]
        public void CanSetAndGetMessage()
        {
            // Arrange
            var testValue = "TestValue262371514";

            // Act
            _testClass.Message=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.Message);
        }

        [TestMethod]
        public void CanSetAndGetEntityName()
        {
            // Arrange
            var testValue = "TestValue127567551";

            // Act
            _testClass.EntityName=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.EntityName);
        }

        [TestMethod]
        public void CanSetAndGetEntityValue()
        {
            // Arrange
            var testValue = "TestValue2108728596";

            // Act
            _testClass.EntityValue=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.EntityValue);
        }

        [TestMethod]
        public void CanSetAndGetLevel()
        {
            // Arrange
            var testValue = "TestValue1190128197";

            // Act
            _testClass.Level=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.Level);
        }

        [TestMethod]
        public void CanSetAndGetOperation()
        {
            // Arrange
            var testValue = "TestValue9811108";

            // Act
            _testClass.Operation=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.Operation);
        }

        [TestMethod]
        public void CanSetAndGetCreatedAt()
        {
            // Arrange
            var testValue = DateTime.UtcNow;

            // Act
            _testClass.CreatedAt=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.CreatedAt);
        }

        [TestMethod]
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
            Assert.AreEqual(log.Message, expectedMessage);
        }

        [TestMethod]
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
            Assert.AreEqual(log.EntityName, expectedEntityName);
        }

        [TestMethod]
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
            Assert.AreEqual(log.EntityValue, expectedEntityValue);
        }

        [TestMethod]
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
            Assert.AreEqual(log.Level, expectedLevel);
        }

        [TestMethod]
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
            Assert.AreEqual(log.Operation, expectedOperation);
        }

        [TestMethod]
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
            Assert.AreEqual(log.CreatedAt, expectedCreatedAt);
        }

        [TestMethod]
        public void Log_Should_Initialize_With_Null_Properties_By_Default()
        {
            // Act
            var log = new Log();

            // Assert
            Assert.AreEqual(log.Message, null);
            Assert.AreEqual(log.EntityName, null);
            Assert.AreEqual(log.EntityValue, null);
            Assert.AreEqual(log.Level, null);
            Assert.AreEqual(log.Operation, null);
        }
    }
}