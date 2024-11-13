namespace Application.Result
{
    using Application.Result.Error;
    using System;

    public static class OperationBuilder<T>
    {
        /// <summary>
        /// Creates a failed operation result for a business validation error.
        /// </summary>
        /// <param name="message">The message describing the validation failure.</param>
        /// <returns>An <see cref="Operation{T}"/> representing the failure.</returns>
        public static Operation<T> FailBusiness(string? message)
            => CreateFailure(message, ErrorTypes.BusinessValidation);

        /// <summary>
        /// Creates a failed operation result for a missing configuration error.
        /// </summary>
        /// <param name="message">The message describing the configuration error.</param>
        /// <returns>An <see cref="Operation{T}"/> representing the failure.</returns>
        public static Operation<T> FailConfig(string message)
            => CreateFailure(message, ErrorTypes.ConfigMissing);

        /// <summary>
        /// Creates a failed operation result for a database-related error.
        /// </summary>
        /// <param name="message">The message describing the database error.</param>
        /// <returns>An <see cref="Operation{T}"/> representing the failure.</returns>
        public static Operation<T> FailDatabase(string message)
            => CreateFailure(message, ErrorTypes.Database);

        /// <summary>
        /// Creates a failed operation result for an invalid data submission.
        /// </summary>
        /// <param name="message">The message describing why the data is invalid.</param>
        /// <returns>An <see cref="Operation{T}"/> representing the failure.</returns>
        public static Operation<T> FailInvalidData(string message)
            => CreateFailure(message, ErrorTypes.InvalidData);

        /// <summary>
        /// Creates a failed operation result for an external service error.
        /// </summary>
        /// <param name="message">The message describing the external service error.</param>
        /// <returns>An <see cref="Operation{T}"/> representing the failure.</returns>
        public static Operation<T> FailExternal(string message)
            => CreateFailure(message, ErrorTypes.ExternalService);

        /// <summary>
        /// Creates a failed operation result for an unexpected error.
        /// </summary>
        /// <param name="message">The message describing the unexpected error.</param>
        /// <returns>An <see cref="Operation{T}"/> representing the failure.</returns>
        public static Operation<T> FailUnexpected(string message)
            => CreateFailure(message, ErrorTypes.Unexpected);

        /// <summary>
        /// Creates a failed operation result for a network error.
        /// </summary>
        /// <param name="message">The message describing the network error.</param>
        /// <returns>An <see cref="Operation{T}"/> representing the failure.</returns>
        public static Operation<T> FailNetwork(string message)
            => CreateFailure(message, ErrorTypes.Network);

        /// <summary>
        /// Helper method to create a failure operation result with a specified error type.
        /// </summary>
        /// <param name="message">The message describing the failure.</param>
        /// <param name="type">The type of error causing the failure.</param>
        /// <returns>An <see cref="Operation{T}"/> representing the failure.</returns>
        private static Operation<T> CreateFailure(string? message, ErrorTypes type)
        {
            ValidateMessage(message);
            return Operation<T>.Failure(message, type);
        }

        /// <summary>
        /// Validates that the provided message is not null, empty, or whitespace.
        /// </summary>
        /// <param name="message">The message to validate.</param>
        /// <exception cref="ArgumentNullException">Thrown if the message is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the message is empty or whitespace.</exception>
        private static void ValidateMessage(string? message)
        {
            var errorMessage = $"{nameof(ValidateMessage)}: The 'message' parameter cannot be null, empty, or whitespace.";
            Validate(message, errorMessage);
        }

        /// <summary>
        /// General validation method to check that a string field is not null, empty, or whitespace.
        /// </summary>
        /// <param name="field">The field to validate.</param>
        /// <param name="errorMessage">The error message to use if validation fails.</param>
        /// <exception cref="ArgumentNullException">Thrown if the field is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the field is empty or whitespace.</exception>
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
