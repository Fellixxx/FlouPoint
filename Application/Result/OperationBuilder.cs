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
            message = message ?? throw new ArgumentNullException(nameof(message));
            return OperationResult<T>.Failure(message, ErrorTypes.BusinessValidationError);
        }

        /// <summary>
        /// Creates a failed operation result for a configuration missing error scenario."
        /// </summary>
        /// <param name="message">The message</param>
        /// <returns>The operation result</returns>
        public static OperationResult<T> FailureConfigurationMissingError(string message)
        {
            return OperationResult<T>.Failure(message, ErrorTypes.ConfigurationMissingError);
        }

        /// <summary>
        /// Creates a failed operation result for a database scenario."
        /// </summary>
        /// <param name="message">The message</param>
        /// <returns>The operation result</returns>
        public static OperationResult<T> FailureDatabase(string message)
        {
            return OperationResult<T>.Failure(message, ErrorTypes.DatabaseError);
        }

        /// <summary>
        /// Creates a failed operation result for a data sumitted invalid scenario."
        /// </summary>
        /// <param name="message">The message</param>
        /// <returns>The operation result</returns>
        public static OperationResult<T> FailureDataSubmittedInvalid(string message)
        {
            return OperationResult<T>.Failure(message, ErrorTypes.DataSubmittedInvalid);
        }

        /// <summary>
        /// Creates a failed operation result for a external service scenario."
        /// </summary>
        /// <param name="message">The message</param>
        /// <returns>The operation result</returns>
        public static OperationResult<T> FailureExtenalService(string message)
        {
            if(string.IsNullOrEmpty(message)) 
            { 
                throw new ArgumentNullException(nameof(message)); 
            }
            return OperationResult<T>.Failure(message, ErrorTypes.ExternalServicesError);
        }

        /// <summary>
        /// Creates a failed operation result for a unexpected errror scenario."
        /// </summary>
        /// <param name="message">The message</param>
        /// <returns>The operation result</returns>
        public static OperationResult<T> FailureUnexpectedError(string message)
        {
            return OperationResult<T>.Failure(message, ErrorTypes.UnexpectedError);
        }

        /// <summary>
        /// Creates a failed operation result for a unexpected errror scenario."
        /// </summary>
        /// <param name="message">The message</param>
        /// <returns>The operation result</returns>
        public static OperationResult<T> FailureNetworkError(string message)
        {
            return OperationResult<T>.Failure(message, ErrorTypes.NetworkError);
        }
    }
}