namespace Infrastructure.Constants
{
    public static class ExceptionMessages
    {
        public const string FailedOccurredDataLayer = "An error occurred in the data layer.";
        public const string FailureUnexpectedError = "An unexpected error occurred. Please check the logs for more details.";
        public const string FailedLogValidException = "The Exception was not submitted.";
        public const string FailedCompress = "The image was not compressed successfully due to an unexpected error.";
        public const string FailedUpload = "The image was not uploaded successfully due to an unexpected error.";
        public const string FailedGlobalException = "Exception Unknowled message:{0} stacktrace {1}";

        public static class Log
        {
            public const string FailureDataSubmittedInvalid = "The message or enitty was not submitted.";
            public const string Success = "The Log validation of the OperationResult was successfully.";
            public const string ErrorFailedLogValidException = "The Exception was not submitted.";
            public const string FailedSerialize = "Failed to serialize entity: {0}";
            public const string UnexpectedNullError = "Null reference encountered: {0}";
            public const string UnknowledgeableError = "An unexpected error occurred: {0}";
        }
        public static class DBResorceProvider
        {
            public const string KeyNotFound = "Resource not found.";
            public const string MultipleResources = "Multiple resources exist with the same key.";
            public const string UnableToRead = "Unable to read the resources file.";
            public const string NoKeysFound = "No resource keys were found.";
        }
        public static class FileResorceProvider
        {
            public const string NoKeysFound = "No resource keys were found.";
            public const string MultipleResources = "Multiple resources exist with the same key.";
            public const string UnableToRead = "Unable to read the resources file.";
        }
        public static class Util
        {
            public const string LogEntry = "Error Message: {0}  StackTrace: {1}";
        }
        public static class JwtHelper
        {
            public const string BearerPrefix = "Bearer ";
            public const string UserData = "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata";
            public const string Whitespace = "Token cannot be null or whitespace.";
            public const string StartBearer = "Token must start with 'Bearer ' prefix.";
            public const string LengthBearer = "The length of the 'bearer' string is less than the 'bearer token' string.";
            public const string InvalidJwt = "Invalid JWT payload format.";
            public const string InvalidBase64Url = "Invalid Base64Url format.";
            public const string Suceesss = "Suceesss";
        }
        public static class ResourceHandler
        {
            public const string CollectionEmpty = "Keys collection cannot be empty.";
            public const string Default = "Default for {0}";
            public const string NoKeysFound = "Resource not found";
        }
        public static class ManagementImage
        {
            public const string General = "General validation operation.";
            public const string ParameterNull = "The parameter is null.";
        }
        public static class GuidValidator
        {
            public const string Invalid = "The submitted value was invalid.";
            public const string Suceesss = "Suceesss";
        }
    }
}
