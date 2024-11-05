namespace Application.UseCases.CRUD.Query.User
{
    using Application.Result;
    using User = Domain.Entities.User;

    /// <summary>
    /// Defines the contract for reading a User entity by its ID.
    /// </summary>
    public interface IUserReadId
    {
        /// <summary>
        /// Retrieves a User entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the User entity to retrieve.</param>
        /// <returns>An <see cref="OperationResult{T}"/> containing the User entity if it exists, or null if it does not.</returns>
        Task<OperationResult<User>> ReadId(string id);

        /// <summary>
        /// Retrieves a User entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the User entity to retrieve.</param>
        /// <returns>An <see cref="OperationResult{T}"/> containing the User entity if it exists, or null if it does not.</returns>
        Task<OperationResult<User>> ReadByBearer(string bearerToken);
    }
}
