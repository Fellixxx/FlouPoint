namespace Infrastructure.ExternalServices.LogExternal
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.ExternalServices.Resources;
    using Application.UseCases.Wrapper;
    using Domain.DTO.Logging;
    using Infrastructure;
    using Infrastructure.Constants;
    using Infrastructure.Repositories;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// The LogService class is responsible for interacting with an external logging service.
    /// Inherits from LogServiceBase and implements ILogService interface.
    /// </summary>
    public class LogService : LogServiceBase, ILogService
    {
        // Private member variables
        private readonly IResourcesProvider _provider; // Provides resources needed for service operations
        private IResourceHandler _handler; // Handles resources, possibly for localization or templating
        private readonly List<string> _resourceKeys; // Stores keys to access specific resources
        /// <summary>
        /// Initializes a new instance of the <see cref = "LogService"/> class.
        /// Sets up required dependencies for the class by injecting them.
        /// </summary>
        /// <param name = "clientFactory">Factory for creating instances of <see cref = "HttpClient"/> used to make HTTP requests.</param>
        /// <param name = "configuration">Provides configuration settings for the application.</param>
        /// <param name = "httpContentWrapper">Wrapper for handling and processing HTTP content.</param>
        /// <param name = "provider">Provider to fetch resource data needed for various operations.</param>
        /// <param name = "handler">Handler to manage resource retrieval and manipulation.</param>
        public LogService(IHttpClientFactory clientFactory, IConfiguration configuration, IWrapper httpContentWrapper, IResourcesProvider provider, IResourceHandler handler) : base(clientFactory, configuration, httpContentWrapper, provider, handler)
        {
            // Initialize member variables with provided arguments
            _provider = provider;
            _handler = handler;
            // Initialize list with a predefined resource key
            _resourceKeys = new List<string>
            {
                "LogSuccessfullyGenericActiveated"
            };
        }

        /// <summary>
        /// Asynchronously creates a log entry in the external log service.
        /// This method attempts to log a message and handles exceptions that may occur during the process.
        /// </summary>
        /// <param name = "log">The log object containing details to be logged.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the operation outcome including a success message.</returns>
        public async Task<Operation<string>> CreateLog(Log log)
        {
            try
            {
                // Prepares the log for creation by setting necessary properties or checks
                var result = SetLog(log);
                // If the setup of the log isn't successful, return the error result
                if (!result.Result.IsSuccessful)
                {
                    return result.Result;
                }

                // Asynchronously handles resource creation, might set up templates or necessary preconditions for logging
                await ResourceHandler.CreateAsync(_provider, _resourceKeys);
                // Retrieves a resource message indicating successful log creation
                var successfullyLogCreate = _handler.GetResource("LogSuccessfullyGenericActiveated");
                // Return a successful operation result with an optional message
                return Operation<string>.Success(string.Empty, successfullyLogCreate);
            }
            catch (Exception ex)
            {
                // Format error message including any exception details
                var message = string.Format(Message.UnknownException, ex.Message, ex.StackTrace);
                // Create a strategy for handling this unexpected error
                var strategy = new UnexpectedErrorStrategy<string>();
                // Return a failed operation result using the strategy
                return OperationStrategy<string>.Fail(message, strategy);
            }
        }
    }
}