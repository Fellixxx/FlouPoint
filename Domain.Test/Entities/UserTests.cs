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
        /// Tests that the initial properties of a newly instantiated <see cref = "User"/> object are set to their default values.
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

        /// <summary>
        /// Verifies that the <see cref = "User.Id"/> property can be set and retrieved correctly.
        /// </summary>
        [TestMethod]
        public void CanSetAndGetId()
        {
            var testValue = "TestValue807988652";
            _testClass.Id = testValue;
            Assert.AreEqual(testValue, _testClass.Id);
            // Test for setting null
            _testClass.Id = null;
            Assert.IsNull(_testClass.Id);
        }

        /// <summary>
        /// Verifies that the <see cref = "User.Name"/> property can be set and retrieved correctly.
        /// </summary>
        [TestMethod]
        public void CanSetAndGetName()
        {
            var testValue = "TestValue221291709";
            _testClass.Name = testValue;
            Assert.AreEqual(testValue, _testClass.Name);
            // Test for setting null
            _testClass.Name = null;
            Assert.IsNull(_testClass.Name);
        }

        /// <summary>
        /// Verifies that the <see cref = "User.Password"/> property can be set and retrieved correctly.
        /// </summary>
        [TestMethod]
        public void CanSetAndGetPassword()
        {
            var testValue = "TestValue701044800";
            _testClass.Password = testValue;
            Assert.AreEqual(testValue, _testClass.Password);
            // Test for setting null
            _testClass.Password = null;
            Assert.IsNull(_testClass.Password);
        }

        /// <summary>
        /// Verifies that the <see cref = "User.Email"/> property can be set and retrieved correctly.
        /// </summary>
        [TestMethod]
        public void CanSetAndGetEmail()
        {
            var testValue = "TestValue616779784";
            _testClass.Email = testValue;
            Assert.AreEqual(testValue, _testClass.Email);
            // Test for setting null
            _testClass.Email = null;
            Assert.IsNull(_testClass.Email);
        }

        /// <summary>
        /// Verifies that the <see cref = "User.Avatar"/> property can be set and retrieved correctly.
        /// </summary>
        [TestMethod]
        public void CanSetAndGetAvatar()
        {
            var testValue = "TestValue638963839";
            _testClass.Avatar = testValue;
            Assert.AreEqual(testValue, _testClass.Avatar);
            // Test for setting null
            _testClass.Avatar = null;
            Assert.IsNull(_testClass.Avatar);
        }

        /// <summary>
        /// Verifies that the <see cref = "User.CreatedAt"/> property can be set and retrieved correctly.
        /// </summary>
        [TestMethod]
        public void CanSetAndGetCreatedAt()
        {
            var testValue = DateTime.UtcNow;
            _testClass.CreatedAt = testValue;
            Assert.AreEqual(testValue, _testClass.CreatedAt);
        }

        /// <summary>
        /// Verifies that the <see cref = "User.UpdatedAt"/> property can be set and retrieved correctly.
        /// </summary>
        [TestMethod]
        public void CanSetAndGetUpdatedAt()
        {
            var testValue = DateTime.UtcNow;
            _testClass.UpdatedAt = testValue;
            Assert.AreEqual(testValue, _testClass.UpdatedAt);
        }

        /// <summary>
        /// Verifies that the <see cref = "User.Active"/> property can be set and retrieved correctly.
        /// </summary>
        [TestMethod]
        public void CanSetAndGetActive()
        {
            var testValue = false;
            _testClass.Active = testValue;
            Assert.IsFalse(_testClass.Active);
            // Test setting it to true
            _testClass.Active = true;
            Assert.IsTrue(_testClass.Active);
        }
    }
}