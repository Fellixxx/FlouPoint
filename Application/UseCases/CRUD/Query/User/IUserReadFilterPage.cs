namespace Application.UseCases.CRUD.Query.User
{
    using Application.Result;
    using System.Linq;
    using System.Threading.Tasks;
    using User = Domain.Entities.User;

    /// <summary>
    /// Defines the contract for reading a paginated list of User entities based on a filter.
    /// </summary>
    public interface IUserReadFilterPage
    {
        /// <summary>
        /// Retrieves a paginated list of User entities based on the provided filter string.
        /// The pageNumber and pageSize parameters control the pagination.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The size of each page.</param>
        /// <param name="filter">The filter string to apply.</param>
        /// <returns>An <see cref="OperationResult{T}"/> containing an IQueryable of User entities that match the filter criteria.</returns>
        Task<OperationResult<IQueryable<User>>> ReadFilterPage(int pageNumber, int pageSize, string filter);
    }
}
