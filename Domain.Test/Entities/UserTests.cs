namespace Domain.Test.Entities
{
    using System;
    using Domain.Entities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UserTests
    {
        private User _testClass;

        [TestInitialize]
        public void SetUp()
        {
            _testClass=new User();
        }

        [TestMethod]
        public void CanSetAndGetId()
        {
            // Arrange
            var testValue = "TestValue807988652";

            // Act
            _testClass.Id=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.Id);
        }

        [TestMethod]
        public void CanSetAndGetName()
        {
            // Arrange
            var testValue = "TestValue221291709";

            // Act
            _testClass.Name=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.Name);
        }

        [TestMethod]
        public void CanSetAndGetPassword()
        {
            // Arrange
            var testValue = "TestValue701044800";

            // Act
            _testClass.Password=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.Password);
        }

        [TestMethod]
        public void CanSetAndGetEmail()
        {
            // Arrange
            var testValue = "TestValue616779784";

            // Act
            _testClass.Email=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.Email);
        }

        [TestMethod]
        public void CanSetAndGetAvatar()
        {
            // Arrange
            var testValue = "TestValue638963839";

            // Act
            _testClass.Avatar=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.Avatar);
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
        public void CanSetAndGetUpdatedAt()
        {
            // Arrange
            var testValue = DateTime.UtcNow;

            // Act
            _testClass.UpdatedAt=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.UpdatedAt);
        }

        [TestMethod]
        public void CanSetAndGetActive()
        {
            // Arrange
            var testValue = false;

            // Act
            _testClass.Active=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.Active);
        }
    }
}