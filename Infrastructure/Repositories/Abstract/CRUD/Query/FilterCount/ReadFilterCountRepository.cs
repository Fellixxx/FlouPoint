namespace Infrastructure.Repositories.Abstract.CRUD.Query.FilterCount
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.Repository.CRUD.Query;
    using Infrastructure.Other;
    using Persistence.Repositories;
    using System;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;
    using Domain.EnumType;
    using Infrastructure.Constants;
    using Application.UseCases.ExternalServices.Resources;

    /// <summary>
    /// Abstract repository class for reading and counting entities based on a filter.
    /// </summary>
    /// <typeparam name = "T">The entity type this repository works with, constrained to reference types.</typeparam>
    public abstract class ReadFilterCountRepository<T> : Read<T>, IReadFilterCount<T> where T : class
    {
        // Protected field for logging operations.
        protected readonly ILogService _logService;
        // Private field for accessing resources.
        private readonly IResourcesProvider _provider;
        // Private field for handling resource-related operations.
        private IResourceHandler _handler;
        // List of resource keys used for retrieving messages.
        private readonly List<string> _resourceKeys;
        /// <summary>
        /// Constructor for initializing the repository with necessary services.
        /// </summary>
        /// <param name = "context">The database context for EF Core operations.</param>
        /// <param name = "logService">Service used to log error and informational messages.</param>
        /// <param name = "provider">Provides access to resources, such as strings or messages.</param>
        /// <param name = "handler">Handles operations related to resource management.</param>
        protected ReadFilterCountRepository(DbContext context, ILogService logService, IResourcesProvider provider, IResourceHandler handler) : base(context)
        {
            _logService = logService;
            _provider = provider;
            _handler = handler;
            // Initialize with predefined resource keys for successful operations.
            _resourceKeys = ["SuccessfullyReadFilterCount"];
        }

        /// <summary>
        /// Method for reading and counting entities with a specified filter.
        /// </summary>
        /// <param name = "filter">String representation of filter criteria.</param>
        /// <returns>Asynchronous operation returning the count of entities that match the filter.</returns>
        public async Task<Operation<int>> ReadFilterCount(string filter)
        {
            try
            {
                // Converts the string filter into a predicate expression.
                Expression<Func<T, bool>> predicate = GetPredicate(filter);
                // Performs the count operation based on the predicate.
                int result = await ReadCountFilter(predicate);
                // Asynchronously creates a resource handler with specified keys.
                await ResourceHandler.CreateAsync(_provider, _resourceKeys);
                // Retrieves a localized successful operation message.
                var messageSuccessfully = _handler.GetResource("SuccessfullyReadFilterCount");
                // Returns a successful operation result with count and message.
                return Operation<int>.Success(result, messageSuccessfully);
            }
            catch (Exception ex)
            {
                // Creates a log entry for any exceptions that occur.
                var log = Util.GetLogError(ex, filter, ActionType.GetCountFilter);
                // Attempts to log the error and checks the result.
                var result = await _logService.CreateLog(log);
                // Handle any failures occurring during the logging process.
                if (!result.IsSuccessful)
                {
                    result.ConvertTo<int>();
                }

                // Prepare a failure operation result for issues related to data access.
                var strategy = new DatabaseStrategy<int>();
                // Error message to indicate an error in the data layer.
                var errorOccurredDataLayer = Message.ErrorOccurredDataLayer;
                // Return the failure operation strategy.
                return OperationStrategy<int>.Fail(errorOccurredDataLayer, strategy);
            }
        }

        /// <summary>
        /// Abstract method to be implemented in derived classes for converting a filter to a predicate.
        /// </summary>
        /// <param name = "filter">String-based filter to be converted.</param>
        /// <returns>Expression-based predicate for filtering entities.</returns>
        public abstract Expression<Func<T, bool>> GetPredicate(string filter);
    }
}