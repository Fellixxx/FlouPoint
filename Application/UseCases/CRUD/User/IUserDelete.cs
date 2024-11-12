namespace Application.UseCases.CRUD.User
{
    using Application.Result;

    /// <summary>
    /// Defines the contract for deleting a User entity from the repository.
    /// </summary>
    public interface IUserDelete //: IDeleteRepository<User>
    {
        /// <summary>
        /// Deletes a User entity from the repository based on its ID.
        /// </summary>
        /// <param name="id">The ID of the User entity to delete.</param>
        /// <returns>An <see cref="Operation{T}"/> containing a boolean that indicates whether the deletion was successful.</returns>
        Task<Operation<bool>> Delete(string id);
    }
}
