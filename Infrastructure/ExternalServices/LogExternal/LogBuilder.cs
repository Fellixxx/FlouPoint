namespace Infrastructure.ExternalServices.LogExternal
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Domain.EnumType;
    using Domain.DTO.Logging;

    /// <summary>
    /// Provides functionality to build and validate log entries.
    /// This class implements a singleton pattern to ensure a single instance of LogBuilder is used throughout the application.
    /// </summary>
    public class LogBuilder : ILogBuilder<Log>
    {
        // Lazy instance ensures thread-safety for singleton initialization
        private static readonly Lazy<LogBuilder> _lazyInstance = new Lazy<LogBuilder>(() => new LogBuilder());
        // Private constructor ensures that LogBuilder objects can only be created within this class.
        private LogBuilder()
        {
        }

        /// <summary>
        /// Retrieves the singleton instance of LogBuilder.
        /// </summary>
        /// <returns>The singleton instance of LogBuilder.</returns>
        public static LogBuilder GetLogBuilder() => _lazyInstance.Value;
        /// <summary>
        /// Creates a log entry for the Trace log level.
        /// </summary>
        /// <param name = "message">The log message to be recorded.</param>
        /// <param name = "entity">The entity associated with the log entry.</param>
        /// <param name = "type">The type of action that triggered the log entry.</param>
        /// <returns>An Operation containing the Log object.</returns>
        public Operation<Log> Trace(string message, object entity, ActionType type)
        {
            return CreateLog.TryCreate(message, entity, type, LogLevel.Trace);
        }

        /// <summary>
        /// Creates a log entry for the Debug log level.
        /// </summary>
        /// <param name = "message">The log message to be recorded.</param>
        /// <param name = "entity">The entity associated with the log entry.</param>
        /// <param name = "type">The type of action that triggered the log entry.</param>
        /// <returns>An Operation containing the Log object.</returns>
        public Operation<Log> Debug(string message, object entity, ActionType type)
        {
            return CreateLog.TryCreate(message, entity, type, LogLevel.Debug);
        }

        /// <summary>
        /// Creates a log entry for the Information log level.
        /// </summary>
        /// <param name = "message">The log message to be recorded.</param>
        /// <param name = "entity">The entity associated with the log entry.</param>
        /// <param name = "type">The type of action that triggered the log entry.</param>
        /// <returns>An Operation containing the Log object.</returns>
        public Operation<Log> Information(string message, object entity, ActionType type)
        {
            return CreateLog.TryCreate(message, entity, type, LogLevel.Information);
        }

        /// <summary>
        /// Creates a log entry for the Warning log level.
        /// </summary>
        /// <param name = "message">The log message to be recorded.</param>
        /// <param name = "entity">The entity associated with the log entry.</param>
        /// <param name = "type">The type of action that triggered the log entry.</param>
        /// <returns>An Operation containing the Log object.</returns>
        public Operation<Log> Warning(string message, object entity, ActionType type)
        {
            return CreateLog.TryCreate(message, entity, type, LogLevel.Warning);
        }

        /// <summary>
        /// Creates a log entry for the Error log level.
        /// </summary>
        /// <param name = "message">The log message to be recorded.</param>
        /// <param name = "entity">The entity associated with the log entry.</param>
        /// <param name = "type">The type of action that triggered the log entry.</param>
        /// <returns>An Operation containing the Log object.</returns>
        public Operation<Log> Error(string message, object entity, ActionType type)
        {
            return CreateLog.TryCreate(message, entity, type, LogLevel.Error);
        }

        /// <summary>
        /// Creates a log entry for the Fatal log level.
        /// </summary>
        /// <param name = "message">The log message to be recorded.</param>
        /// <param name = "entity">The entity associated with the log entry.</param>
        /// <param name = "type">The type of action that triggered the log entry.</param>
        /// <returns>An Operation containing the Log object.</returns>
        public Operation<Log> Fatal(string message, object entity, ActionType type)
        {
            return CreateLog.TryCreate(message, entity, type, LogLevel.Fatal);
        }
    }
}