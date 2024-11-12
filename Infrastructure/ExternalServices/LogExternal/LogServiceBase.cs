namespace Infrastructure.ExternalServices.LogExternal
{
    using Application.Result;
    using Application.UseCases.Wrapper;
    using Infrastructure;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using System.Net.Http.Headers;
    using System.Text;
    using Domain.DTO.Login;
    using Domain.DTO.Logging;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.Repository;
    using Infrastructure.Repositories;

    /// <summary>
    /// Provides a base for interacting with an external logging service.
    /// </summary>
    public abstract class LogServiceBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _username;
        private readonly string _password;
        private readonly string _urlLogservice;
        private readonly IWrapper _httpContentWrapper;
        private readonly HttpClient _client;
        private readonly IResourceProvider _resourceProvider;
        private IResourceHandler _resourceHandler;
        private readonly List<string> _resourceKeys;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogServiceBase"/> class.
        /// </summary>
        /// <param name="clientFactory">Factory for creating instances of <see cref="HttpClient"/>.</param>
        /// <param name="configuration">Application's configuration interface.</param>
        /// <param name="httpContentWrapper">Wrapper for handling HTTP content.</param>
        protected LogServiceBase(IHttpClientFactory clientFactory, IConfiguration configuration, IWrapper httpContentWrapper, IResourceProvider resourceProvider, IResourceHandler resourceHandler)
        {
            _client = clientFactory.CreateClient();
            _configuration = configuration;
            _username = _configuration.GetSection("mongodb:username").Value ?? string.Empty;
            _password = _configuration.GetSection("mongodb:password").Value ?? string.Empty;
            _urlLogservice = _configuration.GetSection("logService:urlLogservice").Value ?? string.Empty;
            _httpContentWrapper = httpContentWrapper;
            _resourceProvider = resourceProvider;
            _resourceHandler = resourceHandler;
            _resourceKeys =
            [
                "FailureConfigurationMissingError",
                "FailedGetToken",
                "SuccessfullyGetToken",
                "FailedSetLog",
                "SuccessfullySetLog"
            ];
        }

        /// <summary>
        /// Asynchronously gets a token for authentication with the logging service.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result contains the operation result with the authentication token or an error message.</returns>
        private async Task<Operation<string>> GetToken()
        {
            await ResourceHandler.CreateAsync(_resourceProvider, _resourceKeys);
            if (HasParameter())
            {
                await ResourceHandler.CreateAsync(_resourceProvider, _resourceKeys);
                var failureConfigurationMissingError = _resourceHandler.GetResource("FailureConfigurationMissingError");
                var message = failureConfigurationMissingError;
                return OperationBuilder<string>.FailConfig(message);
            }

            var url = GetUrl();
            var response = await _httpContentWrapper.PostAsync(_client, url, null, null);

            if (!response.IsSuccessStatusCode)
            {
                var failedGetToken = _resourceHandler.GetResource("FailedGetToken");
                return OperationBuilder<string>.FailExternal(failedGetToken);
            }

            var result = await _httpContentWrapper.ReadAsStringAsync(response.Content);
            var _tokenResponse = JsonConvert.DeserializeObject<ResponseLogin>(result);

            string accessToken = _tokenResponse?.AccessToken ?? string.Empty;
            var successfullyLogCreate = _resourceHandler.GetResource("SuccessfullyGetToken");
            return Operation<string>.Success(accessToken, successfullyLogCreate);
        }

        /// <summary>
        /// Builds the URL used for logging in to the external logging service.
        /// </summary>
        /// <returns>The constructed URL.</returns>
        private string GetUrl()
        {
            return $"{_urlLogservice}/Login?user={_username}&password={_password}";
        }

        /// <summary>
        /// Determines if any of the required parameters (URL, username, password) are missing or invalid.
        /// </summary>
        /// <returns>True if any parameters are missing or invalid, otherwise false.</returns>
        private bool HasParameter()
        {
            return string.IsNullOrWhiteSpace(_urlLogservice) ||
                   string.IsNullOrWhiteSpace(_username) ||
                   string.IsNullOrWhiteSpace(_password);
        }

        /// <summary>
        /// Asynchronously creates a log entry in the external log service.
        /// </summary>
        /// <param name="log">The log entity to create.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the operation result with an optional message.</returns>
        protected async Task<Operation<string>> SetLog(Log log)
        {
            var bearerToken = await GetToken();

            if (!bearerToken.IsSuccessful)
            {
                return bearerToken;
            }

            var url = $"{_urlLogservice}";
            var json = JsonConvert.SerializeObject(log);
            var content = GetContent(json);
            var authorization = new AuthenticationHeaderValue("Bearer", bearerToken.Data);
            var response = await _httpContentWrapper.PostAsync(_client, url, content, authorization);
            await ResourceHandler.CreateAsync(_resourceProvider, _resourceKeys);
            if (!response.IsSuccessStatusCode)
            {
                var failedSetLog = _resourceHandler.GetResource("FailedGetToken");
                return OperationBuilder<string>.FailExternal(failedSetLog);
            }
            var successfullySetLog = _resourceHandler.GetResource("FailedGetToken");
            return Operation<string>.Success(string.Empty, successfullySetLog);
        }

        /// <summary>
        /// Creates the HTTP content to be sent in the request.
        /// </summary>
        /// <param name="json">The JSON string to include in the content.</param>
        /// <returns>The constructed HTTP content.</returns>
        private static StringContent GetContent(string json)
        {
            return new StringContent(json ?? "NOT_POSIBLE_GET_A_VALID_LOG", Encoding.UTF8, "application/json");
        }
    }
}