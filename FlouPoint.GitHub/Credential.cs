namespace FlouPoint.GitHub
{
    /// <summary>
    /// Represents user credentials for GitHub authentication.
    /// </summary>
    public class Credential
    {
        /// <summary>
        /// Gets the username associated with the credentials.
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// Gets the token associated with the credentials.
        /// </summary>
        public string Token { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Credential"/> class with the specified username and token.
        /// </summary>
        /// <param name="username">The GitHub username.</param>
        /// <param name="token">The GitHub authentication token.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="username"/> or <paramref name="token"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="username"/> or <paramref name="token"/> is empty or consists only of white-space characters.
        /// </exception>
        public Credential(string username, string token)
        {
            // Validate that username is not null.
            if (username is null)
            {
                throw new ArgumentNullException(nameof(username), "Username cannot be null.");
            }

            // Validate that username is not empty or whitespace.
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username cannot be empty or whitespace.", nameof(username));
            }

            // Validate that token is not null.
            if (token is null)
            {
                throw new ArgumentNullException(nameof(token), "Token cannot be null.");
            }

            // Validate that token is not empty or whitespace.
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException("Token cannot be empty or whitespace.", nameof(token));
            }

            Username = username;
            Token = token;
        }
    }
}
