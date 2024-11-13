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
    using Application.UseCases.Repository;
    using Infrastructure.Repositories;
    using Application.UseCases.ExternalServices.Resorces;

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
        private readonly IResorcesProvider _provider;
        private IResourceHandler _handler;
        private readonly List<string> _resourceKeys;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogServiceBase"/> class.
        /// </summary>
        /// <param name="factory">Factory for creating instances of <see cref="HttpClient"/>.</param>
        /// <param name="config">Application's configuration interface.</param>
        /// <param name="wrapper">Wrapper for handling HTTP content.</param>
        protected LogServiceBase(IHttpClientFactory factory, IConfiguration config, IWrapper wrapper, IResorcesProvider provider, IResourceHandler handler)
        {
            _client = factory.CreateClient();
            _configuration = config;
            _username = _configuration.GetSection("mongodb:username").Value ?? string.Empty;
            _password = _configuration.GetSection("mongodb:password").Value ?? string.Empty;
            _urlLogservice = _configuration.GetSection("logService:urlLogservice").Value ?? string.Empty;
            _httpContentWrapper = wrapper;
            _provider = provider;
            _handler = handler;
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
            await ResourceHandler.CreateAsync(_provider, _resourceKeys);
            if (HasParameter())
            {
                await ResourceHandler.CreateAsync(_provider, _resourceKeys);
                var failureConfigurationMissingError = _handler.GetResource("FailureConfigurationMissingError");
                var message = failureConfigurationMissingError;
                return OperationBuilder<string>.FailConfig(message);
            }

            var url = GetUrl();
            var response = await _httpContentWrapper.PostAsync(_client, url, null, null);

            if (!response.IsSuccessStatusCode)
            {
                var failedGetToken = _handler.GetResource("FailedGetToken");
                return OperationBuilder<string>.FailExternal(failedGetToken);
            }

            var result = await _httpContentWrapper.ReadAsStringAsync(response.Content);
            var _tokenResponse = JsonConvert.DeserializeObject<ResponseLogin>(result);

            string accessToken = _tokenResponse?.AccessToken ?? string.Empty;
            var successfullyLogCreate = _handler.GetResource("SuccessfullyGetToken");
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
            await ResourceHandler.CreateAsync(_provider, _resourceKeys);
            if (!response.IsSuccessStatusCode)
            {
                var failedSetLog = _handler.GetResource("FailedGetToken");
                return OperationBuilder<string>.FailExternal(failedSetLog);
            }
            var successfullySetLog = _handler.GetResource("FailedGetToken");
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