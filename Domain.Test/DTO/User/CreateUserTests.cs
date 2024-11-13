namespace Domain.Test.DTO.User
{
    using System;
    using Domain.DTO.User;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This class contains unit tests for the CreateUser class, which is 
    /// part of the Domain.DTO.User namespace. The tests ensure that the 
    /// properties of the CreateUser class can be set and retrieved correctly.
    /// </summary>
    [TestClass]
    public class CreateUserTests
    {
        private CreateUser _testClass;
        /// <summary>
        /// Initializes a new instance of the CreateUser class before each test.
        /// This method is called before the execution of each test method to 
        /// set up any necessary test data.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            _testClass = new CreateUser();
        }

        /// <summary>
        /// Tests whether the Name property of the CreateUser class can be 
        /// set and retrieved successfully. The test passes if the retrieved 
        /// Name matches the value that was set.
        /// </summary>
        [TestMethod]
        public void CanSetAndGetName()
        {
            // Arrange
            var testValue = "TestValue1731729349";
            // Act
            _testClass.Name = testValue;
            // Assert
            Assert.AreEqual(testValue, _testClass.Name);
        }

        /// <summary>
        /// Tests whether the Password property of the CreateUser class can be 
        /// set and retrieved successfully. The test passes if the retrieved 
        /// Password matches the value that was set.
        /// </summary>
        [TestMethod]
        public void CanSetAndGetPassword()
        {
            // Arrange
            var testValue = "TestValue1458374591";
            // Act
            _testClass.Password = testValue;
            // Assert
            Assert.AreEqual(testValue, _testClass.Password);
        }

        /// <summary>
        /// Tests whether the Email property of the CreateUser class can be 
        /// set and retrieved successfully. The test passes if the retrieved 
        /// Email matches the value that was set.
        /// </summary>
        [TestMethod]
        public void CanSetAndGetEmail()
        {
            // Arrange
            var testValue = "TestValue1026318214";
            // Act
            _testClass.Email = testValue;
            // Assert
            Assert.AreEqual(testValue, _testClass.Email);
        }
    }
}