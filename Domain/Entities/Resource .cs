namespace Domain.Entities
{
    using Domain.Interfaces.Entity;

    /// <summary>
    /// Represents a resource entity with properties for identification, 
    /// key name, value, optional comments, created and updated timestamps,
    /// and an active status indicator. This class implements the <see cref = "IEntity"/> interface.
    /// </summary>
    public class Resource : IEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the resource entry.
        /// This property is used to uniquely identify each resource entry instance.
        /// Initialized to an empty string to prevent null references.
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the name of the resource entry.
        /// This property represents the key or name that identifies the resource.
        /// Initialized to an empty string for consistency.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the value of the resource entry.
        /// This property contains the actual data or content associated with the resource.
        /// Initialized to an empty string to ensure it is never null.
        /// </summary>
        public string Value { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets an optional comment associated with the resource entry.
        /// This property can provide additional information or context for the resource.
        /// Initialized to an empty string to maintain null safety.
        /// </summary>
        public string Comment { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the date and time when the resource was created.
        /// This property is crucial for auditing and tracking the resource lifecycles.
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Gets or sets the date and time when the resource was last updated.
        /// Can be null if the resource has never been updated since its creation, 
        /// helping to determine resources that haven't changed over time.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
        /// <summary>
        /// Gets or sets a boolean value indicating whether the resource entry is currently active.
        /// If this property is false, the resource is considered inactive or deprecated.
        /// Important for resource management and for operations that should only include active resources.
        /// </summary>
        public bool Active { get; set; }
    }
}