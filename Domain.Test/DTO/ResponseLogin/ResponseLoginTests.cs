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

        [TestMethod]
        public void CanSetPropertiesToNullAfterSettingValue()
        {
            // Arrange
            _testClass.AccessToken = "SampleAccessToken";
            _testClass.RefreshToken = "SampleRefreshToken";

            // Act
            _testClass.AccessToken = null;
            _testClass.RefreshToken = null;

            // Assert
            Assert.IsNull(_testClass.AccessToken);
            Assert.IsNull(_testClass.RefreshToken);
        }

        [TestMethod]
        public void CanAddResponseLoginToCollection()
        {
            // Arrange
            var responseLogin1 = new ResponseLogin { AccessToken = "Token1", RefreshToken = "Refresh1" };
            var responseLogin2 = new ResponseLogin { AccessToken = "Token2", RefreshToken = "Refresh2" };
            var responses = new List<ResponseLogin>();

            // Act
            responses.Add(responseLogin1);
            responses.Add(responseLogin2);

            // Assert
            Assert.AreEqual(2, responses.Count);
            Assert.AreEqual("Token1", responses[0].AccessToken);
            Assert.AreEqual("Refresh1", responses[0].RefreshToken);
            Assert.AreEqual("Token2", responses[1].AccessToken);
            Assert.AreEqual("Refresh2", responses[1].RefreshToken);
        }

        [TestMethod]
        public void TwoResponseLoginObjectsWithSameValues_AreNotEqualByDefault()
        {
            // Arrange
            var responseLogin1 = new ResponseLogin
            {
                AccessToken = "SameToken",
                RefreshToken = "SameRefresh"
            };

            var responseLogin2 = new ResponseLogin
            {
                AccessToken = "SameToken",
                RefreshToken = "SameRefresh"
            };

            // Act & Assert
            Assert.AreNotEqual(responseLogin1, responseLogin2);
        }

        [TestMethod]
        public void CanCloneResponseLoginObject()
        {
            // Arrange
            var originalResponse = new ResponseLogin
            {
                AccessToken = "OriginalAccessToken",
                RefreshToken = "OriginalRefreshToken"
            };

            // Act
            var clonedResponse = new ResponseLogin
            {
                AccessToken = originalResponse.AccessToken,
                RefreshToken = originalResponse.RefreshToken
            };

            // Assert
            Assert.AreEqual(originalResponse.AccessToken, clonedResponse.AccessToken);
            Assert.AreEqual(originalResponse.RefreshToken, clonedResponse.RefreshToken);
            Assert.AreNotSame(originalResponse, clonedResponse);
        }

        [TestMethod]
        public void When_ResponseLoginProperties_Have_UnicodeCharacters_Then_Should_Handle_Correctly()
        {
            // Given
            var unicodeString = "アクセストークン🔑";
            var responseLogin = new ResponseLogin
            {
                AccessToken = unicodeString,
                RefreshToken = unicodeString
            };

            // Then
            Assert.AreEqual(unicodeString, responseLogin.AccessToken);
            Assert.AreEqual(unicodeString, responseLogin.RefreshToken);
        }

        [TestMethod]
        public void When_ResponseLogin_With_Null_Properties_Is_Serialized_Then_Should_Deserialize_Correctly()
        {
            // Arrange
            var responseLogin = new ResponseLogin
            {
                AccessToken = null,
                RefreshToken = null
            };

            // Act
            var json = JsonConvert.SerializeObject(responseLogin);
            var deserializedResponseLogin = JsonConvert.DeserializeObject<ResponseLogin>(json);

            // Assert
            Assert.IsNull(deserializedResponseLogin.AccessToken);
            Assert.IsNull(deserializedResponseLogin.RefreshToken);
        }

        [TestMethod]
        public void CanSetPropertiesToEmptyStringAfterSettingValue()
        {
            // Arrange
            _testClass.AccessToken = "SampleAccessToken";
            _testClass.RefreshToken = "SampleRefreshToken";

            // Act
            _testClass.AccessToken = string.Empty;
            _testClass.RefreshToken = string.Empty;

            // Assert
            Assert.AreEqual(string.Empty, _testClass.AccessToken);
            Assert.AreEqual(string.Empty, _testClass.RefreshToken);
        }

        [TestMethod]
        public void CanCreateDerivedClassFromResponseLogin()
        {
            // Arrange
            var extendedResponse = new ExtendedResponseLogin
            {
                AccessToken = "ExtendedAccessToken",
                RefreshToken = "ExtendedRefreshToken",
                AdditionalProperty = "ExtraData"
            };

            // Act & Assert
            Assert.AreEqual("ExtendedAccessToken", extendedResponse.AccessToken);
            Assert.AreEqual("ExtendedRefreshToken", extendedResponse.RefreshToken);
            Assert.AreEqual("ExtraData", extendedResponse.AdditionalProperty);
        }

        public class ExtendedResponseLogin : ResponseLogin
        {
            public string AdditionalProperty { get; set; }
        }

        [TestMethod]
        public void ResponseLoginProperties_CanBeAccessedConcurrently()
        {
            // Arrange
            var responseLogin = new ResponseLogin();
            var tasks = new List<Task>();
            var exceptionOccurred = false;

            // Act
            for (int i = 0; i < 100; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    try
                    {
                        responseLogin.AccessToken = "ConcurrentAccessToken";
                        var temp = responseLogin.AccessToken;
                    }
                    catch
                    {
                        exceptionOccurred = true;
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            // Assert
            Assert.IsFalse(exceptionOccurred, "Exception occurred during concurrent access.");
        }

        [TestMethod]
        public void ResponseLogin_CannotBeUsedAsDictionaryKeyWithoutOverridingEqualsAndGetHashCode()
        {
            // Arrange
            var responseLogin1 = new ResponseLogin { AccessToken = "Token1", RefreshToken = "Refresh1" };
            var responseLogin2 = new ResponseLogin { AccessToken = "Token1", RefreshToken = "Refresh1" };
            var dictionary = new Dictionary<ResponseLogin, string>();

            // Act
            dictionary[responseLogin1] = "First Entry";
            dictionary[responseLogin2] = "Second Entry"; // Should overwrite if objects are equal

            // Assert
            Assert.AreEqual(2, dictionary.Count); // They are considered different keys
        }

    }
}