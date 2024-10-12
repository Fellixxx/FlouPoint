namespace Domain.Interfaces.Entity
{
    /// <summary>
    /// IEntity is an interface that should be implemented by all domain entities.
    /// It provides a standard structure for identifying and tracking the state of entities.
    /// </summary>
    public interface IIdentifiable
    {
        /// <summary>
        /// Gets or sets the unique identifier of an entity. 
        /// This property should be used to uniquely identify each instance of an implementing class.
        /// This is typically used as a primary key in the context of a database.
        /// </summary>
        string Id { get; set; }
    }
}
