namespace Domain.DTO.User
{
    /// <summary>
    /// Represents a request to add a new user in the system. This class is typically used during
    /// the user registration process, where user details need to be captured and stored.
    /// </summary>
    public class CreateUser
    {
        /// <summary>
        /// Gets or sets the username of the user.
        /// This property acts as a unique identifier for the user in the system and is used
        /// for user logins. It is important to ensure that usernames are unique across the system
        /// to prevent login conflicts.
        /// It is possible for this property to be null if the username has not been set.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Gets or sets the password for the user's account. 
        /// It is crucial that this password is stored securely, ideally through hashing using
        /// a cryptographically secure algorithm, to protect the user's data from unauthorized access.
        /// Like the username, this property can also be null if the password has not been provided.
        /// </summary>
        public string? Password { get; set; }
        /// <summary>
        /// Gets or sets the email address of the user.
        /// The email serves not only as a means of communication with the user but also as
        /// a potential recovery option for account access if the user forgets their password.
        /// The email may be set to null if it has not yet been specified.
        /// </summary>
        public string? Email { get; set; }
    }
}