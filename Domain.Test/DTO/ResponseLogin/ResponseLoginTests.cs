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
        [TestMethod]
        public void ResponseLogin_Should_Have_Default_Empty_AccessToken_And_RefreshToken()
        {
            // Act
            var responseLogin = new ResponseLogin();

            // Assert
            Assert.AreEqual(responseLogin.AccessToken, string.Empty);
            Assert.AreEqual(responseLogin.RefreshToken, string.Empty);
        }

        [TestMethod]
        public void ResponseLogin_Should_Store_AccessToken_Property_Correctly()
        {
            // Arrange
            string expectedAccessToken = "sample-access-token";

            // Act
            var responseLogin = new ResponseLogin
            {
                AccessToken = expectedAccessToken
            };

            // Assert
            Assert.AreEqual(responseLogin.AccessToken, expectedAccessToken);
        }

        [TestMethod]
        public void ResponseLogin_Should_Store_RefreshToken_Property_Correctly()
        {
            // Arrange
            string expectedRefreshToken = "sample-refresh-token";

            // Act
            var responseLogin = new ResponseLogin
            {
                RefreshToken = expectedRefreshToken
            };

            // Assert
            Assert.AreEqual(responseLogin.RefreshToken, expectedRefreshToken);
        }
    }
}