namespace FlouPoint.Test.Infrastructure.Repository
{
    using global::Domain.EnumType;
    using FluentAssertions;
    using global::Application.Result.Error;

    /// <summary>
    /// Unit tests for the HelperErrorTypesExtensions class, which tests functionality
    /// related to the ErrorTypes enumeration used within the application.
    /// </summary>
    [TestFixture]
    public class HelperErrorTypesExtensionsTests
    {
        /// <summary>
        /// Tests that when the ErrorType is 'None', the corresponding string representation
        /// is "NONE".
        /// </summary>
        [Test]
        public Task When_ErrorType_None_Then_StringIs_NONE()
        {
            // Given
            // Initializing an ErrorTypes variable with a value of 'None'.
            ErrorTypes error = ErrorTypes.None;
            // Expected string representation for the 'None' ErrorType.
            var expectedString = "NONE";
            // When
            // Obtaining the custom name string for the ErrorType value.
            var actualString = error.GetCustomName() ?? string.Empty;
            // Then
            // Verifying that the obtained string matches the expected value.
            actualString.Should().Be(expectedString);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Tests that when the ErrorType is 'DatabaseError', the correct error 
        /// description is returned.
        /// </summary>
        [Test]
        public Task When_ErrorType_DatabaseError_Then_DescriptionIsCorrect()
        {
            // Given
            // Initializing an ErrorTypes variable with a value of 'DatabaseError'.
            ErrorTypes error = ErrorTypes.DatabaseError;
            // Expected description for the 'DatabaseError' ErrorType.
            var expectedDescription = "Represents errors when interacting with the database.";
            // When
            // Obtaining the description string for the ErrorType value.
            var actualDescription = error.GetDescription();
            // Then
            // Verifying that the obtained description matches the expected value.
            actualDescription.Should().Be(expectedDescription);
            return Task.CompletedTask;
        }
    // Placeholders for additional tests can be added here. Each test should check
    // a different ErrorType value and assert the correct custom behaviors or properties
    // associated with them.
    }
}