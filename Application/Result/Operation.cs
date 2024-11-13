namespace Application.Result
{
    using Application.Constants;
    using Application.Result.Error;
    using Application.Result.Exceptions;

    /// <summary>
    /// Represents the outcome of an operation with a result of type <typeparamref name = "T"/>.
    /// Provides methods to create both successful and failed operations.
    /// </summary>
    public class Operation<T> : Result<T>
    {
        /// <summary>
        /// Private constructor to prevent direct instantiation. 
        /// Instead, use factory methods like <see cref = "Success"/> or <see cref = "Failure"/>.
        /// </summary>
        private Operation()
        {
        }

        /// <summary>
        /// Ensures the operation represents a failure; otherwise, throws an <see cref = "InvalidOperation"/>.
        /// Useful for operations that should only occur if a failure has been recorded.
        /// </summary>
        /// <exception cref = "InvalidOperation">Thrown if the operation was successful.</exception>
        private void EnsureIsFailure()
        {
            if (IsSuccessful)
            {
                throw new InvalidOperation(Messages.Operation.InvalidOperation);
            }
        }

        /// <summary>
        /// Converts the current failed operation result to a specified generic type.
        /// This function can only be called if the current operation is a failure.
        /// </summary>
        /// <typeparam name = "U">The target result type.</typeparam>
        /// <returns>A new <see cref = "Operation{U}"/> with failure status reflecting the original operation.</returns>
        public Operation<U> AsType<U>()
        {
            EnsureIsFailure(); // Throws an error if the current operation isn't a failure.
            return new Operation<U>
            {
                IsSuccessful = false,
                Message = this.Message, // Carries over the failure message from the current operation.
                Type = this.Type // Carries over the error type from the current operation.
            };
        }

        /// <summary>
        /// Creates a successful operation result with the specified data and optional message.
        /// This is a factory method for generating successful operations.
        /// </summary>
        /// <param name = "data">The result data to be encapsulated in the operation.</param>
        /// <param name = "message">An optional message providing additional context on the success.</param>
        /// <returns>A successful <see cref = "Operation{T}"/> instance.</returns>
        public static Operation<T> Success(T? data, string? message = "")
        {
            return new Operation<T>
            {
                IsSuccessful = true, // Indicates a successful operation.
                Data = data, // Stores the successful result data.
                Message = message ?? string.Empty, // Allows for an optional success message.
                Type = ErrorTypes.None // No error type since the operation is successful.
            };
        }

        /// <summary>
        /// Creates a failed operation result with the specified message and error type.
        /// This is a factory method for generating failed operations.
        /// </summary>
        /// <param name = "message">The message describing the failure.</param>
        /// <param name = "errorTypes">Denotes the type of error that caused the failure.</param>
        /// <returns>A failed <see cref = "Operation{T}"/> instance.</returns>
        public static Operation<T> Failure(string message, ErrorTypes errorTypes)
        {
            return new Operation<T>
            {
                IsSuccessful = false, // Indicates a failed operation.
                Message = message, // Stores the failure message.
                Type = errorTypes // Captures the error type explaining the nature of the failure.
            };
        }

        /// <summary>
        /// Converts the current operation result to a specified generic type.
        /// This function calls <see cref = "AsType{U}"/> internally and is only permissible for failed operations.
        /// </summary>
        /// <typeparam name = "U">The target type to convert the operation to.</typeparam>
        /// <returns>A new <see cref = "Operation{U}"/> with failure status replicated from the current operation.</returns>
        public Operation<U> ConvertTo<U>() => AsType<U>(); // Another method to reroute to `AsType<U>`.
    }
}