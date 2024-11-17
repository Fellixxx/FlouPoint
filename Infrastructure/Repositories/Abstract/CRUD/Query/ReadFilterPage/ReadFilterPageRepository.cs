namespace Infrastructure.Repositories.Abstract.CRUD.Query.ReadFilterPage
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Infrastructure.Other;
    using Persistence.Repositories;
    using System;
    using System.Linq.Expressions;
    using Application.UseCases.Repository.CRUD.Query;
    using Microsoft.EntityFrameworkCore;
    using Domain.DTO.Logging;
    using Domain.EnumType;
    using Infrastructure.Constants;
    using Application.UseCases.ExternalServices.Resources;
    using Infrastructure.Repositories;
    using Application.UseCases.ExternalServices.Resources.Provider;

    /// <summary>
    /// Abstract repository class for reading and filtering entities with pagination.
    /// Provides functionality to read a set of entities of type <typeparamref name = "T"/>
    /// with pagination and filtering capabilities.
    /// </summary>
    /// <typeparam name = "T">The entity type.</typeparam>
    public abstract class ReadFilterPageRepository<T> : Read<T>, IReadFilterPage<T> where T : class
    {
        private readonly ILogService _logService;
        private readonly IResourcesProvider _provider;
        private IResourceHandler _handler;
        private readonly List<string> _resourceKeys;
        /// <summary>
        /// Initializes a new instance of the <see cref = "ReadFilterPageRepository{T}"/> class
        /// with the specified database context, log service, and resource provider.
        /// Injects the dependencies required for database operations, logging, and resource management.
        /// </summary>
        /// <param name = "context">The database context.</param>
        /// <param name = "logService">The log service.</param>
        /// <param name = "provider">The resource provider.</param>
        protected ReadFilterPageRepository(DbContext context, ILogService logService, IResourcesProvider provider) : base(context)
        {
            _logService = logService;
            _provider = provider;
            // Initialize the resource keys that are needed for resource management.
            _resourceKeys = new List<string>
            {
                "ReadFilterPageSuccess"
            };
        }

        /// <summary>
        /// Reads and filters entities with pagination.
        /// Asynchronously retrieves a paginated and filtered set of <typeparamref name = "T"/> entities.
        /// </summary>
        /// <param name = "pageNumber">The page number.</param>
        /// <param name = "pageSize">The size of the page to return.</param>
        /// <param name = "filter">The filter expression as a string.</param>
        /// <returns>A task representing the asynchronous operation with the filtered entities.</returns>
        public async Task<Operation<IQueryable<T>>> ReadFilterPage(int pageNumber, int pageSize, string filter)
        {
            try
            {
                // Get the predicate expression based on the filter string.
                Expression<Func<T, bool>> predicate = GetPredicate(filter);
                // Read entities from the database specified by the predicate, page number, and page size.
                IQueryable<T> result = await ReadPageByFilter(predicate, pageNumber, pageSize);
                // Prepare resources using the resource provider and the list of resource keys.
                await ResourceHandler.CreateAsync(_provider, _resourceKeys);
                // Get the success message resource and format it with the entity type name.
                var readFilterPageSuccess = _handler.GetResource("ReadFilterPageSuccess");
                var messageSuccessfully = string.Format(readFilterPageSuccess, typeof(T).Name);
                // Return the successful operation with the result and formatted message.
                return Operation<IQueryable<T>>.Success(result, messageSuccessfully);
            }
            catch (Exception ex)
            {
                // Capture error details in a log, including the failed operation's filter values.
                var filterValue = new
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Filter = filter
                };
                // Log the error and obtain the result of the logging operation.
                Log log = Util.GetLogError(ex, filterValue, ActionType.GetPageByFilter);
                Operation<string> result = await _logService.CreateLog(log);
                if (!result.IsSuccessful)
                {
                    // If logging was unsuccessful, handle result conversion for the failure scenario.
                    result.ConvertTo<IQueryable<T>>();
                }

                // If operation fails, return a failed operation with a default strategy and error message.
                var strategy = new DatabaseStrategy<IQueryable<T>>();
                var errorOccurredDataLayer = Message.ErrorOccurredDataLayer;
                return OperationStrategy<IQueryable<T>>.Fail(errorOccurredDataLayer, strategy);
            }
        }

        /// <summary>
        /// Abstract method that must be implemented to provide the predicate expression
        /// used to filter entities based on the provided filter string.
        /// </summary>
        /// <param name = "filter">The filter expression as a string.</param>
        /// <returns>An expression that represents a predicate to filter entities.</returns>
        public abstract Expression<Func<T, bool>> GetPredicate(string filter);
    }
}