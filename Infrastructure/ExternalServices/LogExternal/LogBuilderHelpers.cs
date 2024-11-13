namespace Infrastructure.ExternalServices.LogExternal
{
    using Domain.DTO.Logging;
    using Domain.EnumType;

    /// <summary>
    /// Provides helper methods for building log entries.
    /// This class contains utility methods to assist with the creation
    /// of log entries that are used throughout the logging infrastructure.
    /// </summary>
    internal static class LogBuilderHelpers
    {
        /// <summary>
        /// Creates a log entry with the specified parameters.
        /// </summary>
        /// <param name = "message">The log message.</param>
        /// <param name = "name">The name of the associated entity.</param>
        /// <param name = "value">The serialized value of the entity.</param>
        /// <param name = "level">The severity level of the log.</param>
        /// <param name = "type">The operation being executed.</param>
        /// <returns>A new Log entry.</returns>
        public static Log GetLog(string message, string name, string value, LogLevel level, ActionType type)
        {
            return new Log
            {
                Message = message,
                EntityName = name,
                EntityValue = value,
                Level = Enum.GetName(level),
                Operation = ActionType.GetName(type),
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}