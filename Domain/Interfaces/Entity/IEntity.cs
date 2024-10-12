namespace Domain.Interfaces.Entity
{
    /// <summary>
    /// IEntity is an interface that should be implemented by all domain entities.
    /// It provides a standard structure for identifying and tracking the state of entities.
    /// </summary>
    public interface IEntity : IIdentifiable, IActivatable
    {

    }
}
