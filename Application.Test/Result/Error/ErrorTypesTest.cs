using Application.Result.Error;
using Domain.EnumType.Extensions;
using System.Reflection;

namespace Application.Test.Result.Error
{
    /// <summary>
    /// Unit tests for validating the <see cref = "ErrorTypes"/> enum and associated <see cref = "EnumMetadata"/> attributes.
    /// </summary>
    [TestClass]
    public class ErrorTypesTest
    {
        /// <summary>
        /// Tests that each <see cref = "ErrorTypes"/> enum value has the correct <see cref = "EnumMetadata"/> attribute with expected name and description.
        /// </summary>
        /// <param name = "errorType">The <see cref = "ErrorTypes"/> enum value to test.</param>
        /// <param name = "expectedName">The expected name in the <see cref = "EnumMetadata"/> attribute.</param>
        /// <param name = "expectedDescription">The expected description in the <see cref = "EnumMetadata"/> attribute.</param>
        [DataTestMethod]
        [DataRow(ErrorTypes.None, "NONE", "Represents no error.")]
        [DataRow(ErrorTypes.BusinessValidation, "BUSINESS_VALIDATION_ERROR", "Represents errors related to business logic validation.")]
        [DataRow(ErrorTypes.Database, "DATABASE_ERROR", "Represents errors when interacting with the database.")]
        [DataRow(ErrorTypes.ExternalService, "EXTERNAL_SERVICES_ERROR", "Represents errors when interacting with external services.")]
        [DataRow(ErrorTypes.Unexpected, "UNEXPECTED_ERROR", "Represents any unexpected or unclassified errors.")]
        [DataRow(ErrorTypes.InvalidData, "DATA_SUBMITTED_INVALID", "Represents errors due to invalid data submission.")]
        [DataRow(ErrorTypes.ConfigMissing, "CONFIGURATION_MISSING_ERROR", "Represents errors due to missing configurations.")]
        [DataRow(ErrorTypes.Network, "NETWORK_ERROR", "Represents errors due to network issues.")]
        [DataRow(ErrorTypes.UserInput, "USER_INPUT_ERROR", "Represents errors related to user input.")]
        [DataRow(ErrorTypes.NotFound, "NONE_FOUND_ERROR", "Represents errors where a requested resource is not found.")]
        [DataRow(ErrorTypes.Authentication, "AUTHENTICATION_ERROR", "Represents errors related to user authentication.")]
        [DataRow(ErrorTypes.Authorization, "AUTHORIZATION_ERROR", "Represents errors related to user authorization or permissions.")]
        [DataRow(ErrorTypes.Resource, "RESOURCE_ERROR", "Represents errors related to resource allocation or access.")]
        [DataRow(ErrorTypes.Timeout, "TIMEOUT_ERROR", "Represents errors due to operation timeouts.")]
        public void ErrorTypes_Should_Have_Correct_EnumMetadata(ErrorTypes errorType, string expectedName, string expectedDescription)
        {
            // Act
            var fieldInfo = errorType.GetType().GetField(errorType.ToString());
            var attribute = fieldInfo.GetCustomAttribute<EnumMetadata>();
            // Assert
            Assert.IsNotNull(attribute, $"Enum value {errorType} is missing EnumMetadataAttribute.");
            Assert.AreEqual(expectedName, attribute.Name);
            Assert.AreEqual(expectedDescription, attribute.Description);
        }

        /// <summary>
        /// Verifies that each <see cref = "ErrorTypes"/> enum value has an associated <see cref = "EnumMetadata"/> attribute.
        /// </summary>
        [TestMethod]
        public void EnumMetadata_Should_Be_Applied_To_All_ErrorTypes_Values()
        {
            // Given
            var errorTypes = Enum.GetValues(typeof(ErrorTypes));
            // Act & Assert
            foreach (ErrorTypes errorType in errorTypes)
            {
                var fieldInfo = errorType.GetType().GetField(errorType.ToString());
                var attribute = fieldInfo.GetCustomAttribute<EnumMetadata>();
                Assert.IsNotNull(attribute, $"Enum value {errorType} is missing EnumMetadataAttribute.");
            }
        }

        /// <summary>
        /// Tests that the <see cref = "EnumMetadata"/> attribute constructor throws <see cref = "ArgumentNullException"/> when the name is null.
        /// </summary>
        [TestMethod]
        public void EnumMetadataAttribute_Should_Throw_ArgumentNullException_When_Name_Is_Null()
        {
            // Arrange
            string name = null;
            string description = "Valid description";
            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new EnumMetadata(name, description));
        }

        /// <summary>
        /// Tests that the <see cref = "EnumMetadata"/> attribute constructor throws <see cref = "ArgumentNullException"/> when the name is empty.
        /// </summary>
        [TestMethod]
        public void EnumMetadataAttribute_Should_Throw_ArgumentNullException_When_Name_Is_Empty()
        {
            // Arrange
            string name = "";
            string description = "Valid description";
            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new EnumMetadata(name, description));
        }

        /// <summary>
        /// Tests that the <see cref = "EnumMetadata"/> attribute constructor throws <see cref = "ArgumentNullException"/> when the name is whitespace.
        /// </summary>
        [TestMethod]
        public void EnumMetadataAttribute_Should_Throw_ArgumentNullException_When_Name_Is_Whitespace()
        {
            // Arrange
            string name = "   ";
            string description = "Valid description";
            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new EnumMetadata(name, description));
        }

        /// <summary>
        /// Tests that the <see cref = "EnumMetadata"/> attribute constructor throws <see cref = "ArgumentNullException"/> when the description is null.
        /// </summary>
        [TestMethod]
        public void EnumMetadataAttribute_Should_Throw_ArgumentNullException_When_Description_Is_Null()
        {
            // Arrange
            string name = "Valid name";
            string description = null;
            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new EnumMetadata(name, description));
        }

        /// <summary>
        /// Tests that the <see cref = "EnumMetadata"/> attribute constructor throws <see cref = "ArgumentNullException"/> when the description is empty.
        /// </summary>
        [TestMethod]
        public void EnumMetadataAttribute_Should_Throw_ArgumentNullException_When_Description_Is_Empty()
        {
            // Arrange
            string name = "Valid name";
            string description = "";
            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new EnumMetadata(name, description));
        }

        /// <summary>
        /// Tests that the <see cref = "EnumMetadata"/> attribute constructor throws <see cref = "ArgumentNullException"/> when the description is whitespace.
        /// </summary>
        [TestMethod]
        public void EnumMetadataAttribute_Should_Throw_ArgumentNullException_When_Description_Is_Whitespace()
        {
            // Arrange
            string name = "Valid name";
            string description = "   ";
            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new EnumMetadata(name, description));
        }

        /// <summary>
        /// Ensures that all names in the <see cref = "EnumMetadata"/> attributes of <see cref = "ErrorTypes"/> enum are unique.
        /// </summary>
        [TestMethod]
        public void ErrorTypes_Should_Have_Unique_Names_In_Metadata()
        {
            // Arrange
            var names = new HashSet<string>();
            var errorTypes = Enum.GetValues(typeof(ErrorTypes));
            // Act & Assert
            foreach (ErrorTypes errorType in errorTypes)
            {
                var fieldInfo = errorType.GetType().GetField(errorType.ToString());
                var attribute = fieldInfo.GetCustomAttribute<EnumMetadata>();
                Assert.IsNotNull(attribute, $"Enum value {errorType} is missing EnumMetadataAttribute.");
                bool added = names.Add(attribute.Name);
                Assert.IsTrue(added, $"Duplicate Name '{attribute.Name}' found in EnumMetadataAttribute of {errorType}.");
            }
        }

        /// <summary>
        /// Ensures that all descriptions in the <see cref = "EnumMetadata"/> attributes of <see cref = "ErrorTypes"/> enum are unique.
        /// </summary>
        [TestMethod]
        public void ErrorTypes_Should_Have_Unique_Descriptions_In_Metadata()
        {
            // Arrange
            var descriptions = new HashSet<string>();
            var errorTypes = Enum.GetValues(typeof(ErrorTypes));
            // Act & Assert
            foreach (ErrorTypes errorType in errorTypes)
            {
                var fieldInfo = errorType.GetType().GetField(errorType.ToString());
                var attribute = fieldInfo.GetCustomAttribute<EnumMetadata>();
                Assert.IsNotNull(attribute, $"Enum value {errorType} is missing EnumMetadataAttribute.");
                bool added = descriptions.Add(attribute.Description);
                Assert.IsTrue(added, $"Duplicate Description '{attribute.Description}' found in EnumMetadataAttribute of {errorType}.");
            }
        }
    }
}