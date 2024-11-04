namespace Application.UseCases.CRUD.User
{
    using Application.Result;
    using Domain.Entities;

    /// <summary>
    /// Defines the contract for creating a new User entity in the repository.
    /// </summary>
    public interface IUserCreate
    {
        /// <summary>
        /// Adds a new User entity to the repository.
        /// </summary>
        /// <param name="entity">The User entity to add.</param>
        /// <returns>An <see cref="OperationResult{T}"/> containing the ID of the added entity.</returns>
        Task<OperationResult<string>> Create(User entity);
    }
}
