namespace Domain.Test.DTO.User
{
    using System;
    using Domain.DTO.User;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ProxyUserTests
    {
        private ProxyUser _testClass;

        [TestInitialize]
        public void SetUp()
        {
            _testClass=new ProxyUser();
        }

        [TestMethod]
        public void CanSetAndGetId()
        {
            // Arrange
            var testValue = "TestValue793144897";

            // Act
            _testClass.Id=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.Id);
        }

        [TestMethod]
        public void CanSetAndGetName()
        {
            // Arrange
            var testValue = "TestValue1452850";

            // Act
            _testClass.Name=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.Name);
        }

        [TestMethod]
        public void CanSetAndGetEmail()
        {
            // Arrange
            var testValue = "TestValue946483756";

            // Act
            _testClass.Email=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.Email);
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
        public void CanSetAndGetAvatar()
        {
            // Arrange
            var testValue = "TestValue187255694";

            // Act
            _testClass.Avatar=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.Avatar);
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