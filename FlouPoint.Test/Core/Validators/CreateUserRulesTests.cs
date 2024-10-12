namespace FlouPoint.Test.Infrastructure.Repository
{
    using FluentAssertions;
    using FluentValidation.Results;

    [TestFixture]
    public class CreateUserRulesTests
    {
        private CreateUserRules _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new CreateUserRules();
        }

        [Test]
        public void Should_Have_Error_When_Name_Is_Null()
        {
            // Given
            var user = new User
            {
                Name = null,
                Password = "testPassword",
                Email = "testEmail@test.com"
            };

            // When
            ValidationResult result = _validator.Validate(user);

            // Then
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(r => r.PropertyName == "Name");
        }

        [Test]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            // Given
            User user = new User
            {
                Name = "",
                Password = "testPassword",
                Email = "testEmail@test.com"
            };

            // When
            ValidationResult result = _validator.Validate(user);

            // Then
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(r => r.PropertyName == "Name");
        }

        [Test]
        public void Should_Have_Error_When_Username_Is_Too_Short()
        {
            // Given
            User user = new User
            {
                Name = "12345",
                Password = "testPassword",
                Email = "testEmail@test.com"
            };

            // When
            ValidationResult result = _validator.Validate(user);

            // Then
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(r => r.PropertyName == "Name");
        }

        // Similar tests should be created for Password and Email
    }
}
