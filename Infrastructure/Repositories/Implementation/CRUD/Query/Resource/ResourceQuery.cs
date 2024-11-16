namespace Infrastructure.Repositories.Implementation.CRUD.Query.Resource
{
    using Application.Result;
    using Application.UseCases.CRUD.Query.Resource;
    using Application.UseCases.CRUD.Query.User;
    using Domain.Entities;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// The ResourceQuery class provides various methods to query resources from a data source.
    /// It handles operations like fetching resources by bearer token, filter, pagination, and resource ID.
    /// </summary>
    public class ResourceQuery : IResourceQuery
    {
        private readonly IResourceReadFilter _resourceReadFilter;
        private readonly IResourceReadFilterCount _resourceReadFilterCount;
        private readonly IResourceReadFilterPage _resourceReadFilterPage;
        private readonly IResourceReadId _resourceReadId;
        /// <summary>
        /// Initializes a new instance of the <see cref = "ResourceQuery"/> class.
        /// Sets up the various dependencies required for executing resource queries.
        /// </summary>
        /// <param name = "resourceReadFilter">The read filter implementation for querying resources.</param>
        /// <param name = "resourceReadFilterCount">The implementation for counting resources based on a filter.</param>
        /// <param name = "resourceReadFilterPage">The implementation for paginated resource queries.</param>
        /// <param name = "resourceReadId">The implementation for fetching resources by their ID.</param>
        public ResourceQuery(IResourceReadFilter resourceReadFilter, IResourceReadFilterCount resourceReadFilterCount, IResourceReadFilterPage resourceReadFilterPage, IResourceReadId resourceReadId)
        {
            _resourceReadFilter = resourceReadFilter;
            _resourceReadFilterCount = resourceReadFilterCount;
            _resourceReadFilterPage = resourceReadFilterPage;
            _resourceReadId = resourceReadId;
        }

        /// <summary>
        /// Reads a resource associated with the specified bearer token.
        /// </summary>
        /// <param name = "bearerToken">The bearer token used to identify the resource.</param>
        /// <returns>A task that represents the asynchronous operation to retrieve the resource.</returns>
        public Task<Operation<Resource>> ReadByBearer(string bearerToken)
        {
            return _resourceReadId.ReadByBearer(bearerToken);
        }

        /// <summary>
        /// Reads resources that match the specified predicate filter.
        /// </summary>
        /// <param name = "predicate">The predicate expression used to filter the resources.</param>
        /// <returns>A task that represents the asynchronous operation, returning a queryable result of resources.</returns>
        public Task<Operation<IQueryable<Resource>>> ReadFilter(Expression<Func<Resource, bool>> predicate)
        {
            return _resourceReadFilter.ReadFilter(predicate);
        }

        /// <summary>
        /// Gets the count of resources that match the specified filter criteria.
        /// </summary>
        /// <param name = "filter">The filter string used to count matching resources.</param>
        /// <returns>A task that represents the asynchronous operation to count resources.</returns>
        public Task<Operation<int>> ReadFilterCount(string filter)
        {
            return _resourceReadFilterCount.ReadFilterCount(filter);
        }

        /// <summary>
        /// Reads a paginated list of resources that match the specified filter.
        /// </summary>
        /// <param name = "pageNumber">The page number for pagination.</param>
        /// <param name = "pageSize">The number of resources per page.</param>
        /// <param name = "filter">The filter string used to query resources.</param>
        /// <returns>A task that represents the asynchronous operation to retrieve the paginated resources.</returns>
        public Task<Operation<IQueryable<Resource>>> ReadFilterPage(int pageNumber, int pageSize, string filter)
        {
            return _resourceReadFilterPage.ReadFilterPage(pageNumber, pageSize, filter);
        }

        /// <summary>
        /// Reads a resource by its unique identifier.
        /// </summary>
        /// <param name = "id">The unique identifier of the resource to be read.</param>
        /// <returns>A task that represents the asynchronous operation to retrieve the resource by ID.</returns>
        public Task<Operation<Resource>> ReadId(string id)
        {
            return _resourceReadId.ReadId(id);
        }
    }
}