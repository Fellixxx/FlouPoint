namespace FlouPoint.Application.Test.Infrastructure.Repository
{
    using global::Domain.EnumType;
    using FluentAssertions;
    using global::Application.Result.Error;

    [TestFixture]
    public class HelperErrorTypesExtensionsTests
    {
        [Test]
        public Task When_ErrorType_None_Then_StringIs_NONE()
        {
            // Given
            ErrorTypes error = ErrorTypes.None;
            var expectedString = "NONE";

            // When
            var actualString = error.GetCustomName() ?? string.Empty;

            // Then
            actualString.Should().Be(expectedString);
            return Task.CompletedTask;
        }

        [Test]
        public Task When_ErrorType_DatabaseError_Then_DescriptionIsCorrect()
        {
            // Given
            ErrorTypes error = ErrorTypes.DatabaseError;
            var expectedDescription = "Represents errors when interacting with the database.";

            // When
            var actualDescription = error.GetDescription();

            // Then
            actualDescription.Should().Be(expectedDescription);
            return Task.CompletedTask;
        }

        // Add additional tests here for each ErrorType
    }
}
