namespace Infrastructure.ExternalServices.LogExternal
{
    using Domain.DTO.Log;
    using Domain.EnumType.LogLevel;
    using Domain.EnumType.OperationExecute;

    internal static class LogBuilderHelpers
    {

        /// <summary>
        /// Creates a log entry with the specified parameters.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <param name="entityName">The name of the associated entity.</param>
        /// <param name="entityValue">The serialized value of the entity.</param>
        /// <param name="level">The severity level of the log.</param>
        /// <param name="operation">The operation being executed.</param>
        /// <returns>A new Log entry.</returns>
        public static Log GetLog(string message, string entityName, string entityValue, LogLevel level, OperationExecute operation)
        {
            return new Log
            {
                Message = message,
                EntityName = entityName,
                EntityValue = entityValue,
                Level = Enum.GetName(level),
                Operation = Enum.GetName(operation),
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}