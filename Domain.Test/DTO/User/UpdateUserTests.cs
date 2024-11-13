namespace Domain.Test.DTO.User
{
    using System;
    using Domain.DTO.User;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Unit test class for testing the UpdateUser class properties.
    /// This class contains test methods to ensure property setting and getting
    /// work as intended for the UpdateUser DTO.
    /// </summary>
    [TestClass]
    public class UpdateUserTests
    {
        private UpdateUser _testClass;
        /// <summary>
        /// Initializes a new instance of the UpdateUser class before each test.
        /// This method sets up any common preparation required for each test case.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            _testClass = new UpdateUser();
        }

        /// <summary>
        /// Tests whether the Name property can be set and retrieved accurately.
        /// </summary>
        [TestMethod]
        public void CanSetAndGetName()
        {
            // Arrange: Set up a test value for the Name property.
            var testValue = "TestValue1973705464";
            // Act: Assign the test value to the Name property.
            _testClass.Name = testValue;
            // Assert: Verify that the Name property returns the assigned test value.
            Assert.AreEqual(testValue, _testClass.Name);
        }

        /// <summary>
        /// Tests whether the Password property can be set and retrieved correctly.
        /// </summary>
        [TestMethod]
        public void CanSetAndGetPassword()
        {
            // Arrange: Set up a test value for the Password property.
            var testValue = "TestValue573994964";
            // Act: Assign the test value to the Password property.
            _testClass.Password = testValue;
            // Assert: Check that the Password property returns the assigned test value.
            Assert.AreEqual(testValue, _testClass.Password);
        }

        /// <summary>
        /// Tests whether the Email property can be set and retrieved accurately.
        /// </summary>
        [TestMethod]
        public void CanSetAndGetEmail()
        {
            // Arrange: Set up a test value for the Email property.
            var testValue = "TestValue562784087";
            // Act: Assign the test value to the Email property.
            _testClass.Email = testValue;
            // Assert: Ensure that the Email property returns the assigned test value.
            Assert.AreEqual(testValue, _testClass.Email);
        }
    }
}