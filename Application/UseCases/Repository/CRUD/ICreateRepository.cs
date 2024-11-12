namespace Application.UseCases.Repository.CRUD
{
    using Application.Result;
    using Domain.Interfaces.Entity;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a contract for creating a new entity in the repository.
    /// </summary>
    /// <typeparam name="T">The type of the entity. Must implement IEntity.</typeparam>
    public interface ICreateRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// Adds a new entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>An OperationResult containing the ID of the newly added entity.</returns>
        Task<Operation<string>> Create(T entity);
    }
}
