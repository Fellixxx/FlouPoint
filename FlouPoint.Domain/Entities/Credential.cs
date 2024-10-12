namespace FlouPoint.Domain.Entities
{
    public class Credential
    {
        /// <summary>
        /// Gets or sets the username of the user. This property serves as a unique identifier
        /// for a user in the system and is used for user logins. 
        /// This property should not be null once the user has been set.
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// Gets or sets the token of the user. This token is used for authentication 
        /// and should be stored securely. 
        /// This property should not be null once the token has been set.
        /// </summary>
        public string? Token { get; set; }
    }
}
