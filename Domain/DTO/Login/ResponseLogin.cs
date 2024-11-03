namespace Domain.DTO.Login
{
    using Domain.Interfaces.Login;

    /// <summary>
    /// The TokenResponse class represents a response from a token generating service such as an OAuth or JWT service.
    /// </summary>
    public class ResponseLogin : IResponseLogin
    {
        /// <summary>
        ///  This is typically a string encoded in a certain format (like JWT) that contains information 
        /// </summary>
        public string AccessToken { get; set; } = string.Empty;

        /// <summary>
        ///  This is typically a string encoded in a certain format (like JWT) that contains information 
        /// </summary>
        public string RefreshToken { get; set; } = string.Empty;
    }
}
