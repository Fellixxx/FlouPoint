namespace Application.UseCases.Repository.Status.StatusChange
{
    using Application.Result;

    /// <summary>
    /// Defines the contract for activating a deactivated entity in the repository.
    /// </summary>
    public interface IActivateRepository
    {
        /// <summary>
        /// Activates a deactivated entity in the repository.
        /// </summary>
        /// <param name="id">The ID of the entity to activate.</param>
        /// <returns>An <see cref="Operation{T}"/> containing a boolean that indicates whether the activation was successful.</returns>
        Task<Operation<bool>> Activate(string id);
    }
}
