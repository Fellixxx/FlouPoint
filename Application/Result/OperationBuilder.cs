namespace Application.Result
{
    using Application.Result.Error;

    public static class OperationBuilder<T>
    {

        /// <summary>
        /// Creates a failed operation result for a business validation scenario."
        /// </summary>
        /// <param name="message">The message</param>
        /// <returns>The operation result</returns>
        public static OperationResult<T> FailureBusinessValidation(string? message)
        {
            ValidateMessage(message);
            return OperationResult<T>.Failure(message, ErrorTypes.BusinessValidationError);
        }


        /// <summary>
        /// Creates a failed operation result for a configuration missing error scenario."
        /// </summary>
        /// <param name="message">The message</param>
        /// <returns>The operation result</returns>
        public static OperationResult<T> FailureConfigurationMissingError(string message)
        {
            ValidateMessage(message);
            return OperationResult<T>.Failure(message, ErrorTypes.ConfigurationMissingError);
        }

        /// <summary>
        /// Creates a failed operation result for a database scenario."
        /// </summary>
        /// <param name="message">The message</param>
        /// <returns>The operation result</returns>
        public static OperationResult<T> FailureDatabase(string message)
        {
            ValidateMessage(message);
            return OperationResult<T>.Failure(message, ErrorTypes.DatabaseError);
        }

        /// <summary>
        /// Creates a failed operation result for a data sumitted invalid scenario."
        /// </summary>
        /// <param name="message">The message</param>
        /// <returns>The operation result</returns>
        public static OperationResult<T> FailureDataSubmittedInvalid(string message)
        {
            ValidateMessage(message);
            return OperationResult<T>.Failure(message, ErrorTypes.DataSubmittedInvalid);
        }

        /// <summary>
        /// Creates a failed operation result for a external service scenario."
        /// </summary>
        /// <param name="message">The message</param>
        /// <returns>The operation result</returns>
        public static OperationResult<T> FailureExtenalService(string message)
        {
            ValidateMessage(message);
            return OperationResult<T>.Failure(message, ErrorTypes.ExternalServicesError);
        }

        /// <summary>
        /// Creates a failed operation result for a unexpected errror scenario."
        /// </summary>
        /// <param name="message">The message</param>
        /// <returns>The operation result</returns>
        public static OperationResult<T> FailureUnexpectedError(string message)
        {
            ValidateMessage(message);
            return OperationResult<T>.Failure(message, ErrorTypes.UnexpectedError);
        }

        /// <summary>
        /// Creates a failed operation result for a unexpected errror scenario."
        /// </summary>
        /// <param name="message">The message</param>
        /// <returns>The operation result</returns>
        public static OperationResult<T> FailureNetworkError(string message)
        {
            ValidateMessage(message);
            return OperationResult<T>.Failure(message, ErrorTypes.NetworkError);
        }

        private static void ValidateMessage(string? message)
        {
            var errorMessage = $"{nameof(ValidateMessage)}: The 'message' parameter cannot be null, empty, or whitespace.";
            Validate(message, errorMessage);
            return;
        }

        private static void Validate(string? field, string errorMessage)
        {
            if (field == null)
            {
                throw new ArgumentNullException(nameof(field), errorMessage);
            }
            else if (string.IsNullOrWhiteSpace(field))
            {
                throw new ArgumentException(errorMessage, nameof(field));
            }
        }
    }
}