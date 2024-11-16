namespace Domain.Entities
{
    using Domain.Interfaces.Entity;

    /// <summary>
    /// Represents a resource entity with properties for identification, 
    /// key name, value, optional comments, and an active status indicator. 
    /// This class implements the <see cref = "IEntity"/> interface.
    /// </summary>
    public class Resource : IEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the resource entry.
        /// This property is used to uniquely identify each resource entry instance.
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the name of the resource entry.
        /// This property represents the key or name that identifies the resource.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the value of the resource entry.
        /// This property contains the actual data or content associated with the resource.
        /// </summary>
        public string Value { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets an optional comment associated with the resource entry.
        /// This property can provide additional information or context for the resource.
        /// </summary>
        public string Comment { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets a value indicating whether the resource entry is active.
        /// This property determines if the resource entry is currently enabled or in use.
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Gets or sets the date and time when the User was last updated.
        /// This property can be null if the user has not been updated since creation.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
        /// <summary>
        /// Gets or sets a boolean value indicating whether the User account is currently active or not.
        /// If this property is false, the user account is considered disabled or deactivated.
        /// </summary>
        public bool Active { get; set; }
    }
}