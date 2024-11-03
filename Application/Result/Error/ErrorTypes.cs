namespace Application.Result.Error
{
    using Domain.EnumType.Extensions;

    /// <summary>
    /// Enumerates different types of application errors.
    /// </summary>
    public enum ErrorTypes
    {
        /// <summary>
        /// Represents no error.
        /// </summary>
        [EnumMetadata("NONE", "Represents no error.")]
        None,

        /// <summary>
        /// Represents errors related to business logic validation.
        /// </summary>
        [EnumMetadata("BUSINESS_VALIDATION_ERROR", "Represents errors related to business logic validation.")]
        BusinessValidationError,

        /// <summary>
        /// Represents errors when interacting with the database.
        /// </summary>
        [EnumMetadata("DATABASE_ERROR", "Represents errors when interacting with the database.")]
        DatabaseError,

        /// <summary>
        /// Represents errors when interacting with external services.
        /// </summary>
        [EnumMetadata("EXTERNAL_SERVICES_ERROR", "Represents errors when interacting with external services.")]
        ExternalServicesError,

        /// <summary>
        /// Represents any unexpected or unclassified errors.
        /// </summary>
        [EnumMetadata("UNEXPECTED_ERROR", "Represents any unexpected or unclassified errors.")]
        UnexpectedError,

        /// <summary>
        /// Represents errors due to invalid data submission.
        /// </summary>
        [EnumMetadata("DATA_SUBMITTED_INVALID", "Represents errors due to invalid data submission.")] 
        DataSubmittedInvalid,

        /// <summary>
        /// Represents errors due to missing configurations.
        /// </summary>
        [EnumMetadata("CONFIGURATION_MISSING_ERROR", "Represents errors due to missing configurations.")] 
        ConfigurationMissingError,

        /// <summary>
        /// Represents errors due to network issues.
        /// </summary>
        [EnumMetadata("NETWORK_ERROR", "Represents errors due to network issues.")] 
        NetworkError,

        /// <summary>
        /// Represents errors related to user input.
        /// </summary>
        [EnumMetadata("USER_INPUT_ERROR", "Represents errors related to user input.")] 
        UserInputError,

        /// <summary>
        /// Represents errors where a requested resource is not found.
        /// </summary>
        [EnumMetadata("NONE_FOUND_ERROR", "Represents errors where a requested resource is not found.")] 
        NotFoundError,

        /// <summary>
        /// Represents errors related to user authentication.
        /// </summary>
        [EnumMetadata("AUTHENTICATION_ERROR", "Represents errors related to user authentication.")] 
        AuthenticationError,

        /// <summary>
        /// Represents errors related to user authorization or permissions.
        /// </summary>
        [EnumMetadata("AUTHORIZATION_ERROR", "Represents errors related to user authorization or permissions.")] 
        AuthorizationError,

        /// <summary>
        /// Represents errors related to resource allocation or access.
        /// </summary>
        [EnumMetadata("RESOURCE_ERROR", "Represents errors related to resource allocation or access.")] 
        ResourceError,

        /// <summary>
        /// Represents errors due to operation timeouts.
        /// </summary>
        [EnumMetadata("TIMEOUT_ERROR", "Represents errors due to operation timeouts.")] 
        TimeoutError
    }
}
