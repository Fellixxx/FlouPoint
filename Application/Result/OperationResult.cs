namespace Application.Result
{
    using Application.Result.Error;
    using Application.Result.Exceptions;

    public class OperationResult<T> : Result<T>
    {
        private OperationResult()
        {

        }

        private const string InvalidOperation = "This method can only be used if the value of IsSuccessful is false.";

        /// <summary>
        /// Checks if the current operation result indicates a failure.
        /// Throws an exception if the operation was successful.
        /// </summary>
        private void EnsureIsFailure()
        {
            if (IsSuccessful.Equals(true))
            {
                throw new InvalidOperation(InvalidOperation);
            }
        }

        /// <summary>
        /// Creates a new OperationResult with the specified generic type based on the current result.
        /// </summary>
        public OperationResult<U> AsType<U>()
        {
            EnsureIsFailure();
            return new OperationResult<U>
            {
                IsSuccessful = false,
                Message = this.Message,
                ErrorType = this.ErrorType
            };
        }

        /// <summary>
        /// Creates a successful operation result with the given data and optional message.
        /// </summary>
        /// <param name="data">Data result return</param>
        /// <param name="message">The message</param>
        /// <returns>The operation result</returns>
        public static OperationResult<T> Success(T? data, string? message = "")
        {
            return new OperationResult<T>
            {
                IsSuccessful = true,
                Data = data,
                Message = message ?? string.Empty,
                ErrorType = ErrorTypes.None
            };
        }

        /// <summary>
        /// Converts the current result to a boolean type.
        /// </summary>
        public OperationResult<bool> ToResultWithBoolType()
        {
            return AsType<bool>();
        }

        /// <summary>
        /// Converts the current result to an integer type.
        /// </summary>
        public OperationResult<int> ToResultWithIntType()
        {
            return AsType<int>();
        }

        /// <summary>
        /// Converts the current result to an string type.
        /// </summary>
        public OperationResult<string> ToResultWithStringType()
        {
            return AsType<string>();
        }

        /// <summary>
        /// Converts the current result to an integer type.
        /// </summary>
        public OperationResult<X> ToResultWithXType<X>()
        {
            return AsType<X>();
        }

        /// <summary>
        /// Converts the current result to its generic type.
        /// </summary>
        public OperationResult<T> ToResultWithGenericType()
        {
            return AsType<T>();
        }

        /// <summary>
        /// Creates a failed operation result with the given message and error type.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="errorTypes">The error type</param>
        /// <returns>The operation result</returns>
        public static OperationResult<T> Failure(string message, ErrorTypes errorTypes)
        {
            return new OperationResult<T> { IsSuccessful = false, Message = message, ErrorType = errorTypes };
        }
    }
}