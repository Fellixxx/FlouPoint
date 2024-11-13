namespace Application.UseCases.Repository.CRUD
{
    using Application.Result;
    using Domain.Interfaces.Entity;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a contract for deleting an entity from the repository.
    /// </summary>
    /// <typeparam name="T">The type of the entity. Must implement IEntity.</typeparam>
    public interface IDelete<T> where T : class, IEntity
    {
        /// <summary>
        /// Deletes an entity from the repository based on its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns>An OperationResult containing a boolean that indicates whether the deletion was successful.</returns>
        Task<Operation<bool>> Delete(string id);
    }
}
