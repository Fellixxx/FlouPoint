namespace Domain.DTO.Login
{
    using Domain.Interfaces.Login;

    /// <summary>
    /// Represents a response to a login request, containing access and refresh tokens.
    /// </summary>
    public class ResponseLogin : IResponseLogin
    {
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        public string AccessToken { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the refresh token.
        /// </summary>
        public string RefreshToken { get; set; } = string.Empty;
    }
}