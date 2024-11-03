namespace Domain.Test.DTO.Log
{
    using System;
    using Domain.DTO.Logging;
    using Domain.Interfaces.Log;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;

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

        [TestMethod]
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
            Assert.AreEqual(expectedMessage, log.Message);
            Assert.AreEqual(expectedEntityName, log.EntityName);
            Assert.AreEqual(expectedEntityValue, log.EntityValue);
            Assert.AreEqual(expectedLevel, log.Level);
            Assert.AreEqual(expectedOperation, log.Operation);
            Assert.AreEqual(expectedCreatedAt, log.CreatedAt);
        }

        [TestMethod]
        public void When_LogIsInitialized_Then_DefaultValuesAreNullOrDefault()
        {
            // When
            var log = new Log();

            // Then
            Assert.IsNull(log.Message);
            Assert.IsNull(log.EntityName);
            Assert.IsNull(log.EntityValue);
            Assert.IsNull(log.Level);
            Assert.IsNull(log.Operation);
            Assert.AreEqual(default(DateTime), log.CreatedAt);
        }

        [TestMethod]
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
            Assert.IsNull(log.Message);
            Assert.IsNull(log.EntityName);
            Assert.IsNull(log.EntityValue);
            Assert.IsNull(log.Level);
            Assert.IsNull(log.Operation);
        }

        [TestMethod]
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
            Assert.AreEqual(string.Empty, log.Message);
            Assert.AreEqual(string.Empty, log.EntityName);
            Assert.AreEqual(string.Empty, log.EntityValue);
            Assert.AreEqual(string.Empty, log.Level);
            Assert.AreEqual(string.Empty, log.Operation);
        }

        [TestMethod]
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
            Assert.AreEqual(longString, log.Message);
            Assert.AreEqual(longString, log.EntityName);
            Assert.AreEqual(longString, log.EntityValue);
            Assert.AreEqual(longString, log.Level);
            Assert.AreEqual(longString, log.Operation);
        }

        [TestMethod]
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
            Assert.AreEqual(specialString, log.Message);
            Assert.AreEqual(specialString, log.EntityName);
            Assert.AreEqual(specialString, log.EntityValue);
            Assert.AreEqual(specialString, log.Level);
            Assert.AreEqual(specialString, log.Operation);
        }

        [TestMethod]
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
            Assert.AreEqual(log.Message, deserializedLog.Message);
            Assert.AreEqual(log.EntityName, deserializedLog.EntityName);
            Assert.AreEqual(log.EntityValue, deserializedLog.EntityValue);
            Assert.AreEqual(log.Level, deserializedLog.Level);
            Assert.AreEqual(log.Operation, deserializedLog.Operation);
            Assert.AreEqual(log.CreatedAt, deserializedLog.CreatedAt);
        }

        [TestMethod]
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
            Assert.AreEqual("Interface Test", logInterface.Message);
            Assert.AreEqual("InterfaceEntity", logInterface.EntityName);
            Assert.AreEqual("{}", logInterface.EntityValue);
            Assert.AreEqual("Warning", logInterface.Level);
            Assert.AreEqual("Update", logInterface.Operation);
            Assert.IsTrue(Math.Abs((logInterface.CreatedAt - DateTime.UtcNow).TotalSeconds) < 1);
        }

        [TestMethod]
        public void When_Log_CreatedAt_Is_DefaultValue_Then_Should_Be_MinValue()
        {
            // Given
            var log = new Log();

            // Then
            Assert.AreEqual(DateTime.MinValue, log.CreatedAt);
        }

        [TestMethod]
        public void When_Log_CreatedAt_Is_Set_To_FutureDate_Then_Should_Accept_Value()
        {
            // Given
            var futureDate = DateTime.UtcNow.AddYears(1);
            var log = new Log
            {
                CreatedAt = futureDate
            };

            // Then
            Assert.AreEqual(futureDate, log.CreatedAt);
        }

        [TestMethod]
        public void When_Log_CreatedAt_Is_Set_To_InvalidDate_Then_Should_Throw_Exception()
        {
            // Given
            var log = new Log();

            // When & Then
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                log.CreatedAt = DateTime.MaxValue.AddDays(1);
            });
        }

        [TestMethod]
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
            Assert.AreEqual(whitespaceString, log.Message);
            Assert.AreEqual(whitespaceString, log.EntityName);
            Assert.AreEqual(whitespaceString, log.EntityValue);
            Assert.AreEqual(whitespaceString, log.Level);
            Assert.AreEqual(whitespaceString, log.Operation);
        }
    }
}