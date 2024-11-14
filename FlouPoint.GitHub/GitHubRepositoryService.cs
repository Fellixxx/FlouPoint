using System.Net.Http.Headers;
using System.Text;
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
        /// <param name = "repositoryInfo">Information about the repository to create.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref = "ArgumentNullException">Thrown when <paramref name = "repositoryInfo"/> is null.</exception>
        /// <exception cref = "GitHubApiException">Thrown when the GitHub API returns an error response.</exception>
        Task CreateRepositoryAsync(RepositoryInfo repositoryInfo);
    }

    /// <summary>
    /// Represents information required to create a GitHub repository.
    /// </summary>
    public class RepositoryInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }

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
        public GitHubApiException()
        {
        }

        public GitHubApiException(string message) : base(message)
        {
        }

        public GitHubApiException(string message, Exception innerException) : base(message, innerException)
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
        public GitHubRepositoryService(HttpClient httpClient, ICredentialProvider credentialProvider)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _credentialProvider = credentialProvider ?? throw new ArgumentNullException(nameof(credentialProvider));
            // Configure HttpClient headers once
            _httpClient.BaseAddress = new Uri("https://api.github.com/");
            _httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("CSharpApp");
        }

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