namespace Domain.Interfaces.Entity
{
    /// <summary>
    /// IDescribable is an interface that should be implemented by entities that can provide a description or message.
    /// It standardizes how entities can describe themselves through a message or textual representation.
    /// </summary>
    public interface IDescribable
    {
        /// <summary>
        /// Gets or sets a descriptive message for the entity.
        /// This property can be used to provide a summary or detailed description of the entity's current state.
        /// </summary>
        string Message { get; set; }
    }
}
