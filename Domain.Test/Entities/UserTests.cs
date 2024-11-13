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
        /// Tests that the Id property can be correctly set and retrieved.
        /// </summary>
        [TestMethod]
        public void CanSetAndGetId()
        {
            // Arrange
            var testValue = "TestValue807988652";
            // Act
            _testClass.Id = testValue;
            // Assert
            Assert.AreEqual(testValue, _testClass.Id);
        }

        /// <summary>
        /// Tests that the Name property can be correctly set and retrieved.
        /// </summary>
        [TestMethod]
        public void CanSetAndGetName()
        {
            // Arrange
            var testValue = "TestValue221291709";
            // Act
            _testClass.Name = testValue;
            // Assert
            Assert.AreEqual(testValue, _testClass.Name);
        }

        /// <summary>
        /// Tests that the Password property can be correctly set and retrieved.
        /// </summary>
        [TestMethod]
        public void CanSetAndGetPassword()
        {
            // Arrange
            var testValue = "TestValue701044800";
            // Act
            _testClass.Password = testValue;
            // Assert
            Assert.AreEqual(testValue, _testClass.Password);
        }

        /// <summary>
        /// Tests that the Email property can be correctly set and retrieved.
        /// </summary>
        [TestMethod]
        public void CanSetAndGetEmail()
        {
            // Arrange
            var testValue = "TestValue616779784";
            // Act
            _testClass.Email = testValue;
            // Assert
            Assert.AreEqual(testValue, _testClass.Email);
        }

        /// <summary>
        /// Tests that the Avatar property can be correctly set and retrieved.
        /// </summary>
        [TestMethod]
        public void CanSetAndGetAvatar()
        {
            // Arrange
            var testValue = "TestValue638963839";
            // Act
            _testClass.Avatar = testValue;
            // Assert
            Assert.AreEqual(testValue, _testClass.Avatar);
        }

        /// <summary>
        /// Tests that the CreatedAt property can be correctly set and retrieved.
        /// </summary>
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

        /// <summary>
        /// Tests that the UpdatedAt property can be correctly set and retrieved.
        /// </summary>
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

        /// <summary>
        /// Tests that the Active property can be correctly set and retrieved.
        /// </summary>
        [TestMethod]
        public void CanSetAndGetActive()
        {
            // Arrange
            var testValue = false;
            // Act
            _testClass.Active = testValue;
            // Assert
            Assert.AreEqual(testValue, _testClass.Active);
        }
    }
}