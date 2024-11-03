using Domain.EnumType.Extensions;

namespace Domain.EnumType
{
    /// <summary>
    /// Defines the levels of logs that can be used in the application.
    /// The level of a log entry indicates its severity.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Used for the most detailed log outputs.
        /// </summary>
        [EnumMetadata("Trace", "Used for the most detailed log outputs, including fine-grained information about the application's state.")]
        Trace,

        /// <summary>
        /// Used for interactive investigation during development.
        /// </summary>
        [EnumMetadata("Debug", "Used for interactive investigation during development, providing insights into the application behavior.")]
        Debug,

        /// <summary>
        /// Used to track the general flow of the application.
        /// </summary>
        [EnumMetadata("Information", "Used to track the general flow of the application, providing key insights and operational data.")]
        Information,

        /// <summary>
        /// Used for logs that highlight the abnormal or unexpected events in the application flow.
        /// </summary>
        [EnumMetadata("Warning", "Used for logs that highlight the abnormal or unexpected events in the application flow, which may need attention.")]
        Warning,

        /// <summary>
        /// Used for logs that highlight when the current flow of execution is stopped due to a failure.
        /// </summary>
        [EnumMetadata("Error", "Used for logs that highlight when the current flow of execution is stopped due to a failure or significant issue.")]
        Error,

        /// <summary>
        /// Used to log unhandled exceptions which forces the program to crash.
        /// </summary>
        [EnumMetadata("Fatal", "Used to log unhandled exceptions or critical errors that cause the program to crash or terminate.")]
        Fatal
    }
}
