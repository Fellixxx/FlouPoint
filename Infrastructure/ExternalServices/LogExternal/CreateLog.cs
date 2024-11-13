namespace Infrastructure.ExternalServices.LogExternal
{
    using Application.Result;
    using Newtonsoft.Json;
    using Domain.DTO.Logging;
    using Domain.EnumType;
    using Infrastructure.Constants;

    /// <summary>
    /// Provides functionality to create log entries for a given operation, including validation and error handling.
    /// </summary>
    public static class CreateLog
    {
        // Uncommented private variables are placeholders for potential future use
        // or to guide developers on potential areas for extension or modification.
        // private readonly IResourceProvider _resourceProvider;
        // private IResourceHandler _resourceHandler;
        // private readonly List<string> _resourceKeys;
        /// <summary>
        /// Attempts to create a log entry if the provided data is valid.
        /// </summary>
        /// <param name = "message">The message to log, describing the event or action.</param>
        /// <param name = "entity">The object associated with the log entry, providing context.</param>
        /// <param name = "type">The type of action being logged (e.g., Create, Update, Delete).</param>
        /// <param name = "level">The severity level of the log (e.g., Info, Warning, Error).</param>
        /// <returns>
        /// An operation result containing the created log entry if successful,
        /// or an error message and strategy if validation or creation fails.
        /// </returns>
        public static Operation<Log> TryCreate(string message, object entity, ActionType type, LogLevel level)
        {
            try
            {
                // Check if the message is non-empty and entity is not null
                if (string.IsNullOrWhiteSpace(message) || entity is null)
                {
                    // Define strategy and message for invalid data submission
                    var strategy = new NetworkErrorStrategy<Log>();
                    var invalidDataSubmitted = Message.Log.InvalidDataSubmitted;
                    // Return a failed operation result with the strategy applied
                    return OperationStrategy<Log>.Fail(invalidDataSubmitted, strategy);
                }

                // Retrieve the name of the entity's type and serialize the entity object into a JSON string
                string entityName = entity.GetType().Name;
                string entityValue = JsonConvert.SerializeObject(entity);
                // Build the log entry using the helper and required data
                Log log = LogBuilderHelpers.GetLog(message, entityName, entityValue, level, type);
                // Return a successful operation result with the created log entry and success message
                return Operation<Log>.Success(log, Message.Log.ValidationSuccess);
            }
            catch (JsonSerializationException jsonEx)
            {
                // Handle exception specifically related to JSON serialization issues
                var failedSerialize = string.Format(Message.Log.FailedToSerialize, jsonEx.Message);
                var strategy = new DatabaseStrategy<Log>();
                // Return a failure using a database strategy due to serialization error
                return OperationStrategy<Log>.Fail(failedSerialize, strategy);
            }
            catch (NullReferenceException nullEx)
            {
                // Capture and handle null reference exceptions
                var failedSerialize = string.Format(Message.Log.NullReference, nullEx.Message);
                var strategy = new UnexpectedErrorStrategy<Log>();
                // Return a failed operation with a strategy for unexpected errors from null references
                return OperationStrategy<Log>.Fail(failedSerialize, strategy);
            }
            catch (Exception ex)
            {
                // General handling for any other unexpected exceptions
                var unknowledgeableError = string.Format(Message.Log.NullReference, ex.Message);
                var strategy = new UnexpectedErrorStrategy<Log>();
                // Fail the operation but wrap it with a strategy applicable for unforeseen errors
                return OperationStrategy<Log>.Fail(unknowledgeableError, strategy);
            }
        }
    }
}