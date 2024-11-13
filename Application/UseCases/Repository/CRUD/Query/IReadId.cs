namespace Application.UseCases.Repository.CRUD.Query
{
    using Application.Result;
    using Domain.Interfaces.Entity;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a contract for reading an entity based on its Id.
    /// </summary>
    /// <typeparam name="T">The type of the entity. Must implement IEntity.</typeparam>
    public interface IReadId<T> where T : class, IEntity
    {
        /// <summary>
        /// Retrieves an entity by its Id.
        /// </summary>
        /// <param name="id">The Id of the entity to retrieve.</param>
        /// <returns>An OperationResult containing the entity that matches the specified Id.</returns>
        Task<Operation<T>> ReadId(string id);

        /// <summary>
        /// Retrieves an entity by its Id.
        /// </summary>
        /// <param name="id">The Id of the entity to retrieve.</param>
        /// <returns>An OperationResult containing the entity that matches the specified Id.</returns>
        Task<Operation<T>> ReadByBearer(string bearerToken);
    }
}
