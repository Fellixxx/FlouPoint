namespace Infrastructure.ExternalServices.LogExternal
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.Repository;
    using Application.UseCases.Wrapper;
    using Domain.DTO.Logging;
    using Infrastructure;
    using Infrastructure.Constants;
    using Infrastructure.Repositories;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// The LogService class handles the interactions with the logging service.
    /// </summary>
    public class LogService : LogServiceBase, ILogService
    {
        private readonly IResourceProvider _resourceProvider;
        private IResourceHandler _resourceHandler;
        private readonly List<string> _resourceKeys;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogService"/> class.
        /// </summary>
        /// <param name="clientFactory">Factory for creating instances of <see cref="HttpClient"/>.</param>
        /// <param name="configuration">Application's configuration interface.</param>
        /// <param name="httpContentWrapper">Wrapper for handling HTTP content.</param>
        public LogService(IHttpClientFactory clientFactory, IConfiguration configuration, IWrapper httpContentWrapper, IResourceProvider resourceProvider, IResourceHandler resourceHandler) :
            base(clientFactory, configuration, httpContentWrapper, resourceProvider, resourceHandler)
        {
            _resourceProvider=resourceProvider;
            _resourceHandler=resourceHandler;
            _resourceKeys =
            [
                "LogSuccessfullyGenericActiveated"
            ];
        }

        /// <summary>
        /// Asynchronously creates a log entry in the external log service.
        /// </summary>
        /// <param name="log">The log entity to create.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the operation result with an optional message.</returns>
        public async Task<OperationResult<string>> CreateLog(Log log)
        {
            try
            {
                var result = SetLog(log);
                if (!result.Result.IsSuccessful)
                {
                    return result.Result;
                }
                await ResourceHandler.CreateAsync(_resourceProvider, _resourceKeys);
                var successfullyLogCreate = _resourceHandler.GetResource("LogSuccessfullyGenericActiveated");
                return OperationResult<string>.Success(string.Empty, successfullyLogCreate);
            }
            catch (Exception ex)
            {
                var message = string.Format(ExceptionMessages.FailedGlobalException, ex.Message, ex.StackTrace);
                return OperationBuilder<string>.FailureUnexpectedError(ExceptionMessages.FailureUnexpectedError);
            }
        }
    }
}
