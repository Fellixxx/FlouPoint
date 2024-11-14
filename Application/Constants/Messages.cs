namespace Application.Constants
{
    /// <summary>
    /// Contains message constants used throughout the application.
    /// This class is a centralized location for storing string constants 
    /// that are used in various parts of the application to ensure consistency
    /// and avoid magic strings in the code.
    /// </summary>
    public static class Messages
    {
        /// <summary>
        /// Messages related to invalid operation scenarios.
        /// This class provides constants used to describe invalid operations
        /// and their specific errors.
        /// </summary>
        public static class InvalidOperation
        {
            /// <summary>
            /// Message indicating that a provided message or parameter cannot be null, empty, or whitespace.
            /// </summary>
            public const string NullMessage = "The 'message' parameter cannot be null, empty, or whitespace.";
        }

        /// <summary>
        /// Messages pertaining to operations that a user can perform.
        /// Contains constants that are related to the state or results of particular operations.
        /// </summary>
        public static class Operation
        {
            /// <summary>
            /// Indicates an invalid operation when a method is used incorrectly, particularly when an operation is expected to fail.
            /// </summary>
            public const string InvalidOperation = "This method can only be used if the value of IsSuccessful is false.";
        }

        /// <summary>
        /// Messages related to the strategies used in performing operations.
        /// Used for providing detailed error messages in strategies used for operation handling.
        /// </summary>
        public static class OperationStrategy
        {
            /// <summary>
            /// Template error message indicating a problem with the 'message' parameter. 
            /// The placeholder '{0}' can be replaced with specific context or details about the error.
            /// </summary>
            public const string ErrorMessage = "{0}: The 'message' parameter cannot be null, empty, or whitespace.";
        }

        /// <summary>
        /// Messages related to resource providers within the application.
        /// Provides constants used for indicating status or issues with resources.
        /// </summary>
        public static class IResorcesProvider
        {
            /// <summary>
            /// Message indicating that a requested resource could not be found.
            /// Useful for error handling when a lookup or retrieval operation fails.
            /// </summary>
            public const string ResourceNotFound = "Resource not found";
        }
    }
}