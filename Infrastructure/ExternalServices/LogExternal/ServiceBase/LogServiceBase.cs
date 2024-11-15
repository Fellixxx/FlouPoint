namespace Infrastructure.ExternalServices.LogExternal.ServiceBase
{
    using Application.Result;
    using Application.UseCases.Wrapper;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using System.Net.Http.Headers;
    using System.Text;
    using Domain.DTO.Login;
    using Domain.DTO.Logging;
    using Infrastructure.Repositories;
    using Application.UseCases.ExternalServices.Resources;

    /// <summary>
    /// Provides a base for interacting with an external logging service by managing authentication and log creation.
    /// </summary>
    public abstract class LogServiceBase
    {
        private readonly IConfiguration _configuration; // Configuration interface for accessing app settings.
        private readonly string _username; // Username for the logging service authentication.
        private readonly string _password; // Password for the logging service authentication.
        private readonly string _urlLogservice; // Base URL of the logging service.
        private readonly IWrapper _httpContentWrapper; // Wrapper for handling HTTP content and requests.
        private readonly HttpClient _client; // HttpClient instance for making HTTP requests.
        private readonly IResourcesProvider _provider; // Provider for resource management.
        private IResourceHandler _handler; // Handler for managing resources.
        private readonly List<string> _resourceKeys; // Keys for accessing various resources.
        /// <summary>
        /// Initializes a new instance of the <see cref = "LogServiceBase"/> class.
        /// Configures necessary parameters like credentials and URL for logging service.
        /// </summary>
        /// <param name = "factory">Factory for creating HttpClient instances.</param>
        /// <param name = "config">Configuration service for accessing config settings.</param>
        /// <param name = "wrapper">Wrapper for HTTP content handling.</param>
        /// <param name = "provider">Resource provider for managing resources.</param>
        /// <param name = "handler">Handler to access and manipulate resources.</param>
        protected LogServiceBase(IHttpClientFactory factory, IConfiguration config, IWrapper wrapper, IResourcesProvider provider, IResourceHandler handler)
        {
            _client = factory.CreateClient(); // Initializes the HttpClient using the factory.
            _configuration = config; // Holds application configuration settings.
            // Retrieves necessary credentials and URL from the configuration.
            _username = _configuration.GetSection("mongodb:username").Value ?? string.Empty;
            _password = _configuration.GetSection("mongodb:password").Value ?? string.Empty;
            _urlLogservice = _configuration.GetSection("logService:urlLogservice").Value ?? string.Empty;
            _httpContentWrapper = wrapper; // Assigns the HTTP content wrapper.
            _provider = provider; // Assigns the resource provider.
            _handler = handler; // Assigns the resource handler.
            _resourceKeys = ["LogConfigMissingError", "LogTokenFetchFailed", "LogTokenFetched", "LogCreationFailed", "LogCreated"]; // Initializes resource keys.
        }

        /// <summary>
        /// Asynchronously fetches an authentication token to interact with the logging service.
        /// </summary>
        /// <returns>An Operation result with the authentication token or an error message if unsuccessful.</returns>
        private async Task<Operation<string>> GetToken()
        {
            await ResourceHandler.CreateAsync(_provider, _resourceKeys); // Ensures resource keys are prepared.
            if (HasParameter()) // Checks for missing parameters.
            {
                await ResourceHandler.CreateAsync(_provider, _resourceKeys);
                var logConfigMissingError = _handler.GetResource("LogConfigMissingError");
                var message = logConfigMissingError;
                var strategy = new ConfigMissingStrategy<string>();
                return OperationStrategy<string>.Fail(message, strategy); // Returns failure if configuration is incomplete.
            }

            var url = GetUrl(); // Constructs the login URL.
            var response = await _httpContentWrapper.PostAsync(_client, url, null, null); // Attempts to get the token by sending a POST request.
            if (!response.IsSuccessStatusCode)
            {
                var logTokenFetchFailed = _handler.GetResource("LogTokenFetchFailed");
                var strategy = new ExternalServiceStrategy<string>();
                return OperationStrategy<string>.Fail(logTokenFetchFailed, strategy); // Returns failure if the request failed.
            }

            var result = await _httpContentWrapper.ReadAsStringAsync(response.Content); // Reads the response content.
            var _tokenResponse = JsonConvert.DeserializeObject<ResponseLogin>(result); // Deserializes the content to retrieve the token.
            string accessToken = _tokenResponse?.AccessToken ?? string.Empty; // Extracts the token or provides an empty string.
            var logTokenFetched = _handler.GetResource("LogTokenFetched");
            return Operation<string>.Success(accessToken, logTokenFetched); // Returns the token if successful.
        }

        /// <summary>
        /// Generates the URL needed for logging into the external logging service.
        /// </summary>
        /// <returns>The constructed login URL.</returns>
        private string GetUrl()
        {
            return $"{_urlLogservice}/Login?user={_username}&password={_password}"; // Forms the URL using the base URL, username, and password.
        }

        /// <summary>
        /// Checks if any critical parameters (URL, username, password) are missing or invalid.
        /// </summary>
        /// <returns>True if any parameters are missing or invalid, otherwise false.</returns>
        private bool HasParameter()
        {
            return string.IsNullOrWhiteSpace(_urlLogservice) || string.IsNullOrWhiteSpace(_username) || string.IsNullOrWhiteSpace(_password);
        }

        /// <summary>
        /// Asynchronously creates a log entry in the external logging service.
        /// </summary>
        /// <param name = "log">The log entity to be created.</param>
        /// <returns>An Operation result indicating success or failure of the log creation.</returns>
        protected async Task<Operation<string>> SetLog(Log log)
        {
            var bearerToken = await GetToken(); // Acquires the authentication token.
            if (!bearerToken.IsSuccessful)
            {
                return bearerToken; // Returns early if token acquisition failed.
            }

            var url = $"{_urlLogservice}"; // Preparation for the log service URL.
            var json = JsonConvert.SerializeObject(log); // Serializes the log object to JSON.
            var content = GetContent(json); // Creates HTTP content using the serialized JSON.
            var authorization = new AuthenticationHeaderValue("Bearer", bearerToken.Data); // Constructs the authorization header.
            var response = await _httpContentWrapper.PostAsync(_client, url, content, authorization); // Sends a POST request to create the log.
            await ResourceHandler.CreateAsync(_provider, _resourceKeys); // Ensures resource keys are available.
            if (!response.IsSuccessStatusCode)
            {
                var logCreationFailed = _handler.GetResource("LogTokenFetchFailed");
                var strategy = new ExternalServiceStrategy<string>();
                return OperationStrategy<string>.Fail(logCreationFailed, strategy); // Returns failure if the log creation request failed.
            }

            var logTokenFetchFailed = _handler.GetResource("LogTokenFetchFailed");
            return Operation<string>.Success(string.Empty, logTokenFetchFailed); // Indicates a successful log creation.
        }

        /// <summary>
        /// Prepares the HTTP content for requests based on provided JSON string.
        /// </summary>
        /// <param name = "json">The JSON string that represents the request body.</param>
        /// <returns>The constructed StringContent with JSON payload.</returns>
        private static StringContent GetContent(string json)
        {
            return new StringContent(json ?? "NOT_POSIBLE_GET_A_VALID_LOG", Encoding.UTF8, "application/json");
        }
    }
}