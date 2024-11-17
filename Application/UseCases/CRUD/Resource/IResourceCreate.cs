namespace Application.UseCases.CRUD.Resource
{
    using Application.Result;
    using Domain.Entities;

    /// <summary>
    /// Defines the contract for creating a new User entity in the repository.
    /// </summary>
    public interface IResourceCreate
    {
        /// <summary>
        /// Adds a new User entity to the repository.
        /// </summary>
        /// <param name="entity">The User entity to add.</param>
        /// <returns>An <see cref="Operation{T}"/> containing the ID of the added entity.</returns>
        Task<Operation<string>> Create(Resource entity);
    }
}
