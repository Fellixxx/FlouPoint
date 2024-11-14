namespace Domain.Test.Entities
{
    using System;
    using Domain.Entities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Contains unit tests for the <see cref = "User"/> class to verify its properties' functionality.
    /// </summary>
    [TestClass]
    public class UserTests
    {
        private User _testClass;
        /// <summary>
        /// Sets up the test environment by initializing a new instance of the <see cref = "User"/> class before each test.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            _testClass = new User();
        }

        /// <summary>
        /// Tests that the initial properties are set correctly.
        /// </summary>
        [TestMethod]
        public void InitialPropertiesAreSetCorrectly()
        {
            Assert.AreEqual(string.Empty, _testClass.Id);
            Assert.IsNull(_testClass.Name);
            Assert.IsNull(_testClass.Password);
            Assert.IsNull(_testClass.Email);
            Assert.IsNull(_testClass.Avatar);
            Assert.IsNull(_testClass.UpdatedAt);
            Assert.AreEqual(default(DateTime), _testClass.CreatedAt);
            Assert.IsFalse(_testClass.Active);
        }

        [TestMethod]
        public void CanSetAndGetId()
        {
            // Arrange
            var testValue = "TestValue807988652";
            // Act
            _testClass.Id = testValue;
            // Assert
            Assert.AreEqual(testValue, _testClass.Id);
            // Test for setting null
            _testClass.Id = null;
            Assert.IsNull(_testClass.Id);
        }

        [TestMethod]
        public void CanSetAndGetName()
        {
            // Arrange
            var testValue = "TestValue221291709";
            // Act
            _testClass.Name = testValue;
            // Assert
            Assert.AreEqual(testValue, _testClass.Name);
            // Test for setting null
            _testClass.Name = null;
            Assert.IsNull(_testClass.Name);
        }

        [TestMethod]
        public void CanSetAndGetPassword()
        {
            // Arrange
            var testValue = "TestValue701044800";
            // Act
            _testClass.Password = testValue;
            // Assert
            Assert.AreEqual(testValue, _testClass.Password);
            // Test for setting null
            _testClass.Password = null;
            Assert.IsNull(_testClass.Password);
        }

        [TestMethod]
        public void CanSetAndGetEmail()
        {
            // Arrange
            var testValue = "TestValue616779784";
            // Act
            _testClass.Email = testValue;
            // Assert
            Assert.AreEqual(testValue, _testClass.Email);
            // Test for setting null
            _testClass.Email = null;
            Assert.IsNull(_testClass.Email);
        }

        [TestMethod]
        public void CanSetAndGetAvatar()
        {
            // Arrange
            var testValue = "TestValue638963839";
            // Act
            _testClass.Avatar = testValue;
            // Assert
            Assert.AreEqual(testValue, _testClass.Avatar);
            // Test for setting null
            _testClass.Avatar = null;
            Assert.IsNull(_testClass.Avatar);
        }

        [TestMethod]
        public void CanSetAndGetCreatedAt()
        {
            // Arrange
            var testValue = DateTime.UtcNow;
            // Act
            _testClass.CreatedAt = testValue;
            // Assert
            Assert.AreEqual(testValue, _testClass.CreatedAt);
        }

        [TestMethod]
        public void CanSetAndGetUpdatedAt()
        {
            // Arrange
            var testValue = DateTime.UtcNow;
            // Act
            _testClass.UpdatedAt = testValue;
            // Assert
            Assert.AreEqual(testValue, _testClass.UpdatedAt);
        }

        [TestMethod]
        public void CanSetAndGetActive()
        {
            // Arrange
            var testValue = false;
            // Act
            _testClass.Active = testValue;
            // Assert
            Assert.IsFalse(_testClass.Active);
            // Test setting it to true
            _testClass.Active = true;
            Assert.IsTrue(_testClass.Active);
        }
    }
}