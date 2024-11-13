namespace Application.UseCases.Repository.CRUD.Query
{
    using Application.Result;

    /// <summary>
    /// Defines a contract for reading a paginated list of items based on a filter.
    /// </summary>
    /// <typeparam name="T">The type of the entity. Must implement IEntity.</typeparam>
    public interface IReadFilterPage<T> where T : class
    {
        /// <summary>
        /// Retrieves a paginated list of items that match the specified filter.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <param name="filter">The filter criteria to apply.</param>
        /// <returns>An OperationResult containing an IQueryable of items that match the filter, paginated according to the specified page number and size.</returns>
        Task<Operation<IQueryable<T>>> ReadFilterPage(int pageNumber, int pageSize, string filter);
    }
}
