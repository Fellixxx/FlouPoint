namespace Infrastructure.Repositories.Implementation.CRUD.Query
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.Repository.CRUD.Resource;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Persistence.Repositories;
    using System.Linq.Expressions;

    /// <summary>
    /// Provides query operations for the Resource entity.
    /// This class implements reading functionalities to query data from the database.
    /// </summary>
    public class ResourceEntryQuery : Read<Resource>, IQuery
    {
        protected readonly ILogService _logService;
        /// <summary>
        /// Initializes a new instance of the <see cref = "ResourceEntryQuery"/> class.
        /// </summary>
        /// <param name = "context">The database context to be used for data access.</param>
        /// <param name = "logService">The logging service to log information, warnings, and errors.</param>
        /// <exception cref = "ArgumentNullException">
        /// Thrown when either <paramref name = "context"/> or <paramref name = "logService"/> is null.
        /// </exception>
        public ResourceEntryQuery(DbContext context, ILogService logService) : base(context)
        {
            //_authFlowDbContext = context ?? throw new ArgumentNullException(nameof(context));
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
        }

        /// <summary>
        /// Reads a Resource entity identified by its bearer token.
        /// </summary>
        /// <param name = "bearerToken">The bearer token used to identify the resource.</param>
        /// <returns>A task that represents the asynchronous operation, with an operation result containing the Resource entity.</returns>
        public Task<Operation<Resource>> ReadByBearer(string bearerToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the count of resources matching the specified filter.
        /// </summary>
        /// <param name = "filter">The filter string used to match resources.</param>
        /// <returns>A task that represents the asynchronous operation, with an operation result containing the count of matching resources.</returns>
        public Task<Operation<int>> ReadFilterCount(string filter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads a page of resources that match the specified filter.
        /// </summary>
        /// <param name = "pageNumber">The number of the page to retrieve.</param>
        /// <param name = "pageSize">The size of the page to retrieve.</param>
        /// <param name = "filter">The filter string to match resources.</param>
        /// <returns>A task that represents the asynchronous operation, with an operation result containing the page of matching resources.</returns>
        public Task<Operation<IQueryable<Resource>>> ReadFilterPage(int pageNumber, int pageSize, string filter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads a Resource entity by its identifier.
        /// </summary>
        /// <param name = "id">The identifier of the resource to read.</param>
        /// <returns>A task that represents the asynchronous operation, with an operation result containing the Resource entity.</returns>
        public Task<Operation<Resource>> ReadId(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads resources that match a given predicate.
        /// </summary>
        /// <param name = "predicate">The expression predicate to filter resources.</param>
        /// <returns>A task that represents the asynchronous operation, with an operation result containing the queried resources.</returns>
        Task<Operation<IQueryable<Resource>>> IReadFilter.ReadFilter(Expression<Func<Resource, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}