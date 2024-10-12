using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FlouPoint.GitHub
{
    /// <summary>
    /// Interface for a service that manages GitHub repositories.
    /// </summary>
    public interface IGitHubRepositoryService
    {
        /// <summary>
        /// Creates a new GitHub repository with the specified information.
        /// </summary>
        /// <param name="repositoryInfo">Information about the repository to create.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="repositoryInfo"/> is null.</exception>
        /// <exception cref="GitHubApiException">Thrown when the GitHub API returns an error response.</exception>
        Task CreateRepositoryAsync(RepositoryInfo repositoryInfo);
    }

    /// <summary>
    /// Represents information required to create a GitHub repository.
    /// </summary>
    public class RepositoryInfo
    {
        /// <summary>
        /// Gets the name of the repository.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the description of the repository.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets a value indicating whether the repository is private.
        /// </summary>
        public bool IsPrivate { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryInfo"/> class.
        /// </summary>
        /// <param name="name">The name of the repository.</param>
        /// <param name="description">The description of the repository.</param>
        /// <param name="isPrivate">Indicates whether the repository is private.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="name"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> is empty or whitespace.</exception>
        public RepositoryInfo(string name, string description, bool isPrivate)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name), "Repository name cannot be null.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Repository name cannot be empty or whitespace.", nameof(name));

            Name = name;
            Description = description ?? string.Empty;
            IsPrivate = isPrivate;
        }
    }

    /// <summary>
    /// Exception thrown when an error occurs while interacting with the GitHub API.
    /// </summary>
    public class GitHubApiException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GitHubApiException"/> class.
        /// </summary>
        public GitHubApiException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GitHubApiException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message.</param>
        public GitHubApiException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GitHubApiException"/> class with a specified error message and inner exception.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public GitHubApiException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    /// <summary>
    /// Provides methods to interact with GitHub repositories.
    /// </summary>
    public class GitHubRepositoryService : IGitHubRepositoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ICredentialProvider _credentialProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="GitHubRepositoryService"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client used to communicate with the GitHub API.</param>
        /// <param name="credentialProvider">The credential provider to obtain GitHub credentials.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="httpClient"/> or <paramref name="credentialProvider"/> is <c>null</c>.
        /// </exception>
        public GitHubRepositoryService(HttpClient httpClient, ICredentialProvider credentialProvider)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _credentialProvider = credentialProvider ?? throw new ArgumentNullException(nameof(credentialProvider));

            // Configure HttpClient headers once
            _httpClient.BaseAddress = new Uri("https://api.github.com/");
            _httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("CSharpApp");
        }

        /// <inheritdoc />
        public async Task CreateRepositoryAsync(RepositoryInfo repositoryInfo)
        {
            if (repositoryInfo == null)
                throw new ArgumentNullException(nameof(repositoryInfo));

            var credential = _credentialProvider.GetCredentials();

            // Set up the basic authentication header
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{credential.Username}:{credential.Token}"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            // Create the repository data
            var repositoryData = new
            {
                name = repositoryInfo.Name,
                description = repositoryInfo.Description,
                @private = repositoryInfo.IsPrivate
            };

            // Serialize to JSON
            var jsonContent = JsonConvert.SerializeObject(repositoryData);
            using var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync("user/repos", content).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    throw new GitHubApiException($"Error creating repository: {response.StatusCode} - {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new GitHubApiException("An error occurred while sending the request to the GitHub API.", ex);
            }
        }
    }
}
