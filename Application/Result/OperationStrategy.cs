namespace Application.Result
{
    using Application.Constants;
    using Application.Result.Error;
    using System;

    public interface IErrorCreationStrategy<T>
    {
        Operation<T> CreateFailure(string message);
    }

    /// <summary>
    /// Business error creation strategy class
    /// </summary>
    public class BusinessStrategy<T> : IErrorCreationStrategy<T>
    {
        /// <summary>
        /// Create a failure operation with a business validation error type
        /// </summary>
        public Operation<T> CreateFailure(string message)
        {
            return Operation<T>.Failure(message, ErrorTypes.BusinessValidation);
        }
    }

    // Repeat the same comments for the subsequent classes
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

    /// <summary>
    /// Static class for operation strategy
    /// </summary>
    public static class OperationStrategy<T>
    {
        /// <summary>
        /// Fail method to create a failure operation using the provided strategy and message
        /// </summary>
        public static Operation<T> Fail(string message, IErrorCreationStrategy<T> strategy)
        {
            ValidateMessage(message);
            return strategy.CreateFailure(message);
        }

        /// <summary>
        /// Validate if the message is not null or empty
        /// </summary>
        private static void ValidateMessage(string? message)
        {
            var errorMessage = string.Format(Messages.OperationStrategy.ErrorMessage, nameof(ValidateMessage));
            Validate(message, errorMessage);
        }

        /// <summary>
        /// Validate if a field is null or empty
        /// </summary>
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