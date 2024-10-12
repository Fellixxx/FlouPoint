namespace Infrastructure.ExternalServices.LogExternal
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.Wrapper;
    using Domain.DTO.Log;
    using Infrastructure;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// The LogService class handles the interactions with the logging service.
    /// </summary>
    public class LogService : LogServiceBase, ILogService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogService"/> class.
        /// </summary>
        /// <param name="clientFactory">Factory for creating instances of <see cref="HttpClient"/>.</param>
        /// <param name="configuration">Application's configuration interface.</param>
        /// <param name="httpContentWrapper">Wrapper for handling HTTP content.</param>
        public LogService(IHttpClientFactory clientFactory, IConfiguration configuration, IWrapper httpContentWrapper) :
            base(clientFactory, configuration, httpContentWrapper)
        {

        }

        /// <summary>
        /// Asynchronously creates a log entry in the external log service.
        /// </summary>
        /// <param name="log">The log entity to create.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the operation result with an optional message.</returns>
        public Task<OperationResult<string>> CreateLog(Log log)
        {
            try
            {
                var result = SetLog(log);
                if (!result.Result.IsSuccessful)
                {
                    return Task.FromResult(result.Result);
                }

                var successfullyLogCreate = Resource.SuccessfullyLogCreate;
                return Task.FromResult(OperationResult<string>.Success(string.Empty, successfullyLogCreate));
            }
            catch (Exception ex)
            {
                var message = string.Format(Resource.FailedGolbalException, ex.Message, ex.StackTrace);
                return Task.FromResult(OperationBuilder<string>.FailureUnexpectedError(Resource.SuccessfullyLogCreate));
            }
        }
    }
}
