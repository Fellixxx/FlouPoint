namespace Application.UseCases.Repository.CRUD.Resource
{
    using Application.Result;

    using Resource = Domain.Entities.Resource;
    public interface IReadId
    {
        /// <summary>
        /// Retrieves a ResourceEntry entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the ResourceEntry entity to retrieve.</param>
        /// <returns>An <see cref="Operation{T}"/> containing the ResourceEntry entity if it exists, or null if it does not.</returns>
        Task<Operation<Resource>> ReadId(string id);

        /// <summary>
        /// Retrieves a ResourceEntry entity based on the provided bearer token.
        /// This can be useful for authenticated requests where the bearer token is used to identify the entity.
        /// </summary>
        /// <param name="bearerToken">The bearer token used to authenticate and retrieve the ResourceEntry.</param>
        /// <returns>An <see cref="Operation{T}"/> containing the ResourceEntry entity if it exists, or null if it does not.</returns>
        Task<Operation<Resource>> ReadByBearer(string bearerToken);
    }
}
