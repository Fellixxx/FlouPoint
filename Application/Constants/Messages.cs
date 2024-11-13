using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Constants
{
    /// <summary>
    /// Contains message constants used throughout the application.
    /// </summary>
    public static class Messages
    {
        /// <summary>
        /// Messages related to Enum extensions.
        /// </summary>
        public static class InvalidOperation
        {
            /// <summary>
            /// Represents an unknown value.
            /// </summary>
            public const string NullMessage = "The 'message' parameter cannot be null, empty, or whitespace.";
        }
        public static class Operation
        {
            /// <summary>
            /// Represents an unknown value.
            /// </summary>
            public const string InvalidOperation = "This method can only be used if the value of IsSuccessful is false.";
        }
        public static class OperationStrategy
        {
            /// <summary>
            /// Represents an unknown value.
            /// </summary>
            public const string ErrorMessage = "{0}: The 'message' parameter cannot be null, empty, or whitespace.";
        }
        public static class IResorcesProvider
        {
            /// <summary>
            /// Represents an unknown value.
            /// </summary>
            public const string ResourceNotFound = "Resource not found";
        }
        
    }
}
