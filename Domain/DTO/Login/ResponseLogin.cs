namespace Domain.DTO.Login
{
    using Domain.Interfaces.Login;

    /// <summary>
    /// Represents a response received upon a successful login attempt, typically containing authentication tokens.
    /// Implements the <see cref = "IResponseLogin"/> interface.
    /// </summary>
    public class ResponseLogin : IResponseLogin
    {
        /// <summary>
        /// Gets or sets the access token that provides short-term access after successful authentication.
        /// This token is generally used to authorize requests to specific resources.
        /// </summary>
        public string AccessToken { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the refresh token used to obtain a new access token once the current one expires.
        /// The refresh token usually has a longer lifespan than the access token.
        /// </summary>
        public string RefreshToken { get; set; } = string.Empty;
    }
}