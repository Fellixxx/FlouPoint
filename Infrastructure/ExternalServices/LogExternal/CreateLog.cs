namespace Infrastructure.ExternalServices.LogExternal
{
    using Application.Result;
    using Newtonsoft.Json;
    using Domain.DTO.Logging;
    using Domain.EnumType;

    public static class CreateLog
    {
        //private readonly IResourceProvider _resourceProvider;
        //private IResourceHandler _resourceHandler;
        //private readonly List<string> _resourceKeys;

        /// <summary>
        /// Validates the logging data, creates and returns the log entry if valid.
        /// </summary>
        /// <param name="message">The log message.</param>
        /// <param name="entity">The associated entity object to log.</param>
        /// <param name="operation">The operation being executed.</param>
        /// <param name="level">The severity level of the log.</param>
        /// <returns>Operation result containing the log entry if successful, or an error message if validation fails.</returns>
        public static OperationResult<Log> CreateLogIfValid(string message, object entity, OperationExecute operation, LogLevel level)
        {
            try
            {
                // Validation for message and entity
                if (string.IsNullOrWhiteSpace(message) || entity is null)
                {
                    return OperationBuilder<Log>.FailureDataSubmittedInvalid("The message or enitty was not submitted.");
                }

                // Get the name of the entity and serialize its value
                string entityName = entity.GetType().Name;
                string entityValue = JsonConvert.SerializeObject(entity);

                // Build the log entry
                Log log = LogBuilderHelpers.GetLog(message, entityName, entityValue, level, operation);
                return OperationResult<Log>.Success(log, "The Log validation of the OperationResult was successfully.");
            }
            catch (JsonSerializationException jsonEx)
            {
                // Handle exceptions related to JSON serialization
                return OperationBuilder<Log>.FailureDataSubmittedInvalid($"Failed to serialize entity: {jsonEx.Message}");
            }
            catch (NullReferenceException nullEx)
            {
                // Handle null reference exceptions
                return OperationBuilder<Log>.FailureUnexpectedError($"Null reference encountered: {nullEx.Message}");
            }
            catch (Exception ex)
            {
                // General error handling for unexpected issues
                return OperationBuilder<Log>.FailureUnexpectedError($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}