namespace Infrastructure.ExternalServices.LogExternal
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Domain.DTO.Log;
    using global::Domain.EnumType;
    using Domain.EnumType.LogLevel;
    using Domain.EnumType.OperationExecute;

    /// <summary>
    /// Provides functionality to build and validate log entries.
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
        /// The following method create log entries for specific trace log level.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="entity">The entity</param>
        /// <param name="operation">The operation</param>
        /// <returns>The log</returns>
        public OperationResult<Log> Trace(string message, object entity, OperationExecute operation)
        {
            return CreateLog.CreateLogIfValid(message, entity, operation, LogLevel.Trace);
        }

        /// <summary>
        /// The following method create log entries for specific debug log level.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="entity">The entity</param>
        /// <param name="operation">The operation</param>
        /// <returns>The log</returns>
        public OperationResult<Log> Debug(string message, object entity, OperationExecute operation)
        {
            return CreateLog.CreateLogIfValid(message, entity, operation, LogLevel.Debug);
        }

        /// <summary>
        /// The following method create log entries for specific information log level.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="entity">The entity</param>
        /// <param name="operation">The operation</param>
        /// <returns>The log</returns>
        public OperationResult<Log> Information(string message, object entity, OperationExecute operation)
        {
            return CreateLog.CreateLogIfValid(message, entity, operation, LogLevel.Information);
        }

        /// <summary>
        /// The following method create log entries for specific warning log level.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="entity">The entity</param>
        /// <param name="operation">The operation</param>
        /// <returns>The log</returns>
        public OperationResult<Log> Warning(string message, object entity, OperationExecute operation)
        {
            return CreateLog.CreateLogIfValid(message, entity, operation, LogLevel.Warning);
        }

        /// <summary>
        /// The following method create log entries for specific error log level.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="entity">The entity</param>
        /// <param name="operation">The operation</param>
        /// <returns>The log</returns>
        public OperationResult<Log> Error(string message, object entity, OperationExecute operation)
        {
            return CreateLog.CreateLogIfValid(message, entity, operation, LogLevel.Error);
        }

        /// <summary>
        /// The following method create log entries for specific fatal log level.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="entity">The entity</param>
        /// <param name="operation">The operation</param>
        /// <returns>The log</returns>
        public OperationResult<Log> Fatal(string message, object entity, OperationExecute operation)
        {
            return CreateLog.CreateLogIfValid(message, entity, operation, LogLevel.Fatal);
        }
    }
}
