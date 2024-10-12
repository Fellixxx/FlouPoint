namespace FlouPoint.Test.DTO.ResponseLogin
{
    using NUnit.Framework;
    using FluentAssertions;
    using System;
    using Newtonsoft.Json;
    using Domain.DTO.ResponseLogin;
    using Domain.Interfaces.ResponseLogin;
    using global::Domain.Interfaces.ResponseLogin;

    [TestFixture]
    public class ResponseLoginTests
    {
        [Test]
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
            responseLogin.AccessToken.Should().Be(expectedAccessToken);
            responseLogin.RefreshToken.Should().Be(expectedRefreshToken);
        }

        [Test]
        public void When_ResponseLogin_Is_Initialized_Then_DefaultValues_Are_EmptyStrings()
        {
            // When
            var responseLogin = new ResponseLogin();

            // Then
            responseLogin.AccessToken.Should().Be(string.Empty);
            responseLogin.RefreshToken.Should().Be(string.Empty);
        }

        [Test]
        public void When_ResponseLoginProperties_Are_Null_Then_Should_Accept_Null()
        {
            // Given
            var responseLogin = new ResponseLogin
            {
                AccessToken = null,
                RefreshToken = null
            };

            // Then
            responseLogin.AccessToken.Should().BeNull();
            responseLogin.RefreshToken.Should().BeNull();
        }

        [Test]
        public void When_ResponseLoginProperties_Are_EmptyStrings_Then_Should_Accept_EmptyStrings()
        {
            // Given
            var responseLogin = new ResponseLogin
            {
                AccessToken = string.Empty,
                RefreshToken = string.Empty
            };

            // Then
            responseLogin.AccessToken.Should().Be(string.Empty);
            responseLogin.RefreshToken.Should().Be(string.Empty);
        }

        [Test]
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
            responseLogin.AccessToken.Should().Be(specialCharacters);
            responseLogin.RefreshToken.Should().Be(specialCharacters);
        }

        [Test]
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
            responseLogin.AccessToken.Should().Be(longString);
            responseLogin.RefreshToken.Should().Be(longString);
        }

        [Test]
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
            deserializedResponseLogin.Should().BeEquivalentTo(responseLogin);
        }

        [Test]
        public void When_ResponseLogin_Is_Assigned_To_IResponseLogin_Interface_Then_Should_Work_Correctly()
        {
            // Given
            IResponseLogin responseLoginInterface = new ResponseLogin
            {
                AccessToken = "interface_access_token",
                RefreshToken = "interface_refresh_token"
            };

            // Then
            responseLoginInterface.AccessToken.Should().Be("interface_access_token");
            responseLoginInterface.RefreshToken.Should().Be("interface_refresh_token");
        }

        [Test]
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
            responseLogin.AccessToken.Should().Be(whitespace);
            responseLogin.RefreshToken.Should().Be(whitespace);
        }

        [Test]
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
            responseLogin.AccessToken.Should().Be(invalidToken);
            responseLogin.RefreshToken.Should().Be(invalidToken);
        }
    }
}
