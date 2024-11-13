namespace FlouPoint.Test.DTO.ResponseLogin
{
    using NUnit.Framework;
    using FluentAssertions;
    using System;
    using Newtonsoft.Json;
    using Domain.DTO.ResponseLogin;
    using Domain.Interfaces.ResponseLogin;
    using global::Domain.Interfaces.ResponseLogin;

    /// <summary>
    /// Test suite for the ResponseLogin class, ensuring that its properties
    /// are correctly set, initialized, and serialized across various use cases.
    /// </summary>
    [TestFixture]
    public class ResponseLoginTests
    {
        /// <summary>
        /// Tests that setting properties of ResponseLogin results in those properties
        /// being accurately returned.
        /// </summary>
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

        /// <summary>
        /// Verifies that a newly initialized ResponseLogin object has default property values
        /// of empty strings.
        /// </summary>
        [Test]
        public void When_ResponseLogin_Is_Initialized_Then_DefaultValues_Are_EmptyStrings()
        {
            // When
            var responseLogin = new ResponseLogin();
            // Then
            responseLogin.AccessToken.Should().Be(string.Empty);
            responseLogin.RefreshToken.Should().Be(string.Empty);
        }

        /// <summary>
        /// Ensures that the ResponseLogin class can accept null values for its properties.
        /// </summary>
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

        /// <summary>
        /// Validates that the ResponseLogin class can handle properties being set to empty strings.
        /// </summary>
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

        /// <summary>
        /// Confirms that the ResponseLogin class correctly handles properties containing special characters.
        /// </summary>
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

        /// <summary>
        /// Tests that very long strings can be correctly stored in the properties
        /// of ResponseLogin without alteration.
        /// </summary>
        [Test]
        public void When_ResponseLoginProperties_Have_VeryLongStrings_Then_Should_Handle_Correctly()
        {
            // Given
            var longString = new string ('a', 10000);
            var responseLogin = new ResponseLogin
            {
                AccessToken = longString,
                RefreshToken = longString
            };
            // Then
            responseLogin.AccessToken.Should().Be(longString);
            responseLogin.RefreshToken.Should().Be(longString);
        }

        /// <summary>
        /// Ensures that a ResponseLogin object is correctly serialized to JSON
        /// and subsequently deserialized without loss of data.
        /// </summary>
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

        /// <summary>
        /// Checks that assigning a ResponseLogin object to an IResponseLogin interface works as expected
        /// and that its properties return the correct values.
        /// </summary>
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

        /// <summary>
        /// Validates that the ResponseLogin properties can correctly store and return values with whitespace.
        /// </summary>
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

        /// <summary>
        /// Tests that invalid tokens assigned to ResponseLogin properties are stored and returned without modification.
        /// </summary>
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