namespace Application.Result
{
    using Application.Constants;
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
            var errorMessage = string.Format(Messages.OperationStrategy.ErrorMessage, nameof(ValidateMessage));
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
}
