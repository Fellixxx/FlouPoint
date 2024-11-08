namespace Application.UseCases.Repository.CRUD.ResourceEntry
{
    using Application.Result;
    using ResourceEntry = Domain.Entities.ResourceEntry;
    public interface IResourceEntryReadFilterPage
    {
        /// <summary>
        /// Retrieves a paginated list of ResourceEntry entities based on the provided filter string.
        /// The pageNumber and pageSize parameters control the pagination.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The size of each page.</param>
        /// <param name="filter">The filter string to apply to the query.</param>
        /// <returns>An <see cref="OperationResult{T}"/> containing an IQueryable of ResourceEntry entities that match the filter criteria.</returns>
        Task<OperationResult<IQueryable<ResourceEntry>>> ReadFilterPage(int pageNumber, int pageSize, string filter);
    }
}
