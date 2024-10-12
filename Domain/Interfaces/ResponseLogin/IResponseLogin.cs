namespace Domain.Interfaces.ResponseLogin
{
    /// <summary>
    /// The TokenResponse class represents a response from a token generating service such as an OAuth or JWT service.
    /// </summary>
    public interface IResponseLogin
    {
        /// <summary>
        ///  This is typically a string encoded in a certain format (like JWT) that contains information 
        /// </summary>
        string AccessToken { get; set; }

        /// <summary>
        ///  This is typically a string encoded in a certain format (like JWT) that contains information 
        /// </summary>
        string RefreshToken { get; set; }
    }
}
