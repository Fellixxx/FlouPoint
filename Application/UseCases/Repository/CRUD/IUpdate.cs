namespace Application.UseCases.Repository.CRUD
{
    using Application.Result;
    using Domain.Interfaces.Entity;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a contract for updating an existing entity in the repository.
    /// </summary>
    /// <typeparam name="T">The type of the entity. Must implement IEntity.</typeparam>
    public interface IUpdate<T> where T : class, IEntity
    {
        /// <summary>
        /// Updates an existing entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>An OperationResult containing a boolean that indicates whether the update was successful.</returns>
        Task<Operation<bool>> Update(T entity);
    }
}
