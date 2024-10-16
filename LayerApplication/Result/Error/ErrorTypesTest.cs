using Application.Result.Error;
using Domain.EnumType;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LayerApplication.Result.Error
{
    [TestFixture]
    public class ErrorTypesTests
    {
        [TestCase(ErrorTypes.None, "NONE", "Represents no error.")]
        [TestCase(ErrorTypes.BusinessValidationError, "BUSINESS_VALIDATION_ERROR", "Represents errors related to business logic validation.")]
        [TestCase(ErrorTypes.DatabaseError, "DATABASE_ERROR", "Represents errors when interacting with the database.")]
        [TestCase(ErrorTypes.ExternalServicesError, "EXTERNAL_SERVICES_ERROR", "Represents errors when interacting with external services.")]
        [TestCase(ErrorTypes.UnexpectedError, "UNEXPECTED_ERROR", "Represents any unexpected or unclassified errors.")]
        [TestCase(ErrorTypes.DataSubmittedInvalid, "DATA_SUBMITTED_INVALID", "Represents errors due to invalid data submission.")]
        [TestCase(ErrorTypes.ConfigurationMissingError, "CONFIGURATION_MISSING_ERROR", "Represents errors due to missing configurations.")]
        [TestCase(ErrorTypes.NetworkError, "NETWORK_ERROR", "Represents errors due to network issues.")]
        [TestCase(ErrorTypes.UserInputError, "USER_INPUT_ERROR", "Represents errors related to user input.")]
        [TestCase(ErrorTypes.NotFoundError, "NONE_FOUND_ERROR", "Represents errors where a requested resource is not found.")]
        [TestCase(ErrorTypes.AuthenticationError, "AUTHENTICATION_ERROR", "Represents errors related to user authentication.")]
        [TestCase(ErrorTypes.AuthorizationError, "AUTHORIZATION_ERROR", "Represents errors related to user authorization or permissions.")]
        [TestCase(ErrorTypes.ResourceError, "RESOURCE_ERROR", "Represents errors related to resource allocation or access.")]
        [TestCase(ErrorTypes.TimeoutError, "TIMEOUT_ERROR", "Represents errors due to operation timeouts.")]
        public void ErrorTypes_Should_Have_Correct_EnumMetadata(ErrorTypes errorType, string expectedName, string expectedDescription)
        {
            // Act
            var fieldInfo = errorType.GetType().GetField(errorType.ToString());
            var attribute = fieldInfo.GetCustomAttribute<EnumMetadataAttribute>();

            // Assert
            attribute.Should().NotBeNull();
            attribute.Name.Should().Be(expectedName);
            attribute.Description.Should().Be(expectedDescription);
        }

        [Test]
        public void EnumMetadata_Should_Be_Applied_To_All_ErrorTypes_Values()
        {
            // Given
            var errorTypes = Enum.GetValues(typeof(ErrorTypes));

            // Act & Assert
            foreach (ErrorTypes errorType in errorTypes)
            {
                var fieldInfo = errorType.GetType().GetField(errorType.ToString());
                var attribute = fieldInfo.GetCustomAttribute<EnumMetadataAttribute>();
                attribute.Should().NotBeNull();
            }
        }
    }

}
