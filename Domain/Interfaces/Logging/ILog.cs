namespace Domain.Interfaces.Loggin
{
    /// <summary>
    ///   The Log class represents a log entry in the system.
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Message property contains the main information about the log entry.
        /// </summary>
        string? Message { get; set; }

        /// <summary>
        /// EntityName property contains the name of the entity that was involved in the event.
        /// </summary>
        string? EntityName { get; set; }

        /// <summary>
        /// EntityValue property contains a serialized version of the entity's state at the moment the event occurred.
        /// </summary>
        string? EntityValue { get; set; }

        /// <summary>
        /// Level property represents the severity of the log entry, converted to a string for easier JSON serialization.
        /// </summary>
        string? Level { get; set; }

        /// <summary>
        /// Operation property indicates what operation was executed, converted to a string for easier JSON serialization.
        /// </summary>
        string? Operation { get; set; }

        /// <summary>
        /// CreatedAt property stores the date and time when the log entry was created.
        /// </summary>
        DateTime CreatedAt { get; set; }
    }
}
