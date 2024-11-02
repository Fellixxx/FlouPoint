namespace Domain.Test.Entities
{
    using System;
    using Domain.Entities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ResourceEntryTests
    {
        private ResourceEntry _testClass;

        [TestInitialize]
        public void SetUp()
        {
            _testClass=new ResourceEntry();
        }

        [TestMethod]
        public void CanSetAndGetId()
        {
            // Arrange
            var testValue = "TestValue68172804";

            // Act
            _testClass.Id=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.Id);
        }

        [TestMethod]
        public void CanSetAndGetName()
        {
            // Arrange
            var testValue = "TestValue503865662";

            // Act
            _testClass.Name=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.Name);
        }

        [TestMethod]
        public void CanSetAndGetValue()
        {
            // Arrange
            var testValue = "TestValue1556702727";

            // Act
            _testClass.Value=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.Value);
        }

        [TestMethod]
        public void CanSetAndGetComment()
        {
            // Arrange
            var testValue = "TestValue616474427";

            // Act
            _testClass.Comment=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.Comment);
        }

        [TestMethod]
        public void CanSetAndGetActive()
        {
            // Arrange
            var testValue = true;

            // Act
            _testClass.Active=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.Active);
        }
    }
}