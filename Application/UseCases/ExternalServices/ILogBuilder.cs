namespace Application.UseCases.ExternalServices
{
    using Application.Result;
    using Domain.EnumType;

    /// <summary>
    /// ILogBuilder provides an interface for building log entries 
    /// with different log levels for a given type `T`.
    /// </summary>
    /// <typeparam name="T">Type to log</typeparam>
    public interface ILogBuilder<T>
    {
        /// <summary>
        /// Creates a trace log entry with the given message, entity, and operation details.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="entity">The entity</param>
        /// <param name="operation">The operation</param>
        /// <returns>The result of operation</returns>
        Operation<T> Trace(string message, object entity, ActionType operation);

        /// <summary>
        /// Creates a debug log entry with the given message, entity, and operation details.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="entity">The entity</param>
        /// <param name="operation">The operation</param>
        /// <returns>The result of operation</returns>
        Operation<T> Debug(string message, object entity, ActionType operation);

        /// <summary>
        /// Creates an informational log entry with the given message, entity, and operation details.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="entity">The entity</param>
        /// <param name="type">The operation</param>
        /// <returns>The result of operation</returns>
        Operation<T> Information(string message, object entity, ActionType type);

        /// <summary>
        /// Creates a warning log entry with the given message, entity, and operation details.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="entity">The entity</param>
        /// <param name="type">The operation</param>
        /// <returns>The result of operation</returns>
        Operation<T> Warning(string message, object entity, ActionType type);

        /// <summary>
        /// Creates an error log entry with the given message, entity, and operation details.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="entity">The entity</param>
        /// <param name="type"></param>
        /// <returns>The result of operation</returns>
        Operation<T> Error(string message, object entity, ActionType type);

        /// <summary>
        /// Creates a fatal error log entry with the given message, entity, and operation details.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="entity">The entity</param>
        /// <param name="type">The operation</param>
        /// <returns>The result of operation</returns>
        Operation<T> Fatal(string message, object entity, ActionType type);
    }
}
