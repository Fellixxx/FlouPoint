namespace Domain.Test.DTO.ResponseLogin
{
    using System;
    using Domain.DTO.Login;
    using Domain.Interfaces.Login;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;

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

        [TestMethod]
        public void When_ResponseLoginProperties_Are_Set_Then_They_Should_Return_Same_Values()
        {
            // Given
            var expectedAccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9";
            var expectedRefreshToken = "dGhpcyBpcyBhIHJlZnJlc2ggdG9rZW4";

            // When
            var responseLogin = new ResponseLogin
            {
                AccessToken = expectedAccessToken,
                RefreshToken = expectedRefreshToken
            };

            // Then
            Assert.AreEqual(expectedAccessToken, responseLogin.AccessToken);
            Assert.AreEqual(expectedRefreshToken, responseLogin.RefreshToken);
        }

        [TestMethod]
        public void When_ResponseLogin_Is_Initialized_Then_DefaultValues_Are_EmptyStrings()
        {
            // When
            var responseLogin = new ResponseLogin();

            // Then
            Assert.AreEqual(string.Empty, responseLogin.AccessToken);
            Assert.AreEqual(string.Empty, responseLogin.RefreshToken);
        }

        [TestMethod]
        public void When_ResponseLoginProperties_Are_Null_Then_Should_Accept_Null()
        {
            // Given
            var responseLogin = new ResponseLogin
            {
                AccessToken = null,
                RefreshToken = null
            };

            // Then
            Assert.IsNull(responseLogin.AccessToken);
            Assert.IsNull(responseLogin.RefreshToken);
        }

        [TestMethod]
        public void When_ResponseLoginProperties_Are_EmptyStrings_Then_Should_Accept_EmptyStrings()
        {
            // Given
            var responseLogin = new ResponseLogin
            {
                AccessToken = string.Empty,
                RefreshToken = string.Empty
            };

            // Then
            Assert.AreEqual(string.Empty, responseLogin.AccessToken);
            Assert.AreEqual(string.Empty, responseLogin.RefreshToken);
        }

        [TestMethod]
        public void When_ResponseLoginProperties_Have_SpecialCharacters_Then_Should_Handle_Correctly()
        {
            // Given
            var specialCharacters = "!@#$%^&*()_+|~=`{}[]:\";'<>?,./\\";
            var responseLogin = new ResponseLogin
            {
                AccessToken = specialCharacters,
                RefreshToken = specialCharacters
            };

            // Then
            Assert.AreEqual(specialCharacters, responseLogin.AccessToken);
            Assert.AreEqual(specialCharacters, responseLogin.RefreshToken);
        }

        [TestMethod]
        public void When_ResponseLoginProperties_Have_VeryLongStrings_Then_Should_Handle_Correctly()
        {
            // Given
            var longString = new string('a', 10000);
            var responseLogin = new ResponseLogin
            {
                AccessToken = longString,
                RefreshToken = longString
            };

            // Then
            Assert.AreEqual(longString, responseLogin.AccessToken);
            Assert.AreEqual(longString, responseLogin.RefreshToken);
        }

        [TestMethod]
        public void When_ResponseLogin_Is_Serialized_To_Json_Then_Should_Deserialize_Correctly()
        {
            // Given
            var responseLogin = new ResponseLogin
            {
                AccessToken = "access_token_example",
                RefreshToken = "refresh_token_example"
            };

            // When
            var json = JsonConvert.SerializeObject(responseLogin);
            var deserializedResponseLogin = JsonConvert.DeserializeObject<ResponseLogin>(json);

            // Then
            Assert.AreEqual(responseLogin.AccessToken, deserializedResponseLogin.AccessToken);
            Assert.AreEqual(responseLogin.RefreshToken, deserializedResponseLogin.RefreshToken);
        }

        [TestMethod]
        public void When_ResponseLogin_Is_Assigned_To_IResponseLogin_Interface_Then_Should_Work_Correctly()
        {
            // Given
            IResponseLogin responseLoginInterface = new ResponseLogin
            {
                AccessToken = "interface_access_token",
                RefreshToken = "interface_refresh_token"
            };

            // Then
            Assert.AreEqual("interface_access_token", responseLoginInterface.AccessToken);
            Assert.AreEqual("interface_refresh_token", responseLoginInterface.RefreshToken);
        }

        [TestMethod]
        public void When_ResponseLoginProperties_Have_Whitespace_Then_Should_Handle_Correctly()
        {
            // Given
            var whitespace = "   ";
            var responseLogin = new ResponseLogin
            {
                AccessToken = whitespace,
                RefreshToken = whitespace
            };

            // Then
            Assert.AreEqual(whitespace, responseLogin.AccessToken);
            Assert.AreEqual(whitespace, responseLogin.RefreshToken);
        }

        [TestMethod]
        public void When_ResponseLoginProperties_Are_Invalid_Tokens_Then_Should_Store_As_Is()
        {
            // Given
            var invalidToken = "invalid_token_string";
            var responseLogin = new ResponseLogin
            {
                AccessToken = invalidToken,
                RefreshToken = invalidToken
            };

            // Then
            Assert.AreEqual(invalidToken, responseLogin.AccessToken);
            Assert.AreEqual(invalidToken, responseLogin.RefreshToken);
        }
    }
}