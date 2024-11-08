﻿namespace Domain.DTO.User
{
    /// <summary>
    /// Represents a user in the context of the application.
    /// Implements the IEntity interface, indicating that it can be used with the application's data persistence layer.
    /// </summary>
    public class ProxyUser
    {
        /// <summary>
        /// Gets or sets the unique identifier for the User. This is typically a primary key in the database.
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the username of the user. This property serves as a unique identifier
        /// for a user in a system and is used for user logins. 
        /// This property can be null if the username has not yet been set.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the email of the user. This can be used for communication with the user,
        /// as well as a recovery method for account access. 
        /// This property can be null if the email has not yet been set.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the User was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the User was last updated.
        /// This property can be null if the user has not been updated since creation.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the avatar of the user. This property serves as a unique identifier
        /// for a user in a system and is used for user logins. 
        /// This property can be null if the username has not yet been set.
        /// </summary>
        public string? Avatar { get; set; }

        /// <summary>
        /// Gets or sets a boolean value indicating whether the User account is currently active or not.
        /// If this property is false, the user account is considered disabled or deactivated.
        /// </summary>
        public bool Active { get; set; }
    }
}
