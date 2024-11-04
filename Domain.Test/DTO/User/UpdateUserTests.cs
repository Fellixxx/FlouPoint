namespace Domain.Test.DTO.User
{
    using System;
    using Domain.DTO.User;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UpdateUserTests
    {
        private UpdateUser _testClass;

        [TestInitialize]
        public void SetUp()
        {
            _testClass=new UpdateUser();
        }

        [TestMethod]
        public void CanSetAndGetName()
        {
            // Arrange
            var testValue = "TestValue1973705464";

            // Act
            _testClass.Name=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.Name);
        }

        [TestMethod]
        public void CanSetAndGetPassword()
        {
            // Arrange
            var testValue = "TestValue573994964";

            // Act
            _testClass.Password=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.Password);
        }

        [TestMethod]
        public void CanSetAndGetEmail()
        {
            // Arrange
            var testValue = "TestValue562784087";

            // Act
            _testClass.Email=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.Email);
        }
    }
}