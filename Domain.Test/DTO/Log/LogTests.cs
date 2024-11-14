namespace Domain.Test.DTO.Log
{
    using System;
    using Domain.DTO.Logging;
    using Domain.Interfaces.Loggin;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;

    /// <summary>
    /// Test class for verifying the behavior of the Log class.
    /// </summary>
    [TestClass]
    public class LogTests
    {
        private Log _testClass;
        /// <summary>
        /// Initializes a new instance of Log for testing.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            _testClass = new Log();
        }

        [TestMethod]
        public void SetAndGetMessage_ShouldWorkCorrectly()
        {
            var testValue = "TestValue262371514";
            _testClass.Message = testValue;
            Assert.AreEqual(testValue, _testClass.Message);
        }

        [TestMethod]
        public void SetAndGetEntityName_ShouldWorkCorrectly()
        {
            var testValue = "TestValue127567551";
            _testClass.EntityName = testValue;
            Assert.AreEqual(testValue, _testClass.EntityName);
        }

        [TestMethod]
        public void SetAndGetEntityValue_ShouldWorkCorrectly()
        {
            var testValue = "TestValue2108728596";
            _testClass.EntityValue = testValue;
            Assert.AreEqual(testValue, _testClass.EntityValue);
        }

        [TestMethod]
        public void SetAndGetLevel_ShouldWorkCorrectly()
        {
            var testValue = "TestValue1190128197";
            _testClass.Level = testValue;
            Assert.AreEqual(testValue, _testClass.Level);
        }

        [TestMethod]
        public void SetAndGetOperation_ShouldWorkCorrectly()
        {
            var testValue = "TestValue9811108";
            _testClass.Operation = testValue;
            Assert.AreEqual(testValue, _testClass.Operation);
        }

        [TestMethod]
        public void SetAndGetCreatedAt_ShouldWorkCorrectly()
        {
            var testValue = DateTime.UtcNow;
            _testClass.CreatedAt = testValue;
            Assert.AreEqual(testValue, _testClass.CreatedAt);
        }

        [TestMethod]
        public void LogDefaultProperties_ShouldBeNull()
        {
            var log = new Log();
            Assert.IsNull(log.Message);
            Assert.IsNull(log.EntityName);
            Assert.IsNull(log.EntityValue);
            Assert.IsNull(log.Level);
            Assert.IsNull(log.Operation);
            Assert.AreEqual(default(DateTime), log.CreatedAt);
        }

        [TestMethod]
        public void LogProperties_SetAndReturnCorrectValues()
        {
            var expectedMessage = "Test Message";
            var expectedEntityName = "TestEntity";
            var expectedEntityValue = "{ \"id\": 1, \"name\": \"Test\" }";
            var expectedLevel = "Info";
            var expectedOperation = "Create";
            var expectedCreatedAt = DateTime.UtcNow;
            var log = new Log
            {
                Message = expectedMessage,
                EntityName = expectedEntityName,
                EntityValue = expectedEntityValue,
                Level = expectedLevel,
                Operation = expectedOperation,
                CreatedAt = expectedCreatedAt
            };
            Assert.AreEqual(expectedMessage, log.Message);
            Assert.AreEqual(expectedEntityName, log.EntityName);
            Assert.AreEqual(expectedEntityValue, log.EntityValue);
            Assert.AreEqual(expectedLevel, log.Level);
            Assert.AreEqual(expectedOperation, log.Operation);
            Assert.AreEqual(expectedCreatedAt, log.CreatedAt);
        }

        [TestMethod]
        public void HandleNullValuesProperly_WhenLogPropertiesAreNull()
        {
            var log = new Log
            {
                CreatedAt = DateTime.UtcNow
            };
            Assert.IsNull(log.Message);
            Assert.IsNull(log.EntityName);
            Assert.IsNull(log.EntityValue);
            Assert.IsNull(log.Level);
            Assert.IsNull(log.Operation);
        }

        [TestMethod]
        public void AcceptEmptyStrings_WhenLogPropertiesAreEmptyStrings()
        {
            var log = new Log
            {
                Message = string.Empty,
                EntityName = string.Empty,
                EntityValue = string.Empty,
                Level = string.Empty,
                Operation = string.Empty,
                CreatedAt = DateTime.UtcNow
            };
            Assert.AreEqual(string.Empty, log.Message);
            Assert.AreEqual(string.Empty, log.EntityName);
            Assert.AreEqual(string.Empty, log.EntityValue);
            Assert.AreEqual(string.Empty, log.Level);
            Assert.AreEqual(string.Empty, log.Operation);
        }

        [TestMethod]
        public void HandleVeryLongStringsProperly()
        {
            var longString = new string ('a', 10000);
            var log = new Log
            {
                Message = longString,
                EntityName = longString,
                EntityValue = longString,
                Level = longString,
                Operation = longString,
                CreatedAt = DateTime.UtcNow
            };
            Assert.AreEqual(longString, log.Message);
            Assert.AreEqual(longString, log.EntityName);
            Assert.AreEqual(longString, log.EntityValue);
            Assert.AreEqual(longString, log.Level);
            Assert.AreEqual(longString, log.Operation);
        }

        [TestMethod]
        public void HandleSpecialCharactersCorrectly()
        {
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
            Assert.AreEqual(specialString, log.Message);
            Assert.AreEqual(specialString, log.EntityName);
            Assert.AreEqual(specialString, log.EntityValue);
            Assert.AreEqual(specialString, log.Level);
            Assert.AreEqual(specialString, log.Operation);
        }

        [TestMethod]
        public void SerializeAndDeserializeLogCorrectly_WhenSerializedToJson()
        {
            var log = new Log
            {
                Message = "Test Message",
                EntityName = "TestEntity",
                EntityValue = "{ \"id\": 1, \"name\": \"Test\" }",
                Level = "Info",
                Operation = "Create",
                CreatedAt = DateTime.UtcNow
            };
            var json = JsonConvert.SerializeObject(log);
            var deserializedLog = JsonConvert.DeserializeObject<Log>(json);
            Assert.AreEqual(log.Message, deserializedLog.Message);
            Assert.AreEqual(log.EntityName, deserializedLog.EntityName);
            Assert.AreEqual(log.EntityValue, deserializedLog.EntityValue);
            Assert.AreEqual(log.Level, deserializedLog.Level);
            Assert.AreEqual(log.Operation, deserializedLog.Operation);
            Assert.AreEqual(log.CreatedAt, deserializedLog.CreatedAt);
        }

        [TestMethod]
        public void AssignLogToILogInterface_CorrectlyHandlesAssignment()
        {
            ILog logInterface = new Log
            {
                Message = "Interface Test",
                EntityName = "InterfaceEntity",
                EntityValue = "{}",
                Level = "Warning",
                Operation = "Update",
                CreatedAt = DateTime.UtcNow
            };
            Assert.AreEqual("Interface Test", logInterface.Message);
            Assert.AreEqual("InterfaceEntity", logInterface.EntityName);
            Assert.AreEqual("{}", logInterface.EntityValue);
            Assert.AreEqual("Warning", logInterface.Level);
            Assert.AreEqual("Update", logInterface.Operation);
            Assert.IsTrue(Math.Abs((logInterface.CreatedAt - DateTime.UtcNow).TotalSeconds) < 1);
        }

        [TestMethod]
        public void CreatedAt_ShouldBeMinValue_ByDefault()
        {
            var log = new Log();
            Assert.AreEqual(DateTime.MinValue, log.CreatedAt);
        }

        [TestMethod]
        public void SetFutureDate_ShouldAcceptFutureDate_ForCreatedAt()
        {
            var futureDate = DateTime.UtcNow.AddYears(1);
            var log = new Log
            {
                CreatedAt = futureDate
            };
            Assert.AreEqual(futureDate, log.CreatedAt);
        }

        [TestMethod]
        public void SetInvalidDate_ShouldThrowException_ForCreatedAt()
        {
            var log = new Log();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                log.CreatedAt = DateTime.MaxValue.AddDays(1);
            });
        }

        [TestMethod]
        public void HandleWhitespaceStringsCorrectly()
        {
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
            Assert.AreEqual(whitespaceString, log.Message);
            Assert.AreEqual(whitespaceString, log.EntityName);
            Assert.AreEqual(whitespaceString, log.EntityValue);
            Assert.AreEqual(whitespaceString, log.Level);
            Assert.AreEqual(whitespaceString, log.Operation);
        }

        [TestMethod]
        public void SetCreatedAtToDateTimeMinValue_ShouldWorkCorrectly()
        {
            var testValue = DateTime.MinValue;
            _testClass.CreatedAt = testValue;
            Assert.AreEqual(testValue, _testClass.CreatedAt);
        }

        [TestMethod]
        public void SetCreatedAtToDateTimeMaxValue_ShouldWorkCorrectly()
        {
            var testValue = DateTime.MaxValue;
            _testClass.CreatedAt = testValue;
            Assert.AreEqual(testValue, _testClass.CreatedAt);
        }

        [TestMethod]
        public void SerializeAndDeserializeLogWithNullProperties_Correctly()
        {
            var log = new Log
            {
                Message = null,
                EntityName = null,
                EntityValue = null,
                Level = null,
                Operation = null,
                CreatedAt = DateTime.UtcNow
            };
            var json = JsonConvert.SerializeObject(log);
            var deserializedLog = JsonConvert.DeserializeObject<Log>(json);
            Assert.IsNull(deserializedLog.Message);
            Assert.IsNull(deserializedLog.EntityName);
            Assert.IsNull(deserializedLog.EntityValue);
            Assert.IsNull(deserializedLog.Level);
            Assert.IsNull(deserializedLog.Operation);
            Assert.AreEqual(log.CreatedAt, deserializedLog.CreatedAt);
        }

        [TestMethod]
        public void CanSetPropertiesToNull_AfterSettingValues()
        {
            _testClass.Message = "Test Message";
            _testClass.EntityName = "Test Entity";
            _testClass.EntityValue = "{ \"id\": 1 }";
            _testClass.Level = "Info";
            _testClass.Operation = "Create";
            _testClass.Message = null;
            _testClass.EntityName = null;
            _testClass.EntityValue = null;
            _testClass.Level = null;
            _testClass.Operation = null;
            Assert.IsNull(_testClass.Message);
            Assert.IsNull(_testClass.EntityName);
            Assert.IsNull(_testClass.EntityValue);
            Assert.IsNull(_testClass.Level);
            Assert.IsNull(_testClass.Operation);
        }

        [TestMethod]
        public void AddLogToCollection_ShouldWorkCorrectly()
        {
            var log1 = new Log
            {
                Message = "Log 1"
            };
            var log2 = new Log
            {
                Message = "Log 2"
            };
            var logs = new List<Log>();
            logs.Add(log1);
            logs.Add(log2);
            Assert.AreEqual(2, logs.Count);
            Assert.AreEqual("Log 1", logs[0].Message);
            Assert.AreEqual("Log 2", logs[1].Message);
        }

        [TestMethod]
        public void TwoLogObjectsWithSameValues_AreNotEqualByDefault()
        {
            var log1 = new Log
            {
                Message = "Test Message",
                EntityName = "Test Entity",
                EntityValue = "{ \"id\": 1 }",
                Level = "Info",
                Operation = "Create",
                CreatedAt = DateTime.UtcNow
            };
            var log2 = new Log
            {
                Message = "Test Message",
                EntityName = "Test Entity",
                EntityValue = "{ \"id\": 1 }",
                Level = "Info",
                Operation = "Create",
                CreatedAt = log1.CreatedAt
            };
            Assert.AreNotEqual(log1, log2);
        }

        [TestMethod]
        public void CloneLogObject_ShouldWorkCorrectly()
        {
            var originalLog = new Log
            {
                Message = "Original Message",
                EntityName = "Original Entity",
                EntityValue = "{ \"id\": 1 }",
                Level = "Info",
                Operation = "Create",
                CreatedAt = DateTime.UtcNow
            };
            var clonedLog = new Log
            {
                Message = originalLog.Message,
                EntityName = originalLog.EntityName,
                EntityValue = originalLog.EntityValue,
                Level = originalLog.Level,
                Operation = originalLog.Operation,
                CreatedAt = originalLog.CreatedAt
            };
            Assert.AreEqual(originalLog.Message, clonedLog.Message);
            Assert.AreEqual(originalLog.EntityName, clonedLog.EntityName);
            Assert.AreEqual(originalLog.EntityValue, clonedLog.EntityValue);
            Assert.AreEqual(originalLog.Level, clonedLog.Level);
            Assert.AreEqual(originalLog.Operation, clonedLog.Operation);
            Assert.AreEqual(originalLog.CreatedAt, clonedLog.CreatedAt);
            Assert.AreNotSame(originalLog, clonedLog);
        }

        [TestMethod]
        public void LevelProperty_AllowsAnyString()
        {
            var testValues = new[]
            {
                "Info",
                "Warning",
                "Error",
                "Debug",
                "Verbose",
                "CustomLevel"
            };
            foreach (var level in testValues)
            {
                _testClass.Level = level;
                Assert.AreEqual(level, _testClass.Level);
            }
        }

        [TestMethod]
        public void EntityValue_AllowsDifferentSerializedFormats()
        {
            var jsonValue = "{ \"id\": 1, \"name\": \"Test\" }";
            var xmlValue = "<Entity><Id>1</Id><Name>Test</Name></Entity>";
            var yamlValue = "id: 1\nname: Test";
            _testClass.EntityValue = jsonValue;
            Assert.AreEqual(jsonValue, _testClass.EntityValue);
            _testClass.EntityValue = xmlValue;
            Assert.AreEqual(xmlValue, _testClass.EntityValue);
            _testClass.EntityValue = yamlValue;
            Assert.AreEqual(yamlValue, _testClass.EntityValue);
        }

        [TestMethod]
        public void CreateDerivedClassFromLog_ShouldWorkCorrectly()
        {
            var extendedLog = new ExtendedLog
            {
                Message = "Extended Message",
                AdditionalInfo = "Extra Data"
            };
            Assert.AreEqual("Extended Message", extendedLog.Message);
            Assert.AreEqual("Extra Data", extendedLog.AdditionalInfo);
        }

        public class ExtendedLog : Log
        {
            public string AdditionalInfo { get; set; }
        }

        [TestMethod]
        public void AccessLogPropertiesConcurrently_ShouldNotThrowExceptions()
        {
            var log = new Log();
            var tasks = new List<Task>();
            var exceptionOccurred = false;
            for (int i = 0; i < 100; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    try
                    {
                        log.Message = "Concurrent Message";
                        var temp = log.Message;
                    }
                    catch
                    {
                        exceptionOccurred = true;
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
            Assert.IsFalse(exceptionOccurred, "Exception occurred during concurrent access.");
        }
    }
}