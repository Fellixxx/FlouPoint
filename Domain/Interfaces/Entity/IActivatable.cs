namespace Domain.Interfaces.Entity
{
    /// <summary>
    /// IEntity is an interface that should be implemented by all domain entities.
    /// It provides a standard structure for identifying and tracking the state of entities.
    /// </summary>
    public interface IActivatable
    {

        /// <summary>
        /// Gets or sets a boolean value indicating whether the entity is currently active or not.
        /// Implementing classes should use this property to track the active status of an entity.
        /// This can be used to soft delete entities or toggle their active status without deleting records.
        /// </summary>
        bool Active { get; set; }
    }
}
