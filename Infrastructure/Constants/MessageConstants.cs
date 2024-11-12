namespace Infrastructure.Constants
{
    public static class MessageConstants
    {
        // General Errors
        public const string ErrorOccurredDataLayer = "An error occurred in the data layer.";
        public const string UnexpectedError = "An unexpected error occurred. Please check the logs for more details.";
        public const string FailedLogValidation = "The exception was not submitted.";
        public const string FailedToCompressImage = "The image was not compressed successfully due to an unexpected error.";
        public const string FailedToUploadImage = "The image was not uploaded successfully due to an unexpected error.";
        public const string UnknownException = "Unknown exception message: {0} StackTrace: {1}";

        public static class Log
        {
            public const string InvalidDataSubmitted = "The message or entity was not submitted.";
            public const string ValidationSuccess = "The log validation of the OperationResult was successful.";
            public const string FailedExceptionSubmission = "The exception was not submitted.";
            public const string FailedToSerializeEntity = "Failed to serialize entity: {0}";
            public const string NullReferenceEncountered = "Null reference encountered: {0}";
            public const string UnknownError = "An unexpected error occurred: {0}";
        }

        public static class ResourceProvider
        {
            public const string KeyNotFound = "Resource not found.";
            public const string MultipleResourcesWithSameKey = "Multiple resources exist with the same key.";
            public const string UnableToReadResourceFile = "Unable to read the resource file.";
            public const string NoResourceKeysFound = "No resource keys were found.";
        }

        public static class Utility
        {
            public const string LogEntryFormat = "Error Message: {0}  StackTrace: {1}";
        }

        public static class JwtHelper
        {
            public const string BearerPrefix = "Bearer ";
            public const string UserDataClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata";
            public const string TokenCannotBeWhitespace = "Token cannot be null or whitespace.";
            public const string TokenMustStartWithBearer = "Token must start with 'Bearer ' prefix.";
            public const string InvalidBearerTokenLength = "The length of the 'Bearer' string is less than the 'Bearer token' string.";
            public const string InvalidJwtPayloadFormat = "Invalid JWT payload format.";
            public const string InvalidBase64UrlFormat = "Invalid Base64Url format.";
            public const string Success = "Success";
        }

        public static class ResourceHandler
        {
            public const string EmptyKeysCollection = "Keys collection cannot be empty.";
            public const string DefaultResource = "Default for {0}";
            public const string ResourceNotFound = "Resource not found.";
        }

        public static class ImageManagement
        {
            public const string GeneralValidation = "General validation operation.";
            public const string ParameterIsNull = "The parameter is null.";
        }

        public static class GuidValidator
        {
            public const string InvalidGuid = "The submitted value was invalid.";
            public const string Success = "Success";
        }
    }
}
