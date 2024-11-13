namespace Infrastructure.Constants
{
    /// <summary>
    /// Provides a collection of string constants used for defining error and status messages across the infrastructure layer.
    /// </summary>
    public static class Message
    {
        // General Errors
        /// <summary>
        /// Error message indicating a problem occurred in the data layer.
        /// </summary>
        public const string ErrorOccurredDataLayer = "An error occurred in the data layer.";
        /// <summary>
        /// Error message for an unexpected error with a suggestion to check logs for details.
        /// </summary>
        public const string UnexpectedError = "An unexpected error occurred. Please check the logs for more details.";
        /// <summary>
        /// Error message for when log validation fails.
        /// </summary>
        public const string FailedLogValidation = "The exception was not submitted.";
        /// <summary>
        /// Error message indicating failure in image compression.
        /// </summary>
        public const string FailedToCompressImage = "The image was not compressed successfully due to an unexpected error.";
        /// <summary>
        /// Error message indicating failure in image upload.
        /// </summary>
        public const string FailedToUploadImage = "The image was not uploaded successfully due to an unexpected error.";
        /// <summary>
        /// Error message template for unknown exceptions, includes placeholders for message and stack trace.
        /// </summary>
        public const string UnknownException = "Unknown exception message: {0} StackTrace: {1}";
        /// <summary>
        /// Provides constants representing log messages used within the application.
        /// </summary>
        public static class Log
        {
            /// <summary>
            /// Error message for when invalid data is submitted and not logged.
            /// </summary>
            public const string InvalidDataSubmitted = "The message or entity was not submitted.";
            /// <summary>
            /// Status message indicating successful validation of a log entry.
            /// </summary>
            public const string ValidationSuccess = "The log validation of the OperationResult was successful.";
            /// <summary>
            /// Error message for a failure to log exception data.
            /// </summary>
            public const string FailedException = "The exception was not submitted.";
            /// <summary>
            /// Error message when serialization of an entity fails, with a placeholder for the entity name.
            /// </summary>
            public const string FailedToSerialize = "Failed to serialize entity: {0}";
            /// <summary>
            /// Error message indicating a null reference was encountered, includes a placeholder for the reference.
            /// </summary>
            public const string NullReference = "Null reference encountered: {0}";
            /// <summary>
            /// Template for a generic unexpected error message that includes a placeholder for details.
            /// </summary>
            public const string UnknownError = "An unexpected error occurred: {0}";
        }

        /// <summary>
        /// Contains messages related to the resource provider operations.
        /// </summary>
        public static class ResourceProvider
        {
            /// <summary>
            /// Message indicating a resource key was not found.
            /// </summary>
            public const string KeyNotFound = "Resource not found.";
            /// <summary>
            /// Message indicating multiple resources share the same key.
            /// </summary>
            public const string MultipleWithSameKey = "Multiple resources exist with the same key.";
            /// <summary>
            /// Error message for when a resource file cannot be read.
            /// </summary>
            public const string UnableToReadFile = "Unable to read the resource file.";
            /// <summary>
            /// Message indicating no resource keys were discovered.
            /// </summary>
            public const string NoKeysFound = "No resource keys were found.";
        }

        /// <summary>
        /// Contains utility messages and formats.
        /// </summary>
        public static class Utility
        {
            /// <summary>
            /// Format for log entries, includes placeholders for the error message and stack trace.
            /// </summary>
            public const string LogEntryFormat = "Error Message: {0}  StackTrace: {1}";
        }

        /// <summary>
        /// Provides constants related to JWT handling and validation.
        /// </summary>
        public static class JwtHelper
        {
            /// <summary>
            /// Prefix string indicating the start of a bearer token.
            /// </summary>
            public const string BearerPrefix = "Bearer ";
            /// <summary>
            /// URL representing user data claims.
            /// </summary>
            public const string UserDataClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata";
            /// <summary>
            /// Error message implying the token cannot be empty or whitespace.
            /// </summary>
            public const string TokenCannotBeWhitespace = "Token cannot be null or whitespace.";
            /// <summary>
            /// Error message indicating the token must start with 'Bearer '.
            /// </summary>
            public const string TokenMustStartWithBearer = "Token must start with 'Bearer ' prefix.";
            /// <summary>
            /// Error message for an incorrect length of bearer strings in a token.
            /// </summary>
            public const string InvalidBearerTokenLength = "The length of the 'Bearer' string is less than the 'Bearer token' string.";
            /// <summary>
            /// Error message specifying an invalid JWT payload format.
            /// </summary>
            public const string InvalidJwtPayloadFormat = "Invalid JWT payload format.";
            /// <summary>
            /// Error message for invalid Base64Url formatted input.
            /// </summary>
            public const string InvalidBase64UrlFormat = "Invalid Base64Url format.";
            /// <summary>
            /// Standard success message.
            /// </summary>
            public const string Success = "Success";
        }

        /// <summary>
        /// Contains messages related to resource handling.
        /// </summary>
        public static class ResourceHandler
        {
            /// <summary>
            /// Error message indicating the keys collection should not be empty.
            /// </summary>
            public const string EmptyKeysCollection = "Keys collection cannot be empty.";
            /// <summary>
            /// Format string for default resource status, includes a placeholder for resource specifics.
            /// </summary>
            public const string DefaultResource = "Default for {0}";
            /// <summary>
            /// Error message indicating the specified resource could not be found.
            /// </summary>
            public const string ResourceNotFound = "Resource not found.";
        }

        /// <summary>
        /// Contains constants for managing image-related operations.
        /// </summary>
        public static class ImageManagement
        {
            /// <summary>
            /// Message indicating a general validation operation for images.
            /// </summary>
            public const string GeneralValidation = "General validation operation.";
            /// <summary>
            /// Error message for when a required parameter is null.
            /// </summary>
            public const string ParameterIsNull = "The parameter is null.";
        }

        /// <summary>
        /// Provides constants used in GUID validation processes.
        /// </summary>
        public static class GuidValidator
        {
            /// <summary>
            /// Error message indicating the provided GUID is invalid.
            /// </summary>
            public const string InvalidGuid = "The submitted value was invalid.";
            /// <summary>
            /// Status message signaling a successful operation.
            /// </summary>
            public const string Success = "Success";
        }
    }
}