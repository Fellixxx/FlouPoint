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
        /// Tests that the correct string representation is returned 
        /// for various ErrorType values.
        /// </summary>
        [TestCase(ErrorTypes.None, "NONE")]
        [TestCase(ErrorTypes.DatabaseError, "DATABASE_ERROR")]
        [TestCase(ErrorTypes.NetworkError, "NETWORK_ERROR")] // Add more cases as needed
        public void When_ErrorType_Then_StringIsCorrect(ErrorTypes errorType, string expectedString)
        {
            // Act
            var actualString = errorType.GetCustomName() ?? string.Empty;
            // Assert
            actualString.Should().Be(expectedString, $"because the custom name of {errorType} should be {expectedString}");
        }

        /// <summary>
        /// Tests that the correct description is returned 
        /// for various ErrorType values.
        /// </summary>
        [TestCase(ErrorTypes.None, "No error.")]
        [TestCase(ErrorTypes.DatabaseError, "Represents errors when interacting with the database.")]
        [TestCase(ErrorTypes.NetworkError, "Represents errors occurring during network communication.")] // Add more cases as needed
        public void When_ErrorType_Then_DescriptionIsCorrect(ErrorTypes errorType, string expectedDescription)
        {
            // Act
            var actualDescription = errorType.GetDescription() ?? string.Empty;
            // Assert
            actualDescription.Should().Be(expectedDescription, $"because the description of {errorType} should be {expectedDescription}");
        }
    // Helper method to provide additional enum values and their expected properties
    // can be placed here if the value set becomes large.
    }
}