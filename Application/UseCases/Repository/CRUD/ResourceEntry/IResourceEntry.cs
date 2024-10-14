using Application.Result;
using System.Linq.Expressions;

namespace Application.UseCases.Repository.CRUD.ResourceEntry
{
    using ResourceEntry = Domain.Entities.ResourceEntry;

    /// <summary>
    /// The IResourceEntryQuery interface defines the contract for querying ResourceEntry entities.
    /// It extends multiple interfaces to provide filtering, counting, paginated retrieval, 
    /// and ID-based querying functionality for ResourceEntry entities.
    /// </summary>
    public interface IResourceEntryQuery : IResourceEntryReadFilter, IResourceEntryReadFilterCount, IResourceEntryReadFilterPage, IResourceEntryReadId
    {
    }

    public interface IResourceEntryReadFilter
    {
        /// <summary>
        /// Returns all ResourceEntry entities that satisfy the specified predicate.
        /// Takes a lambda expression representing the condition that the entities should meet.
        /// </summary>
        /// <param name="predicate">The predicate used to filter the entities.</param>
        /// <returns>An <see cref="OperationResult{T}"/> containing an IQueryable of ResourceEntry entities that meet the specified condition.</returns>
        Task<OperationResult<IQueryable<ResourceEntry>>> ReadFilter(Expression<Func<ResourceEntry, bool>> predicate);
    }

    public interface IResourceEntryReadFilterCount
    {
        /// <summary>
        /// Retrieves the count of ResourceEntry entities that match the specified filter.
        /// </summary>
        /// <param name="filter">The filter string to apply to the count operation.</param>
        /// <returns>An <see cref="OperationResult{T}"/> containing the count of entities that match the filter criteria.</returns>
        Task<OperationResult<int>> ReadFilterCount(string filter);
    }

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

    public interface IResourceEntryReadId
    {
        /// <summary>
        /// Retrieves a ResourceEntry entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the ResourceEntry entity to retrieve.</param>
        /// <returns>An <see cref="OperationResult{T}"/> containing the ResourceEntry entity if it exists, or null if it does not.</returns>
        Task<OperationResult<ResourceEntry>> ReadId(string id);

        /// <summary>
        /// Retrieves a ResourceEntry entity based on the provided bearer token.
        /// This can be useful for authenticated requests where the bearer token is used to identify the entity.
        /// </summary>
        /// <param name="bearerToken">The bearer token used to authenticate and retrieve the ResourceEntry.</param>
        /// <returns>An <see cref="OperationResult{T}"/> containing the ResourceEntry entity if it exists, or null if it does not.</returns>
        Task<OperationResult<ResourceEntry>> ReadByBearer(string bearerToken);
    }
}
