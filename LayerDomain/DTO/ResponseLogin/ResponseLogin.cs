using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerDomain.DTO.ResponseLogin
{
    using Domain.DTO.ResponseLogin;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class ResponseLoginTests
    {
        [Test]
        public void ResponseLogin_Should_Have_Default_Empty_AccessToken_And_RefreshToken()
        {
            // Act
            var responseLogin = new ResponseLogin();

            // Assert
            responseLogin.AccessToken.Should().BeEmpty();
            responseLogin.RefreshToken.Should().BeEmpty();
        }

        [Test]
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
            responseLogin.AccessToken.Should().Be(expectedAccessToken);
        }

        [Test]
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
            responseLogin.RefreshToken.Should().Be(expectedRefreshToken);
        }
    }
}
