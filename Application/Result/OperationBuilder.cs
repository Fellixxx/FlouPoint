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
        public static OperationResult<T> FailBusiness(string? message)
        {
            ValidateMessage(message);
            return OperationResult<T>.Failure(message, ErrorTypes.BusinessValidation);
        }


        /// <summary>
        /// Creates a failed operation result for a configuration missing error scenario."
        /// </summary>
        /// <param name="message">The message</param>
        /// <returns>The operation result</returns>
        public static OperationResult<T> FailConfig(string message)
        {
            ValidateMessage(message);
            return OperationResult<T>.Failure(message, ErrorTypes.ConfigMissing);
        }

        /// <summary>
        /// Creates a failed operation result for a database scenario."
        /// </summary>
        /// <param name="message">The message</param>
        /// <returns>The operation result</returns>
        public static OperationResult<T> FailDatabase(string message)
        {
            ValidateMessage(message);
            return OperationResult<T>.Failure(message, ErrorTypes.Database);
        }

        /// <summary>
        /// Creates a failed operation result for a data sumitted invalid scenario."
        /// </summary>
        /// <param name="message">The message</param>
        /// <returns>The operation result</returns>
        public static OperationResult<T> FailInvalidData(string message)
        {
            ValidateMessage(message);
            return OperationResult<T>.Failure(message, ErrorTypes.InvalidData);
        }

        /// <summary>
        /// Creates a failed operation result for a external service scenario."
        /// </summary>
        /// <param name="message">The message</param>
        /// <returns>The operation result</returns>
        public static OperationResult<T> FailExternal(string message)
        {
            ValidateMessage(message);
            return OperationResult<T>.Failure(message, ErrorTypes.ExternalService);
        }

        /// <summary>
        /// Creates a failed operation result for a unexpected errror scenario."
        /// </summary>
        /// <param name="message">The message</param>
        /// <returns>The operation result</returns>
        public static OperationResult<T> FailUnexpected(string message)
        {
            ValidateMessage(message);
            return OperationResult<T>.Failure(message, ErrorTypes.Unexpected);
        }

        /// <summary>
        /// Creates a failed operation result for a unexpected errror scenario."
        /// </summary>
        /// <param name="message">The message</param>
        /// <returns>The operation result</returns>
        public static OperationResult<T> FailNetwork(string message)
        {
            ValidateMessage(message);
            return OperationResult<T>.Failure(message, ErrorTypes.Network);
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