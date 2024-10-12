// Namespace for persistence repository interfaces
namespace Persistence.Repositories.Interface
{
    using Domain.Interfaces.Entity;
    using System.Threading.Tasks; // Assuming that the 'Task' type is from System.Threading.Tasks namespace

    // Interface for generic repository operations.
    // This interface defines common methods for interacting with the data in the repository.
    public interface IRepository<T> : IRead<T> where T : class, IEntity
    {
        /// <summary>
        /// Adds an entity of type T to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the id of the added entity.</returns>
        Task<string> Create(T entity);

        /// <summary>
        /// Updates an entity of type T in the repository.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating if the update was successful.</returns>
        Task<bool> Update(T entity);

        /// <summary>
        /// Deletes an entity of type T from the repository.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating if the deletion was successful.</returns>
        Task<bool> Delete(T entity);
    }
}
