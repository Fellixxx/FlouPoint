namespace Domain.Test.DTO.User
{
    using System;
    using Domain.DTO.User;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Unit test class for testing the properties of the <see cref = "ProxyUser"/> class.
    /// </summary>
    [TestClass]
    public class ProxyUserTests
    {
        private ProxyUser _testClass;
        /// <summary>
        /// Initializes the test class with a fresh instance of <see cref = "ProxyUser"/>.
        /// This method is called before each test method is executed.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            _testClass = new ProxyUser();
        }

        /// <summary>
        /// Test to ensure that the Id property can be set and retrieved correctly.
        /// </summary>
        [TestMethod]
        public void CanSetAndGetId()
        {
            // Arrange
            var testValue = "TestValue793144897";
            // Act
            _testClass.Id = testValue;
            // Assert
            Assert.AreEqual(testValue, _testClass.Id);
        }

        /// <summary>
        /// Test to ensure that the Name property can be set and retrieved correctly.
        /// </summary>
        [TestMethod]
        public void CanSetAndGetName()
        {
            // Arrange
            var testValue = "TestValue1452850";
            // Act
            _testClass.Name = testValue;
            // Assert
            Assert.AreEqual(testValue, _testClass.Name);
        }

        /// <summary>
        /// Test to ensure that the Email property can be set and retrieved correctly.
        /// </summary>
        [TestMethod]
        public void CanSetAndGetEmail()
        {
            // Arrange
            var testValue = "TestValue946483756";
            // Act
            _testClass.Email = testValue;
            // Assert
            Assert.AreEqual(testValue, _testClass.Email);
        }

        /// <summary>
        /// Test to ensure that the CreatedAt property can be set and retrieved correctly.
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
        /// Test to ensure that the UpdatedAt property can be set and retrieved correctly.
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
        /// Test to ensure that the Avatar property can be set and retrieved correctly.
        /// </summary>
        [TestMethod]
        public void CanSetAndGetAvatar()
        {
            // Arrange
            var testValue = "TestValue187255694";
            // Act
            _testClass.Avatar = testValue;
            // Assert
            Assert.AreEqual(testValue, _testClass.Avatar);
        }

        /// <summary>
        /// Test to ensure that the Active property can be set and retrieved correctly.
        /// </summary>
        [TestMethod]
        public void CanSetAndGetActive()
        {
            // Arrange
            var testValue = true;
            // Act
            _testClass.Active = testValue;
            // Assert
            Assert.AreEqual(testValue, _testClass.Active);
        }
    }
}