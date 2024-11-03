using Application.Result.Error;
using Domain.EnumType.Extensions;
using System.Reflection;

namespace Application.Test.Result.Error
{
    [TestClass]
    public class ErrorTypesTest
    {
        [DataTestMethod]
        [DataRow(ErrorTypes.None, "NONE", "Represents no error.")]
        [DataRow(ErrorTypes.BusinessValidationError, "BUSINESS_VALIDATION_ERROR", "Represents errors related to business logic validation.")]
        [DataRow(ErrorTypes.DatabaseError, "DATABASE_ERROR", "Represents errors when interacting with the database.")]
        [DataRow(ErrorTypes.ExternalServicesError, "EXTERNAL_SERVICES_ERROR", "Represents errors when interacting with external services.")]
        [DataRow(ErrorTypes.UnexpectedError, "UNEXPECTED_ERROR", "Represents any unexpected or unclassified errors.")]
        [DataRow(ErrorTypes.DataSubmittedInvalid, "DATA_SUBMITTED_INVALID", "Represents errors due to invalid data submission.")]
        [DataRow(ErrorTypes.ConfigurationMissingError, "CONFIGURATION_MISSING_ERROR", "Represents errors due to missing configurations.")]
        [DataRow(ErrorTypes.NetworkError, "NETWORK_ERROR", "Represents errors due to network issues.")]
        [DataRow(ErrorTypes.UserInputError, "USER_INPUT_ERROR", "Represents errors related to user input.")]
        [DataRow(ErrorTypes.NotFoundError, "NONE_FOUND_ERROR", "Represents errors where a requested resource is not found.")]
        [DataRow(ErrorTypes.AuthenticationError, "AUTHENTICATION_ERROR", "Represents errors related to user authentication.")]
        [DataRow(ErrorTypes.AuthorizationError, "AUTHORIZATION_ERROR", "Represents errors related to user authorization or permissions.")]
        [DataRow(ErrorTypes.ResourceError, "RESOURCE_ERROR", "Represents errors related to resource allocation or access.")]
        [DataRow(ErrorTypes.TimeoutError, "TIMEOUT_ERROR", "Represents errors due to operation timeouts.")]
        public void ErrorTypes_Should_Have_Correct_EnumMetadata(ErrorTypes errorType, string expectedName, string expectedDescription)
        {
            // Act
            var fieldInfo = errorType.GetType().GetField(errorType.ToString());
            var attribute = fieldInfo.GetCustomAttribute<EnumMetadataAttribute>();

            // Assert
            Assert.IsNotNull(attribute);
            Assert.AreEqual(attribute.Name, expectedName);
            Assert.AreEqual(attribute.Description, expectedDescription);
        }

        [TestMethod]
        public void EnumMetadata_Should_Be_Applied_To_All_ErrorTypes_Values()
        {
            // Given
            var errorTypes = Enum.GetValues(typeof(ErrorTypes));

            // Act & Assert
            foreach (ErrorTypes errorType in errorTypes)
            {
                var fieldInfo = errorType.GetType().GetField(errorType.ToString());
                var attribute = fieldInfo.GetCustomAttribute<EnumMetadataAttribute>();
                Assert.IsNotNull(attribute);
            }
        }
    }
}
