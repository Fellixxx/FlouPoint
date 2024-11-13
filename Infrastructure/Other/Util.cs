namespace Infrastructure.Other
{
    using Application.Result;
    using Domain.DTO.Logging;
    using Domain.EnumType;
    using Infrastructure.Constants;
    using Infrastructure.ExternalServices.LogExternal;
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Utility class providing static helper methods for logging and diagnostics.
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// Generates a log entry for a specific exception, detailing the error message, stack trace,
        /// associated entity, and operation causing the exception.
        /// </summary>
        /// <param name = "ex">The exception instance to be logged. Must not be null.</param>
        /// <param name = "entity">The related entity involved in the operation when the exception occurred.</param>
        /// <param name = "operation">The type of action being performed when the exception was thrown.</param>
        /// <returns>A <see cref = "Log"/> object representing the error details. If not successful, returns a new, default Log object.</returns>
        /// <exception cref = "Exception">Thrown if the exception parameter is null, indicating a failure in logging validation.</exception>
        public static Log GetLogError(Exception ex, object entity, ActionType operation)
        {
            // Validate that the exception object provided is not null to avoid logging issues.
            if (ex == null)
            {
                throw new Exception(Message.FailedLogValidation);
            }

            // Format the log message with the exception's message and stack trace for detailed error reporting.
            string message = string.Format(Message.Utility.LogEntryFormat, ex.Message, ex.StackTrace);
            // Obtain an instance of the LogBuilder responsible for creating structured log entries.
            LogBuilder logBuilder = LogBuilder.GetLogBuilder();
            // Use the logBuilder to create an error log entry encapsulating all provided details.
            Operation<Log> result = logBuilder.Error(message, entity, operation);
            // Extract and return the log entry from the operation result. If unavailable, return a new log object.
            Log data = result?.Data ?? new Log();
            return data;
        }

        /// <summary>
        /// Retrieves the name of the method or property that called this method for diagnostic purposes.
        /// </summary>
        /// <param name = "memberName">Auto-filled by the runtime, this represents the name of the calling method or property.</param>
        /// <returns>A string representing the name of the method or property that invoked this utility method.</returns>
        public static string GetMethodName([CallerMemberName] string memberName = "")
        {
            // Simply returns the provided memberName which represents the caller method or property name.
            return memberName;
        }
    }
}