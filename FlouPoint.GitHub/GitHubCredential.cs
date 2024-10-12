namespace FlouPoint.GitHub
{
    /// <summary>
    /// Represents errors that occur when credentials are not found or invalid.
    /// </summary>
    public class CredentialNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CredentialNotFoundException"/> class.
        /// </summary>
        public CredentialNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CredentialNotFoundException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public CredentialNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CredentialNotFoundException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for this exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public CredentialNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    /// <summary>
    /// Represents a provider that retrieves credentials.
    /// </summary>
    public interface ICredentialProvider
    {
        /// <summary>
        /// Retrieves credentials.
        /// </summary>
        /// <returns>An instance of <see cref="Credential"/> containing the credentials.</returns>
        Credential GetCredentials();
    }

    /// <summary>
    /// Provides an interface for retrieving environment variables.
    /// </summary>
    public interface IEnvironmentVariableProvider
    {
        /// <summary>
        /// Retrieves the value of an environment variable.
        /// </summary>
        /// <param name="key">The name of the environment variable.</param>
        /// <returns>The value of the environment variable, or <c>null</c> if it is not found.</returns>
        string GetEnvironmentVariable(string key);
    }

    /// <summary>
    /// Provides methods for retrieving environment variables.
    /// </summary>
    public class EnvironmentVariableProvider : IEnvironmentVariableProvider
    {
        /// <inheritdoc />
        public string GetEnvironmentVariable(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            // Retrieves the value of an environment variable from the current process.
            return Environment.GetEnvironmentVariable(key);
        }
    }

    /// <summary>
    /// Provides methods to retrieve GitHub credentials from environment variables.
    /// </summary>
    public class GitHubCredentialProvider : ICredentialProvider
    {
        private readonly IEnvironmentVariableProvider _environmentVariableProvider;

        // Constants for the environment variable names.
        private const string GitHubUsernameEnvVar = "GITHUB_USERNAME";
        private const string GitHubTokenEnvVar = "GITHUB_TOKEN";

        /// <summary>
        /// Initializes a new instance of the <see cref="GitHubCredentialProvider"/> class.
        /// </summary>
        /// <param name="environmentVariableProvider">An instance of <see cref="IEnvironmentVariableProvider"/> to retrieve environment variables.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="environmentVariableProvider"/> is <c>null</c>.</exception>
        public GitHubCredentialProvider(IEnvironmentVariableProvider environmentVariableProvider)
        {
            _environmentVariableProvider = environmentVariableProvider ?? throw new ArgumentNullException(nameof(environmentVariableProvider));
        }

        /// <summary>
        /// Retrieves the GitHub credentials from the environment variables.
        /// </summary>
        /// <returns>An instance of <see cref="Credential"/> containing the GitHub credentials.</returns>
        /// <exception cref="CredentialNotFoundException">Thrown when the required environment variables are not set.</exception>
        public Credential GetCredentials()
        {
            // Retrieve the username and token from environment variables.
            var username = _environmentVariableProvider.GetEnvironmentVariable(GitHubUsernameEnvVar);
            var token = _environmentVariableProvider.GetEnvironmentVariable(GitHubTokenEnvVar);

            // Validate that the username is not null or whitespace.
            if (string.IsNullOrWhiteSpace(username))
                throw new CredentialNotFoundException($"GitHub username is not set in the environment variable '{GitHubUsernameEnvVar}'.");

            // Validate that the token is not null or whitespace.
            if (string.IsNullOrWhiteSpace(token))
                throw new CredentialNotFoundException($"GitHub token is not set in the environment variable '{GitHubTokenEnvVar}'.");

            // Return a new Credential object with the retrieved username and token.
            return new Credential(username, token);
        }
    }
}
