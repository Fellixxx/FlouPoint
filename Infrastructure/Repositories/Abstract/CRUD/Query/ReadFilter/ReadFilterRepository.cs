namespace Infrastructure.Repositories.Abstract.CRUD.Query.ReadFilter
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.Repository.CRUD.Query;
    using Infrastructure.Other;
    using Persistence.Repositories;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;
    using Domain.DTO.Logging;
    using Domain.EnumType;
    using Infrastructure.Constants;
    using Application.UseCases.ExternalServices.Resources;

    /// <summary>
    /// Abstract repository class for reading and filtering entities.
    /// </summary>
    /// <typeparam name = "T">The entity type that the repository will operate on.</typeparam>
    public abstract class ReadFilterRepository<T> : Read<T>, IReadFilter<T> where T : class
    {
        private readonly ILogService _logService; // Used for logging operations and errors
        private readonly IResourcesProvider _provider; // Provides access to resource strings
        private IResourceHandler _handler; // Handles retrieval of specific resources
        private readonly List<string> _resourceKeys; // List of keys used to access resources
        /// <summary>
        /// Constructor with dependency injection.
        /// Initializes a new instance of the ReadFilterRepository class.
        /// </summary>
        /// <param name = "context">The database context for accessing the database.</param>
        /// <param name = "logService">The log service for logging operations and errors.</param>
        /// <param name = "provider">The resource provider for accessing localized resource strings.</param>
        /// <param name = "handler">The resource handler for managing resource-related operations.</param>
        protected ReadFilterRepository(DbContext context, ILogService logService, IResourcesProvider provider, IResourceHandler handler) : base(context)
        {
            _logService = logService;
            _provider = provider;
            _handler = handler;
            _resourceKeys = ["ReadFilterSuccess"]; // Resource keys for logging messages
        }

        /// <summary>
        /// Reads and filters entities based on the provided predicate.
        /// </summary>
        /// <param name = "predicate">The filter predicate to apply.</param>
        /// <returns>A task representing the asynchronous operation with the filtered entities as IQueryable.</returns>
        public new async Task<Operation<IQueryable<T>>> ReadFilter(Expression<Func<T, bool>> predicate)
        {
            try
            {
                IQueryable<T> result = await base.ReadFilter(predicate); // Calls the base read filter method
                await ResourceHandler.CreateAsync(_provider, _resourceKeys); // Initializes resources
                var successfullySearchGeneric = _handler.GetResource("ReadFilterSuccess"); // Retrieves the success message resource
                var messageSuccessfully = string.Format(successfullySearchGeneric, typeof(T).Name); // Formats the success message
                return Operation<IQueryable<T>>.Success(result, messageSuccessfully); // Returns a successful operation result
            }
            catch (Exception ex)
            {
                // Creates a log entry for the exception that occurred during filtering
                Log log = Util.GetLogError(ex, predicate, ActionType.GetAllByFilter);
                Operation<string> result = await _logService.CreateLog(log); // Logs the error
                // If logging fails, convert the result to the appropriate type
                if (!result.IsSuccessful)
                {
                    result.ConvertTo<IQueryable<T>>();
                }

                // Defines a strategy for handling database layer errors
                var strategy = new DatabaseStrategy<IQueryable<T>>();
                var errorOccurredDataLayer = Message.ErrorOccurredDataLayer; // Resource for error message
                return OperationStrategy<IQueryable<T>>.Fail(errorOccurredDataLayer, strategy); // Returns a failed operation result
            }
        }
    }
}