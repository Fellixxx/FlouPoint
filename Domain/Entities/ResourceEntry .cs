namespace Domain.Entities
{
    using Domain.Interfaces.Entity;

    public class ResourceEntry : IEntity
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
        public bool Active { get; set; }
    }
}