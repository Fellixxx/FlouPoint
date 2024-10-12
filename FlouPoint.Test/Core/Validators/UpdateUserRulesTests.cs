namespace FlouPoint.Test.Infrastructure.Repository
{
    using Application.Validators.User;
    using FlouPoint.Domain.Entities;
    using FluentAssertions;
    using FluentValidation.Results;

    [TestFixture]
    public class UpdateUserRulesTests
    {
        private UpdateUserRules _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new UpdateUserRules();
        }

        [Test]
        public Task When_ModifyUser_ValidId_Then_Success()
        {
            // Given
            var expectedId = Guid.NewGuid().ToString();
            User user = new()
            {
                Id = expectedId,
                Name = "testUsername",
                Password = "testPassword",
                Email = "testEmail@test.com"
            };

            // When
            ValidationResult result = _validator.Validate(user);
            var actualId = user.Id;

            // Then
            result.IsValid.Should().BeTrue();
            actualId.Should().Be(expectedId);
            return Task.CompletedTask;
        }

        [Test]
        public Task When_ModifyUser_ValidUsername_Then_Success()
        {
            // Given
            var expectedUsername = "testUsername";
            User user = new()
            {
                Id = Guid.NewGuid().ToString(),
                Name = expectedUsername,
                Password = "testPassword",
                Email = "testEmail@test.com"
            };

            // When
            ValidationResult result = _validator.Validate(user);
            var actualUsername = user.Name;

            // Then
            result.IsValid.Should().BeTrue();
            actualUsername.Should().Be(expectedUsername);
            return Task.CompletedTask;
        }

        [Test]
        public Task When_ModifyUser_ValidPassword_Then_Success()
        {
            // Given
            var expectedPassword = "testPassword";
            User user = new()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "testUsername",
                Password = expectedPassword,
                Email = "testEmail@test.com"
            };

            // When
            ValidationResult result = _validator.Validate(user);
            var actualPassword = user.Password;

            // Then
            result.IsValid.Should().BeTrue();
            actualPassword.Should().Be(expectedPassword);
            return Task.CompletedTask;
        }

        [Test]
        public Task When_ModifyUser_ValidEmail_Then_Success()
        {
            // Given
            var expectedEmail = "testEmail@test.com";
            User user = new()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "testUsername",
                Password = "testPassword",
                Email = expectedEmail
            };

            // When
            ValidationResult result = _validator.Validate(user);
            var actualEmail = user.Email;

            // Then
            result.IsValid.Should().BeTrue();
            actualEmail.Should().Be(expectedEmail);
            return Task.CompletedTask;
        }
    }
}
