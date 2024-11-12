namespace Application.UseCases.Repository.CRUD.Query
{
    using Application.Result;

    /// <summary>
    /// Defines a contract for reading the count of items based on a filter.
    /// </summary>
    /// <typeparam name="T">The type of the entity. Must implement IEntity.</typeparam>
    public interface IReadFilterCountRepository<T> where T : class
    {
        /// <summary>
        /// Retrieves the count of items that match the specified filter.
        /// </summary>
        /// <param name="filter">The filter criteria to apply.</param>
        /// <returns>An OperationResult containing the count of items that match the filter.</returns>
        Task<Operation<int>> ReadFilterCount(string filter);
    }
}
