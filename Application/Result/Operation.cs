namespace Application.Result
{
    using Application.Result.Error;
    using Application.Result.Exceptions;

    /// <summary>
    /// Represents the outcome of an operation with a result of type <typeparamref name="T"/>.
    /// Provides methods to create both successful and failed operations.
    /// </summary>
    public class Operation<T> : Result<T>
    {
        private const string InvalidOperation = "This method can only be used if the value of IsSuccessful is false.";
        /// <summary>
        /// Private constructor to prevent direct instantiation. Use factory methods <see cref="Success"/> or <see cref="Failure"/>.
        /// </summary>
        private Operation() { }



        /// <summary>
        /// Ensures the operation represents a failure; otherwise, throws an <see cref="InvalidOperation"/>.
        /// </summary>
        /// <exception cref="InvalidOperation">Thrown if the operation was successful.</exception>

        private void EnsureIsFailure()
        {
            if (IsSuccessful)
            {
                throw new InvalidOperation(InvalidOperation);
            }
        }

        /// <summary>
        /// Converts the current failed operation result to a specified generic type.
        /// Only allowed if the current operation is a failure.
        /// </summary>
        /// <typeparam name="U">The target result type.</typeparam>
        /// <returns>A new <see cref="Operation{U}"/> with failure status.</returns>
        public Operation<U> AsType<U>()
        {
            EnsureIsFailure();
            return new Operation<U>
            {
                IsSuccessful = false,
                Message = this.Message,
                Type = this.Type
            };
        }

        /// <summary>
        /// Creates a successful operation result with the given data and optional message.
        /// </summary>
        /// <param name="data">The result data.</param>
        /// <param name="message">Optional message describing the result.</param>
        /// <returns>A successful <see cref="Operation{T}"/> instance.</returns>
        public static Operation<T> Success(T? data, string? message = "")
        {
            return new Operation<T>
            {
                IsSuccessful = true,
                Data = data,
                Message = message ?? string.Empty,
                Type = ErrorTypes.None
            };
        }


        /// <summary>
        /// Creates a failed operation result with the specified message and error type.
        /// </summary>
        /// <param name="message">The failure message.</param>
        /// <param name="errorTypes">The type of error encountered.</param>
        /// <returns>A failed <see cref="Operation{T}"/> instance.</returns>
        public static Operation<T> Failure(string message, ErrorTypes errorTypes)
        {
            return new Operation<T>
            {
                IsSuccessful = false,
                Message = message,
                Type = errorTypes
            };
        }

        /// <summary>
        /// Converts the current operation result to a specified generic type.
        /// Only allowed if the operation represents a failure.
        /// </summary>
        /// <typeparam name="U">The target type to convert to.</typeparam>
        /// <returns>A new <see cref="Operation{U}"/> with the same failure status.</returns>
        public Operation<U> ConvertTo<U>() => AsType<U>();
    }
}