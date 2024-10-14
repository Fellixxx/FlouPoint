using Domain.Interfaces.Entity;

namespace FlouPoint.Domain.Entities
{
    /// <summary>
    /// Represents information about a repository.
    /// </summary>
    public class RepositoryInfo : IEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier of the repository.
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the repository.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the repository.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the repository is private.
        /// </summary>
        public bool IsPrivate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the repository is currently active.
        /// </summary>
        public bool Active { get; set; }
    }
}
