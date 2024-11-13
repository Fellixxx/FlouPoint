namespace Application.Result
{
    using Application.Result.Error;
    using System;

    public interface IErrorCreationStrategy<T>
    {
        Operation<T> CreateFailure(string message);
    }
    public class BusinessStrategy<T> : IErrorCreationStrategy<T>
    {
        public Operation<T> CreateFailure(string message)
        {
            return Operation<T>.Failure(message, ErrorTypes.BusinessValidation);
        }
    }

    public class ConfigMissingStrategy<T> : IErrorCreationStrategy<T>
    {
        public Operation<T> CreateFailure(string message)
        {
            return Operation<T>.Failure(message, ErrorTypes.ConfigMissing);
        }
    }

    public class DatabaseStrategy<T> : IErrorCreationStrategy<T>
    {
        public Operation<T> CreateFailure(string message)
        {
            return Operation<T>.Failure(message, ErrorTypes.Database);
        }
    }

    public class InvalidDataStrategy<T> : IErrorCreationStrategy<T>
    {
        public Operation<T> CreateFailure(string message)
        {
            return Operation<T>.Failure(message, ErrorTypes.InvalidData);
        }
    }

    public class ExternalServiceStrategy<T> : IErrorCreationStrategy<T>
    {
        public Operation<T> CreateFailure(string message)
        {
            return Operation<T>.Failure(message, ErrorTypes.ExternalService);
        }
    }

    public class UnexpectedErrorStrategy<T> : IErrorCreationStrategy<T>
    {
        public Operation<T> CreateFailure(string message)
        {
            return Operation<T>.Failure(message, ErrorTypes.Unexpected);
        }
    }

    public class NetworkErrorStrategy<T> : IErrorCreationStrategy<T>
    {
        public Operation<T> CreateFailure(string message)
        {
            return Operation<T>.Failure(message, ErrorTypes.Network);
        }
    }


    public static class OperationStrategy<T>
    {
        public static Operation<T> Fail(string message, IErrorCreationStrategy<T> strategy)
        {
            ValidateMessage(message);
            return strategy.CreateFailure(message);
        }

        private static void ValidateMessage(string? message)
        {
            var errorMessage = $"{nameof(ValidateMessage)}: The 'message' parameter cannot be null, empty, or whitespace.";
            Validate(message, errorMessage);
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

    public static class OperationBuilder<T>
    {


        /// <summary>
        /// Creates a failed operation result for an invalid data submission.
        /// </summary>
        /// <param name="message">The message describing why the data is invalid.</param>
        /// <returns>An <see cref="Operation{T}"/> representing the failure.</returns>
        public static Operation<T> FailInvalidData(string message)
            => CreateFailure(message, ErrorTypes.InvalidData);

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
