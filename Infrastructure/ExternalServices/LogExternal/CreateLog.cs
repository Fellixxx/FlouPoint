namespace Infrastructure.ExternalServices.LogExternal
{
    using Application.Result;
    using Newtonsoft.Json;
    using Domain.DTO.Logging;
    using Domain.EnumType;
    using Infrastructure.Constants;

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
        /// <param name="type">The operation being executed.</param>
        /// <param name="level">The severity level of the log.</param>
        /// <returns>Operation result containing the log entry if successful, or an error message if validation fails.</returns>
        public static Operation<Log> TryCreate(string message, object entity, ActionType type, LogLevel level)
        {
            try
            {
                // Validation for message and entity
                if (string.IsNullOrWhiteSpace(message) || entity is null)
                {
                    return OperationBuilder<Log>.FailInvalidData(Message.Log.InvalidDataSubmitted);
                }

                // Get the name of the entity and serialize its value
                string entityName = entity.GetType().Name;
                string entityValue = JsonConvert.SerializeObject(entity);

                // Build the log entry
                Log log = LogBuilderHelpers.GetLog(message, entityName, entityValue, level, type);
                return Operation<Log>.Success(log, Message.Log.ValidationSuccess);
            }
            catch (JsonSerializationException jsonEx)
            {
                // Handle exceptions related to JSON serialization
                var failedSerialize = string.Format(Message.Log.FailedToSerialize, jsonEx.Message);
                return OperationBuilder<Log>.FailInvalidData(failedSerialize);
            }
            catch (NullReferenceException nullEx)
            {
                // Handle null reference exceptions
                var failedSerialize = string.Format(Message.Log.NullReference, nullEx.Message);
                return OperationBuilder<Log>.FailUnexpected(failedSerialize);
            }
            catch (Exception ex)
            {
                // General error handling for unexpected issues
                var unknowledgeableError = string.Format(Message.Log.NullReference, ex.Message);
                return OperationBuilder<Log>.FailUnexpected(unknowledgeableError);
            }
        }
    }
}