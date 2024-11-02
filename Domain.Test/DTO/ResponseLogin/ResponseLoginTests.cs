namespace Domain.Test.DTO.ResponseLogin
{
    using System;
    using Domain.DTO.ResponseLogin;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ResponseLoginTests
    {
        private ResponseLogin _testClass;

        [TestInitialize]
        public void SetUp()
        {
            _testClass=new ResponseLogin();
        }

        [TestMethod]
        public void CanSetAndGetAccessToken()
        {
            // Arrange
            var testValue = "TestValue931380528";

            // Act
            _testClass.AccessToken=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.AccessToken);
        }

        [TestMethod]
        public void CanSetAndGetRefreshToken()
        {
            // Arrange
            var testValue = "TestValue1193297862";

            // Act
            _testClass.RefreshToken=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.RefreshToken);
        }
    }
}