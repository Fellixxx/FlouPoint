namespace Application.UseCases.Repository.CRUD.Resource
{
    using Application.Result;
    using Resource = Domain.Entities.Resource;
    public interface IReadFilterPage
    {
        /// <summary>
        /// Retrieves a paginated list of ResourceEntry entities based on the provided filter string.
        /// The pageNumber and pageSize parameters control the pagination.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The size of each page.</param>
        /// <param name="filter">The filter string to apply to the query.</param>
        /// <returns>An <see cref="Operation{T}"/> containing an IQueryable of ResourceEntry entities that match the filter criteria.</returns>
        Task<Operation<IQueryable<Resource>>> ReadFilterPage(int pageNumber, int pageSize, string filter);
    }
}
