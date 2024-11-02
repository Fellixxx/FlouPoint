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
    }
}