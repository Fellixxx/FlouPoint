namespace Application.UseCases.CRUD.User
{
    using Application.Result;
    using Domain.Entities;

    /// <summary>
    /// Defines the contract for updating a User entity in the repository.
    /// </summary>
    public interface IUserUpdate //: IUpdateRepository<User>
    {
        /// <summary>
        /// Updates an existing User entity in the repository.
        /// </summary>
        /// <param name="entity">The User entity to update.</param>
        /// <returns>An <see cref="OperationResult{T}"/> containing a boolean that indicates whether the update was successful.</returns>
        Task<OperationResult<bool>> Update(User entity);
    }
}
