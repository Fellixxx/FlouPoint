namespace Domain.Test.DTO.User
{
    using System;
    using Domain.DTO.User;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CreateUserTests
    {
        private CreateUser _testClass;

        [TestInitialize]
        public void SetUp()
        {
            _testClass=new CreateUser();
        }

        [TestMethod]
        public void CanSetAndGetName()
        {
            // Arrange
            var testValue = "TestValue1731729349";

            // Act
            _testClass.Name=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.Name);
        }

        [TestMethod]
        public void CanSetAndGetPassword()
        {
            // Arrange
            var testValue = "TestValue1458374591";

            // Act
            _testClass.Password=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.Password);
        }

        [TestMethod]
        public void CanSetAndGetEmail()
        {
            // Arrange
            var testValue = "TestValue1026318214";

            // Act
            _testClass.Email=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.Email);
        }
    }
}