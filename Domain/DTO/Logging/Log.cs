namespace Domain.DTO.Logging
{
    using Domain.Interfaces.Loggin;

    /// <summary>
    /// Represents a log entry in the system, providing a detailed record of operations, their context, and their outcomes.
    /// </summary>
    public class Log : ILog
    {
        /// <summary>
        /// Gets or sets the main information about the log entry. 
        /// This typically describes what occurred during the logging event.
        /// </summary>
        public string? Message { get; set; }
        /// <summary>
        /// Gets or sets the name of the entity that was involved in the logging event.
        /// This helps to identify the component or module related to the log entry.
        /// </summary>
        public string? EntityName { get; set; }
        /// <summary>
        /// Gets or sets a serialized version of the entity's state at the moment the logging event occurred.
        /// This provides context for the entity's condition when the event took place.
        /// </summary>
        public string? EntityValue { get; set; }
        /// <summary>
        /// Gets or sets the severity level of the log entry.
        /// Typical values might include "Information", "Warning", "Error", etc., 
        /// and are stored as strings for simple JSON serialization.
        /// </summary>
        public string? Level { get; set; }
        /// <summary>
        /// Gets or sets a description of the operation that was executed.
        /// This provides insight into the specific action or process that was logged, stored as a string for ease of serialization.
        /// </summary>
        public string? Operation { get; set; }
        /// <summary>
        /// Gets or sets the date and time when the log entry was created.
        /// This timestamp helps to chronologically arrange the log entries.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}